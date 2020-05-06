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
        public Image Glasses { get; private set; }
        public Image Portal { get; private set; }

        private Image LoadImageFromAssets(string fileName) =>
            Image.FromFile("Assets/Textures/" + fileName);
        
        public void Load()
        {
            Player = LoadImageFromAssets("player.png");
            Wall = LoadImageFromAssets("wall.png");
            ClosedDoor = LoadImageFromAssets("door_closed.png");
            OpenedDoor = LoadImageFromAssets("door_open.png");
            Key = LoadImageFromAssets("key.jpg");
            Glasses = LoadImageFromAssets("glasses.png");
            Portal = LoadImageFromAssets("portal.png");
        }
    }
}