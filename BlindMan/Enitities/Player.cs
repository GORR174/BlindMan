namespace BlindMan.Enitities
{
    public class Player : Entity
    {
        private int speed = 120;

        public Player(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        
        public override void Update(float deltaTime)
        {
            X += speed * deltaTime;
        }
    }
}