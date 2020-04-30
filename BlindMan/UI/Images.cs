using System.Drawing;

namespace BlindMan
{
    public class Images
    {
        public Image Player { get; private set; }
        
        public void Load()
        {
            Player = Image.FromFile("Textures/player.png");
        }
    }
}