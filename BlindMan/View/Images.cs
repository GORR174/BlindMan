using System.Drawing;

namespace BlindMan.View
{
    public class Images
    {
        public Image Player { get; private set; }
        public Image Wall { get; private set; }
        public Image ClosedDoor { get; private set; }
        public Image OpenedDoor { get; private set; }
        public Image Key { get; private set; }

        private Image LoadImageFromAssets(string fileName) =>
            Image.FromFile("Assets/Textures/" + fileName);
        
        public void Load()
        {
            Player = LoadImageFromAssets("player.png");
            Wall = LoadImageFromAssets("wall.png");
            ClosedDoor = LoadImageFromAssets("closed_door.png");
            OpenedDoor = LoadImageFromAssets("opened_door.png");
            Key = LoadImageFromAssets("key.jpg");
        }
    }
}