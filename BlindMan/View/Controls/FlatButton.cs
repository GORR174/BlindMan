using System.Drawing;
using System.Windows.Forms;

namespace BlindMan.View.Controls
{
    public class FlatButton : Button
    {
        public FlatButton(string text, Font font)
        {
            Text = text;
            Font = font;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.MouseOverBackColor = Color.FromArgb(100, 255, 255, 255);
            ForeColor = Color.White;
            BackColor = Color.Transparent;
            AutoSize = true;
        }
    }
}