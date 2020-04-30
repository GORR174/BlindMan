using System.Drawing;
using System.Windows.Forms;

namespace BlindMan
{
    public class GameForm : Form
    {
        public GameForm()
        {
            Text = GameSettings.GameName;
            Size = new Size(GameSettings.GameWidth, GameSettings.GameHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            graphics.DrawImage(Image.FromFile("Textures/player.png"), 10, 10);
        }
    }
}