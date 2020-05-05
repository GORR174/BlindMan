using BlindMan.Domain;

namespace BlindMan.Entities
{
    public abstract class Entity
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Entity(GameModel gameModel)
        {
            
        }

        public virtual void Update(float deltaTime)
        {
            
        }
    }
}