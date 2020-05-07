using System.Windows.Forms;
using BlindMan.Domain;
using NUnit.Framework;

namespace BlindManTests
{
    [TestFixture]
    public class GameModelTests
    {
        private GameModel gameModel;

        [SetUp]
        public void SetUp()
        {
            gameModel = new GameModel();
        }
        
        [Test]
        public void TestButtons()
        {
            var isLeftButtonEventWorking = false;
            var isRightButtonEventWorking = false;
            var isUpButtonEventWorking = false;
            var isDownButtonEventWorking = false;
            var isSpaceButtonEventWorking = false;

            gameModel.LeftKeyDown += () => isLeftButtonEventWorking = true;
            gameModel.RightKeyDown += () => isRightButtonEventWorking = true;
            gameModel.DownKeyDown += () => isDownButtonEventWorking = true;
            gameModel.UpKeyDown += () => isUpButtonEventWorking = true;
            gameModel.SpaceKeyDown += () => isSpaceButtonEventWorking = true;

            gameModel.KeyDown(Keys.Left);
            gameModel.KeyDown(Keys.Right);
            gameModel.KeyDown(Keys.Up);
            gameModel.KeyDown(Keys.Down);
            gameModel.KeyDown(Keys.Space);

            Assert.True(isLeftButtonEventWorking);
            Assert.True(isRightButtonEventWorking);
            Assert.True(isUpButtonEventWorking);
            Assert.True(isDownButtonEventWorking);
            Assert.True(isSpaceButtonEventWorking);
        }

        [Test]
        public void TestGameStateChangingOnGameEnd()
        {
            gameModel.EndGame();
            
            Assert.AreEqual(GameState.GameWon, gameModel.GameState);
        }
        
        [Test]
        public void TestStateChangingEvent()
        {
            var actualState = gameModel.GameState;
            var expectedState = GameState.GameWon;

            gameModel.GameStateChanged += state => actualState = state;
            gameModel.GameState = expectedState;

            Assert.AreEqual(expectedState, actualState);
        }
    }
}