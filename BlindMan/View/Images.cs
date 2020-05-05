using System.Drawing;

namespace BlindMan.View
{
    public class Images
    {
        public Image Player { get; private set; }
        public Image Wall { get; private set; }
        public Image ClosedDoor { get; private set; }

        private Image LoadImageFromAssets(string fileName) =>
            Image.FromFile("Assets/Textures/" + fileName);
        
        public void Load()
        {
            Player = LoadImageFromAssets("player.png");
            Wall = LoadImageFromAssets("wall.png");
            ClosedDoor = LoadImageFromAssets("closed_door.png");
        }
    }
}