namespace MpcNET.Test
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MpcNET.Tags;

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
    }
}