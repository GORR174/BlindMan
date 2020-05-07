using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BlindMan.Domain;
using NUnit.Framework;

namespace BlindManTests
{
    public class PlayerModelTests
    {
        [Test]
        public void TestPlayerFreeMoving()
        {
            var gameModel = new GameModel();
            var labyrinth = new LabyrinthModel(new[,]
            {
                {
                    LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell
                }
            }, new Point(0, 0));
            labyrinth.PortalPositions = new List<Point>();
            gameModel.StartGame(labyrinth);
            
            gameModel.KeyDown(Keys.Right);
            
            var expectedPoint = new Point(1, 0);
            var actualPoint = new Point(gameModel.Player.X, gameModel.Player.Y);
            
            Assert.AreEqual(expectedPoint, actualPoint);
        }
        
        [Test]
        public void TestPlayerWallCollision()
        {
            var gameModel = new GameModel();
            var labyrinth = new LabyrinthModel(new[,]
            {
                {
                    LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Wall
                }
            }, new Point(0, 0));
            labyrinth.PortalPositions = new List<Point>();
            labyrinth.ExitPosition = new Point(-1, -1);
            labyrinth.KeyPosition = new Point(-1, -1);
            gameModel.StartGame(labyrinth);
            
            gameModel.KeyDown(Keys.Right);
            
            var expectedPoint = new Point(0, 0);
            var actualPoint = new Point(gameModel.Player.X, gameModel.Player.Y);
            
            Assert.AreEqual(expectedPoint, actualPoint);
        }
        
        [Test]
        public void TestPlayerClosedExitCollision()
        {
            var gameModel = new GameModel();
            var labyrinth = new LabyrinthModel(new[,]
            {
                {
                    LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell
                }
            }, new Point(0, 0));
            labyrinth.PortalPositions = new List<Point>();
            labyrinth.ExitPosition = new Point(1, 0);
            labyrinth.KeyPosition = new Point(-1, -1);
            gameModel.StartGame(labyrinth);
            
            gameModel.KeyDown(Keys.Right);
            
            var expectedPoint = new Point(0, 0);
            var actualPoint = new Point(gameModel.Player.X, gameModel.Player.Y);
            
            Assert.AreEqual(expectedPoint, actualPoint);
        }
        
        [Test]
        public void TestPlayerKeyPickup()
        {
            var gameModel = new GameModel();
            var labyrinth = new LabyrinthModel(new[,]
            {
                {
                    LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell
                }
            }, new Point(0, 0));
            labyrinth.PortalPositions = new List<Point>();
            labyrinth.ExitPosition = new Point(-1, 0);
            labyrinth.KeyPosition = new Point(1, 0);
            gameModel.StartGame(labyrinth);
            
            Assert.False(gameModel.Player.HasKey);
            
            gameModel.KeyDown(Keys.Right);
            
            var expectedPoint = new Point(1, 0);
            var actualPoint = new Point(gameModel.Player.X, gameModel.Player.Y);
            
            Assert.True(gameModel.Player.HasKey);
            Assert.AreEqual(expectedPoint, actualPoint);
        }
        
        [Test]
        public void TestPlayerGlassesPickup()
        {
            var gameModel = new GameModel();
            var labyrinth = new LabyrinthModel(new[,]
            {
                {
                    LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell
                }
            }, new Point(0, 0));
            labyrinth.PortalPositions = new List<Point>();
            labyrinth.ExitPosition = new Point(-1, 0);
            labyrinth.KeyPosition = new Point(-1, 0);
            labyrinth.GlassesPosition = new Point(1, 0);
            gameModel.StartGame(labyrinth);
            
            Assert.False(gameModel.Player.HasGlasses);
            
            gameModel.KeyDown(Keys.Right);
            
            var expectedPoint = new Point(1, 0);
            var actualPoint = new Point(gameModel.Player.X, gameModel.Player.Y);
            
            Assert.True(gameModel.Player.HasGlasses);
            Assert.AreEqual(expectedPoint, actualPoint);
        }
        
        [Test]
        public void TestWinCondition()
        {
            var gameModel = new GameModel();
            var labyrinth = new LabyrinthModel(new[,]
            {
                {
                    LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell
                }
            }, new Point(0, 0));
            labyrinth.PortalPositions = new List<Point>();
            labyrinth.ExitPosition = new Point(2, 0);
            labyrinth.KeyPosition = new Point(1, 0);
            labyrinth.GlassesPosition = new Point(-1, 0);
            gameModel.StartGame(labyrinth);
            
            Assert.False(gameModel.GameState == GameState.GameWon);
            
            gameModel.KeyDown(Keys.Right);
            gameModel.KeyDown(Keys.Right);

            Assert.True(gameModel.GameState == GameState.GameWon);
        }

        [Test]
        public void TestTeleport()
        {
            var gameModel = new GameModel();
            var labyrinth = new LabyrinthModel(new[,]
            {
                {
                    LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell
                }
            }, new Point(0, 0));
            labyrinth.PortalPositions = new List<Point> { new Point(1, 0) };
            labyrinth.TeleportPoints = new List<Point> { new Point(2, 0) };
            labyrinth.ExitPosition = new Point(-1, 0);
            labyrinth.KeyPosition = new Point(-1, 0);
            labyrinth.GlassesPosition = new Point(1, 0);
            gameModel.StartGame(labyrinth);
            
            Assert.False(gameModel.Player.HasGlasses);
            
            gameModel.KeyDown(Keys.Right);
            
            var expectedPoint = new Point(2, 0);
            var actualPoint = new Point(gameModel.Player.X, gameModel.Player.Y);
            
            Assert.AreEqual(expectedPoint, actualPoint);
        }
        
        [Test]
        public void TestDisarming()
        {
            var gameModel = new GameModel();
            var labyrinth = new LabyrinthModel(new[,]
            {
                {
                    LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell, LabyrinthModel.LabyrinthElements.Cell
                }
            }, new Point(0, 0));
            labyrinth.PortalPositions = new List<Point> { new Point(1, 0) };
            labyrinth.TeleportPoints = new List<Point> { new Point(2, 0) };
            labyrinth.ExitPosition = new Point(-1, 0);
            labyrinth.KeyPosition = new Point(-1, 0);
            labyrinth.GlassesPosition = new Point(1, 0);
            gameModel.StartGame(labyrinth);
            
            Assert.False(gameModel.Player.IsDisarming);
            
            gameModel.KeyDown(Keys.Space);
            
            Assert.True(gameModel.Player.IsDisarming);
        }
    }
}