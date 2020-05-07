using System.Drawing;
using System.Windows.Forms;
using BlindMan.Domain;

namespace BlindMan.View.Controls
{
    public class WinControl : BaseControl
    {
        private Fonts fonts = new Fonts();
        public WinControl(GameModel gameModel) : base(gameModel)
        {
            BackColor = Color.Black;

            var timeLabel = new Label();
            timeLabel.Text = "You win!\n\n Your time is:\n" + gameModel.GameTime;
            timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            timeLabel.AutoSize = false;
            timeLabel.ForeColor = Color.White;
            timeLabel.Size = new Size(1280, 200);
            timeLabel.Font = fonts.Fixedsys24;
            Controls.Add(timeLabel);
            
            var retryButton = new FlatButton("   Retry   ", fonts.Fixedsys24);
            retryButton.Click += (sender, args) => gameModel.GameState = GameState.Game;
            Controls.Add(retryButton);
            
            var menuButton = new FlatButton("   Menu   ", fonts.Fixedsys24);
            menuButton.Click += (sender, args) => gameModel.GameState = GameState.Menu;
            Controls.Add(menuButton);

            SizeChanged += (sender, args) =>
            {
                timeLabel.Left = (ClientSize.Width - timeLabel.Width) / 2;
                timeLabel.Top = (ClientSize.Height - timeLabel.Height) / 2 - 64;
                
                retryButton.Left = (ClientSize.Width - retryButton.Width) / 2;
                retryButton.Top = timeLabel.Bottom + 32;
                
                menuButton.Left = (ClientSize.Width - menuButton.Width) / 2;
                menuButton.Top = retryButton.Bottom + 32;
            };
        }
    }
}