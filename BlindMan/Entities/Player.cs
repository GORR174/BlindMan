using System;
using System.Drawing;
using System.Windows.Forms;
using BlindMan.Domain;

namespace BlindMan.Entities
{
    public class Player : Entity
    {
        public Point labyrinthPosition;
        private LabyrinthModel labyrinth;
        private GameModel gameModel;
        public int Vision { get; private set; }
        public bool HasKey { get; private set; }
        public bool HasGlasses { get; private set; }
        public bool IsDisarming { get; private set; }
        private readonly Timer DisarmTimer = new Timer();
        
        private readonly Random random = new Random();
        private const int DisarmInterval = 1500;

        public Player(int x, int y, int width, int height, GameModel gameModel) : base(gameModel)
        {
            X = x * width;
            Y = y * height;
            Width = width;
            Height = height;
            Vision = 8;
            HasKey = false;
            HasGlasses = false;
            IsDisarming = false;

            this.gameModel = gameModel;

            labyrinthPosition.X = x;
            labyrinthPosition.Y = y;

            labyrinth = gameModel.Labyrinth;
            
            gameModel.LeftKeyDown += () => Move(new Point(-1, 0));
            gameModel.RightKeyDown += () => Move(new Point(1, 0));
            gameModel.UpKeyDown += () => Move(new Point(0, -1));
            gameModel.DownKeyDown += () => Move(new Point(0, 1));

            DisarmTimer.Interval = DisarmInterval;
            DisarmTimer.Tick += (sender, args) =>
            {
                IsDisarming = false;
                DisarmNeighbourCells();
                DisarmTimer.Stop();
            };

            gameModel.SpaceKeyDown += () =>
            {
                IsDisarming = true;
                DisarmTimer.Start();
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
                        var neighbourX = labyrinthPosition.X + j;
                        var neighbourY = labyrinthPosition.Y + i;
                        
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
            
            X += vector.X * Width;
            Y += vector.Y * Height;
            
            labyrinthPosition.X = (int) (X / Width);
            labyrinthPosition.Y = (int) (Y / Height);

            if (labyrinth.Labyrinth[labyrinthPosition.Y, labyrinthPosition.X] == LabyrinthModel.LabyrinthElements.WALL 
                || (!HasKey && labyrinth.ExitPosition == labyrinthPosition))
            {
                Move(new Point(-vector.X, -vector.Y));
            }

            if (HasKey && labyrinth.ExitPosition == labyrinthPosition)
                gameModel.EndGame();

            if (!HasKey && labyrinthPosition == labyrinth.KeyPosition)
                HasKey = true;
            
            if (!HasGlasses && labyrinthPosition == labyrinth.GlassesPosition)
            {
                HasGlasses = true;
                Vision += 16;
            }
            
            if (labyrinth.PortalPositions.Contains(labyrinthPosition))
            {
                labyrinth.PortalPositions.Remove(labyrinthPosition);
                var rndPosition = labyrinth.TeleportPoints[random.Next(labyrinth.TeleportPoints.Count)];
                
                X = rndPosition.X * Width;
                Y = rndPosition.Y * Height;
            
                labyrinthPosition.X = (int) (X / Width);
                labyrinthPosition.Y = (int) (Y / Height);
            }
        }
        
        public override void Update(float deltaTime)
        {
            
        }
    }
}