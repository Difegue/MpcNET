namespace MpcNET.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class LibMpcTest
    {
        [TestMethod]
        public void DeleteCommand_Serialize_SinglePosition()
        {
            Assert.AreEqual("delete 4", new Commands.Queue.DeleteCommand(4).Serialize());
        }

        [TestMethod]
        public void DeleteCommand_Serialize_Range()
        {
            Assert.AreEqual("delete 3:7", new Commands.Queue.DeleteCommand(3, 7).Serialize());
        }

        [TestMethod]
        public void PlaylistInfoCommand_Serialize_NoArgs()
        {
            Assert.AreEqual("playlistinfo", new Commands.Queue.PlaylistInfoCommand().Serialize());
        }

        [TestMethod]
        public void PlaylistInfoCommand_Serialize_SinglePosition()
        {
            Assert.AreEqual("playlistinfo 2", new Commands.Queue.PlaylistInfoCommand(2).Serialize());
        }

        [TestMethod]
        public void PlaylistInfoCommand_Serialize_Range()
        {
            Assert.AreEqual("playlistinfo 0:5", new Commands.Queue.PlaylistInfoCommand(0, 5).Serialize());
        }

        [TestMethod]
        public void PlChangesCommand_Serialize_VersionOnly()
        {
            Assert.AreEqual("plchanges 7", new Commands.Queue.PlChangesCommand(7).Serialize());
        }

        [TestMethod]
        public void PlChangesCommand_Serialize_VersionAndRange()
        {
            Assert.AreEqual("plchanges 7 0:10", new Commands.Queue.PlChangesCommand(7, 0, 10).Serialize());
        }

        [TestMethod]
        public void MoveCommand_Serialize_SingleSongIntTo()
        {
            Assert.AreEqual("move 2 5", new Commands.Queue.MoveCommand(2, 5).Serialize());
        }

        [TestMethod]
        public void MoveCommand_Serialize_SingleSongRelativeTo()
        {
            Assert.AreEqual("move 2 +0", new Commands.Queue.MoveCommand(2, "+0").Serialize());
        }

        [TestMethod]
        public void MoveCommand_Serialize_RangeTo()
        {
            Assert.AreEqual("move 1:5 +0", new Commands.Queue.MoveCommand(1, 5, "+0").Serialize());
        }
    }
}
