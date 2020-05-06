using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlindMan.Domain
{
    public class Player : Entity
    {
        private readonly LabyrinthModel labyrinth;
        private readonly GameModel gameModel;
        public int Vision { get; private set; }
        public bool HasKey { get; private set; }
        public bool HasGlasses { get; private set; }
        public bool IsDisarming { get; private set; }
        private readonly Timer disarmTimer = new Timer();
        
        private readonly Random random = new Random();
        private const int DisarmInterval = 1500;

        public Player(int x, int y, GameModel gameModel) : base(gameModel)
        {
            X = x;
            Y = y;
            Vision = 8;
            HasKey = false;
            HasGlasses = false;
            IsDisarming = false;

            this.gameModel = gameModel;

            labyrinth = gameModel.Labyrinth;
            
            gameModel.LeftKeyDown += () => Move(new Point(-1, 0));
            gameModel.RightKeyDown += () => Move(new Point(1, 0));
            gameModel.UpKeyDown += () => Move(new Point(0, -1));
            gameModel.DownKeyDown += () => Move(new Point(0, 1));

            disarmTimer.Interval = DisarmInterval;
            disarmTimer.Tick += (sender, args) =>
            {
                IsDisarming = false;
                DisarmNeighbourCells();
                disarmTimer.Stop();
            };

            gameModel.SpaceKeyDown += () =>
            {
                IsDisarming = true;
                disarmTimer.Start();
            };
        }

        private void DisarmNeighbourCells()
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (Math.Abs(i) + Math.Abs(j) == 1)
                    {
                        var neighbourX = X + j;
                        var neighbourY = Y + i;
                        
                        var neighbourPoint = new Point(neighbourX, neighbourY);

                        if (labyrinth.PortalPositions.Contains(neighbourPoint))
                            labyrinth.PortalPositions.Remove(neighbourPoint);
                    }
                }
            }
        }

        private void Move(Point vector)
        {
            if (IsDisarming)
                return;
            
            X += vector.X;
            Y += vector.Y;
            
            var playerPoint = new Point(X, Y);

            if (labyrinth.Labyrinth[Y, X] == LabyrinthModel.LabyrinthElements.Wall 
                || (!HasKey && labyrinth.ExitPosition == playerPoint))
            {
                Move(new Point(-vector.X, -vector.Y));
            }
            
            CheckCollisionWithItems();
        }

        private void CheckCollisionWithItems()
        {
            var playerPoint = new Point(X, Y);

            CheckCollisionWithExit(playerPoint);
            CheckCollisionWithKey(playerPoint);
            CheckCollisionWithGlasses(playerPoint);
            CheckCollisionWithPortals(playerPoint);
        }

        private void CheckCollisionWithExit(Point playerPoint)
        {
            if (HasKey && labyrinth.ExitPosition == playerPoint)
                gameModel.EndGame();
        }
        
        private void CheckCollisionWithKey(Point playerPoint)
        {
            if (!HasKey && playerPoint == labyrinth.KeyPosition)
                HasKey = true;
        }
        
        private void CheckCollisionWithGlasses(Point playerPoint)
        {
            if (!HasGlasses && playerPoint == labyrinth.GlassesPosition)
            {
                HasGlasses = true;
                Vision += 16;
            }
        }
        
        private void CheckCollisionWithPortals(Point playerPoint)
        {
            if (labyrinth.PortalPositions.Contains(playerPoint))
            {
                labyrinth.PortalPositions.Remove(playerPoint);
                var rndPosition = labyrinth.TeleportPoints[random.Next(labyrinth.TeleportPoints.Count)];
                
                X = rndPosition.X;
                Y = rndPosition.Y;
            }
        }
    }
}