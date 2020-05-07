using System.Drawing;

namespace BlindMan.View
{
    public class Images
    {
        public Image Player { get; private set; }
        public Image DoorClosed { get; private set; }
        public Image DoorOpen { get; private set; }
        public Image Key { get; private set; }
        public Image Glasses { get; private set; }
        public Image Portal { get; private set; }

        private Image LoadImageFromAssets(string fileName) =>
            Image.FromFile("Assets/Textures/" + fileName);
        
        public void Load()
        {
            Player = LoadImageFromAssets("player.png");
            DoorClosed = LoadImageFromAssets("door_closed.png");
            DoorOpen = LoadImageFromAssets("door_open.png");
            Key = LoadImageFromAssets("key.png");
            Glasses = LoadImageFromAssets("glasses.png");
            Portal = LoadImageFromAssets("portal.png");
        }
    }
}