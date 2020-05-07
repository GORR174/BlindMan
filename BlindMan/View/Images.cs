using System.Drawing;

namespace BlindMan.View
{
    public class Images
    {
        public Image Player { get; }
        public Image DoorClosed { get; }
        public Image DoorOpen { get; }
        public Image Key { get; }
        public Image Glasses { get; }
        public Image Portal { get; }

        private Image LoadImageFromAssets(string fileName) =>
            Image.FromFile("Assets/Textures/" + fileName);

        public Images()
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