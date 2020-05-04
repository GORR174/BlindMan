using System.Drawing;

namespace BlindMan
{
    public class Images
    {
        public Image Player { get; private set; }

        private Image LoadImageFromAssets(string fileName) =>
            Image.FromFile("Assets/Textures/" + fileName);
        
        public void Load()
        {
            Player = LoadImageFromAssets("player.png");
        }
    }
}