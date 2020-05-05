using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace BlindMan.View
{
    public class Fonts
    {
        public Font Fixedsys24;
        
        private readonly PrivateFontCollection fontCollection = new PrivateFontCollection();

        public void Load()
        {
            Fixedsys24 = LoadFontFromFile("fixedsys.ttf", 24);
        }

        private Font LoadFontFromFile(string fileName, int fontSize)
        {
            fontCollection.AddFontFile("Assets/Fonts/" + fileName);
            return new Font(fontCollection.Families.Last(), fontSize);
        }
    }
}