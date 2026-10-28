namespace MpcNET.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MpcNET.Commands.Playback;

    public partial class LibMpcTest
    {
        [TestMethod]
        public void ConsumeCommand_Serialize_Bool()
        {
            Assert.AreEqual("consume 1", new Commands.Playback.ConsumeCommand(true).Serialize());
            Assert.AreEqual("consume 0", new Commands.Playback.ConsumeCommand(false).Serialize());
        }

        [TestMethod]
        public void ConsumeCommand_Serialize_OneShot()
        {
            Assert.AreEqual("consume oneshot",
                new Commands.Playback.ConsumeCommand(OneShotState.OneShot).Serialize());
        }

        [TestMethod]
        public void SingleCommand_Serialize_OneShot()
        {
            Assert.AreEqual("single oneshot",
                new Commands.Playback.SingleCommand(OneShotState.OneShot).Serialize());
        }

        [TestMethod]
        public void IdleCommand_Serialize_NoSubsystem()
        {
            Assert.AreEqual("idle", new Commands.Status.IdleCommand((string)null).Serialize());
        }

        [TestMethod]
        public void IdleCommand_Serialize_SingleSubsystem()
        {
            Assert.AreEqual("idle player", new Commands.Status.IdleCommand("player").Serialize());
        }

        [TestMethod]
        public void IdleCommand_Serialize_MultipleSubsystems()
        {
            var cmd = new Commands.Status.IdleCommand(new[] { "player", "mixer", "options" });
            Assert.AreEqual("idle player mixer options", cmd.Serialize());
        }
    }
}
