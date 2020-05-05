using System.Drawing;
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
        
        public Player(int x, int y, int width, int height, GameModel gameModel) : base(gameModel)
        {
            X = x * width;
            Y = y * height;
            Width = width;
            Height = height;
            Vision = 8;
            HasKey = false;
            HasGlasses = false;

            this.gameModel = gameModel;

            labyrinthPosition.X = x;
            labyrinthPosition.Y = y;

            labyrinth = gameModel.Labyrinth;
            
            gameModel.LeftKeyDown += () => Move(new Point(-1, 0));
            gameModel.RightKeyDown += () => Move(new Point(1, 0));
            gameModel.UpKeyDown += () => Move(new Point(0, -1));
            gameModel.DownKeyDown += () => Move(new Point(0, 1));
        }

        private void Move(Point vector)
        {
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
        }
        
        public override void Update(float deltaTime)
        {
            
        }
    }
}