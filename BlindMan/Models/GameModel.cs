using BlindMan.Enitities;

namespace BlindMan
{
    public class GameModel
    {
        public Player Player { get; private set; }

        public GameModel()
        {
            Player = new Player(0, 200, 240, 300);
        }
        
        public void Update(float deltaTime)
        {
            Player.Update(deltaTime);
        }
    }
}