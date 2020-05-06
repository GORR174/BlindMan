namespace BlindMan.Domain
{
    public abstract class Entity
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public Entity(GameModel gameModel)
        {
            
        }
    }
}