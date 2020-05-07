using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace BlindMan.View
{
    public class Fonts
    {
        public Font Fixedsys36 { get; }
        public Font Fixedsys24 { get; }
        public Font Fixedsys14 { get; }
        
        private readonly PrivateFontCollection fontCollection = new PrivateFontCollection();

        public Fonts()
        {
            Fixedsys36 = LoadFontFromFile("fixedsys.ttf", 36);
            Fixedsys24 = LoadFontFromFile("fixedsys.ttf", 24);
            Fixedsys14 = LoadFontFromFile("fixedsys.ttf", 14);
        }

        private Font LoadFontFromFile(string fileName, int fontSize)
        {
            fontCollection.AddFontFile("Assets/Fonts/" + fileName);
            return new Font(fontCollection.Families.Last(), fontSize);
        }
    }
}