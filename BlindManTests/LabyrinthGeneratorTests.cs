using BlindMan.Domain;
using NUnit.Framework;

namespace BlindManTests
{
    [TestFixture]
    public class LabyrinthGeneratorTests
    {
        private LabyrinthModel labyrinthModel;
        private const int Width = 31;
        private const int Height = 21;
        private const int PortalsCount = 7;

        [SetUp]
        public void SetUp()
        {
            labyrinthModel = new LabyrinthGenerator().CreateLabyrinth(Width, Height, PortalsCount);
        }

        [Test]
        public void TestLabyrinthSize()
        {
            Assert.AreEqual(Width, labyrinthModel.Width);
            Assert.AreEqual(Height, labyrinthModel.Height);
        }

        [Test]
        public void TestPortalsCount()
        {
            Assert.AreEqual(PortalsCount, labyrinthModel.PortalPositions.Count);
        }

        [Test]
        public void TestPossibleTeleportCount()
        {
            Assert.True(labyrinthModel.TeleportPoints.Count > 5);
        }
    }
}