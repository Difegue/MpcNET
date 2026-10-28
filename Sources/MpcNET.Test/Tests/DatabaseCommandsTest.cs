namespace MpcNET.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MpcNET.Tags;
    using MpcNET.Types;
    using MpcNET.Types.Filters;

    public partial class LibMpcTest
    {
        [TestMethod]
        public async Task ListAllTest()
        {
            var response = await Mpc.SendAsync(new Commands.Database.ListAllCommand());

            TestOutput.WriteLine("ListAllTest Result:");
            TestOutput.WriteLine(response);

            Assert.IsTrue(response.Response.Content.Count().Equals(7));
        }

        [TestMethod]
        public async Task FindGenreTest()
        {
            var response = await Mpc.SendAsync(new Commands.Database.FindCommand(MpdTags.Genre, "soundfx"));

            TestOutput.WriteLine("FindGenreTest Result:");
            TestOutput.WriteLine(response);

            Assert.IsTrue(response.Response.Content.Count().Equals(7));
        }

        [TestMethod]
        public void SearchCommand_Serialize_NoSort()
        {
            // Regression: ensure existing find/search commands still serialize without a sort token
            // after _sort was added to BaseFilterCommand.
            var cmd = new Commands.Database.SearchCommand(MpdTags.Artist, "test");
            Assert.AreEqual(@"search ""((artist == \""test\""))""", cmd.Serialize());
        }

        [TestMethod]
        public void FindAddCommand_Serialize_FilterOnly()
        {
            var cmd = new Commands.Database.FindAddCommand(MpdTags.Genre, "rock");
            Assert.AreEqual(@"findadd ""((genre == \""rock\""))""", cmd.Serialize());
        }

        [TestMethod]
        public void FindAddCommand_Serialize_WithPosition()
        {
            var cmd = new Commands.Database.FindAddCommand(MpdTags.Genre, "rock", "5");
            Assert.AreEqual(@"findadd ""((genre == \""rock\""))"" position 5", cmd.Serialize());
        }

        [TestMethod]
        public void FindAddCommand_Serialize_WithRelativePosition()
        {
            var cmd = new Commands.Database.FindAddCommand(MpdTags.Genre, "rock", "+0");
            Assert.AreEqual(@"findadd ""((genre == \""rock\""))"" position +0", cmd.Serialize());
        }

        [TestMethod]
        public void FindAddCommand_Serialize_WithWindowAndPosition()
        {
            var cmd = new Commands.Database.FindAddCommand(MpdTags.Genre, "rock", "+0", 0, 5);
            Assert.AreEqual(@"findadd ""((genre == \""rock\""))"" window 0:5 position +0", cmd.Serialize());
        }

        [TestMethod]
        public void SearchAddCommand_Serialize_WithPosition()
        {
            var cmd = new Commands.Database.SearchAddCommand(MpdTags.Artist, "test", "3");
            Assert.AreEqual(@"searchadd ""((artist == \""test\""))"" position 3", cmd.Serialize());
        }

        [TestMethod]
        public void SearchAddCommand_Serialize_WithRelativePosition()
        {
            var cmd = new Commands.Database.SearchAddCommand(MpdTags.Artist, "test", "-0");
            Assert.AreEqual(@"searchadd ""((artist == \""test\""))"" position -0", cmd.Serialize());
        }

        [TestMethod]
        public void SearchAddCommand_Serialize_WithoutPosition()
        {
            var cmd = new Commands.Database.SearchAddCommand(MpdTags.Artist, "test");
            Assert.AreEqual(@"searchadd ""((artist == \""test\""))""", cmd.Serialize());
        }

        [TestMethod]
        public void SearchAddPlCommand_Serialize_Basic()
        {
            var cmd = new Commands.Database.SearchAddPlCommand("myplaylist", MpdTags.Genre, "rock");
            Assert.AreEqual(@"searchaddpl ""myplaylist"" ""((genre == \""rock\""))""", cmd.Serialize());
        }

        [TestMethod]
        public void SearchAddPlCommand_Serialize_WithPosition()
        {
            var cmd = new Commands.Database.SearchAddPlCommand("myplaylist", MpdTags.Genre, "rock", "+0");
            Assert.AreEqual(@"searchaddpl ""myplaylist"" ""((genre == \""rock\""))"" position +0", cmd.Serialize());
        }

        [TestMethod]
        public void SearchAddPlCommand_Serialize_WithWindowAndPosition()
        {
            var cmd = new Commands.Database.SearchAddPlCommand("myplaylist", MpdTags.Genre, "rock", "2", 0, 10);
            Assert.AreEqual(@"searchaddpl ""myplaylist"" ""((genre == \""rock\""))"" window 0:10 position 2", cmd.Serialize());
        }

        [TestMethod]
        public void FilterAddedSince_Serialize_InSearchCommand()
        {
            var cmd = new Commands.Database.SearchCommand(new FilterAddedSince("2024-01-01", FilterOperator.None));
            // Note: when operator is None, GetDescription() yields "" so two spaces appear between "added-since" and the value.
            Assert.AreEqual(@"search ""((added-since  \""2024-01-01\""))""", cmd.Serialize());
        }

        [TestMethod]
        public void FilterAddedSince_RejectsNonNoneOperator()
        {
            Assert.ThrowsException<System.ArgumentException>(
                () => new FilterAddedSince("2024-01-01", FilterOperator.Equal));
        }

        [TestMethod]
        public void FilterTag_StartsWith_Serialize_InSearchCommand()
        {
            var cmd = new Commands.Database.SearchCommand(new FilterTag(MpdTags.Title, "Foo", FilterOperator.StartsWith));
            Assert.AreEqual(@"search ""((title starts_with \""Foo\""))""", cmd.Serialize());
        }

        [TestMethod]
        public void FilterTag_StartsWith_AllowedOperator()
        {
            // Should not throw
            var filter = new FilterTag(MpdTags.Artist, "Bea", FilterOperator.StartsWith);
            Assert.AreEqual("starts_with", FilterOperatorDescription(filter.Operator));
        }

        private static string FilterOperatorDescription(FilterOperator op)
        {
            return op.GetDescription();
        }

        [TestMethod]
        public void CountCommand_Serialize_FilterOnly()
        {
            var cmd = new Commands.Database.CountCommand(MpdTags.Genre, "rock");
            Assert.AreEqual(@"count ""((genre == \""rock\""))""", cmd.Serialize());
        }

        [TestMethod]
        public void CountCommand_Serialize_MultipleFilters()
        {
            var cmd = new Commands.Database.CountCommand(new List<KeyValuePair<ITag, string>>
            {
                new KeyValuePair<ITag, string>(MpdTags.Genre, "rock"),
                new KeyValuePair<ITag, string>(MpdTags.Artist, "foo"),
            });
            Assert.AreEqual(@"count ""((genre == \""rock\"") AND (artist == \""foo\""))""", cmd.Serialize());
        }

        [TestMethod]
        public void SearchCountCommand_Serialize_FilterOnly()
        {
            var cmd = new Commands.Database.SearchCountCommand(MpdTags.Genre, "rock");
            Assert.AreEqual(@"searchcount ""((genre == \""rock\""))""", cmd.Serialize());
        }

        [TestMethod]
        public void SearchCountCommand_Deserialize_ParsesSongsAndPlaytime()
        {
            var cmd = new Commands.Database.SearchCountCommand(MpdTags.Genre, "rock");
            var response = new SerializedResponse
            {
                ResponseValues = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("songs", "17"),
                    new KeyValuePair<string, string>("playtime", "5872"),
                },
            };
            var result = cmd.Deserialize(response);
            Assert.AreEqual(17, result.Songs);
            Assert.AreEqual(5872, result.Playtime);
        }
    }
}