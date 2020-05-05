using System.Drawing;
using BlindMan.Domain;

namespace BlindMan.Entities
{
    public class Player : Entity
    {
        public Player(int x, int y, int width, int height, GameModel gameModel) : base(gameModel)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            gameModel.LeftKeyDown += () => Move(new Point(-1, 0));
            gameModel.RightKeyDown += () => Move(new Point(1, 0));
            gameModel.UpKeyDown += () => Move(new Point(0, -1));
            gameModel.DownKeyDown += () => Move(new Point(0, 1));
        }

        private void Move(Point vector)
        {
            X += vector.X * Width;
            Y += vector.Y * Height;
        }
        
        public override void Update(float deltaTime)
        {
            
        }
    }
}