using System.Drawing;
using BlindMan.Domain;

namespace BlindMan.Entities
{
    public class Player : Entity
    {
        public Point labyrinthPosition;
        private LabyrinthModel labyrinth;
        public int Vision { get; }
        
        public Player(int x, int y, int width, int height, GameModel gameModel) : base(gameModel)
        {
            X = x * width;
            Y = y * height;
            Width = width;
            Height = height;
            Vision = 12;

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

            if (labyrinth.Labyrinth[labyrinthPosition.Y, labyrinthPosition.X] == LabyrinthModel.LabyrinthElements.WALL)
            {
                Move(new Point(-vector.X, -vector.Y));
            }
        }
        
        public override void Update(float deltaTime)
        {
            
        }
    }
}