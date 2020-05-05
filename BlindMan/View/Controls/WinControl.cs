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
            fonts.Load();
            var retryButton = new Button();
            retryButton.Text = "Retry";
            retryButton.Click += (sender, args) => gameModel.GameState = GameState.Game;
            Controls.Add(retryButton);
            
            var timeLabel = new Label();
            timeLabel.Text = "You win!\n\n Your time is:\n" + gameModel.GameTime;
            timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            timeLabel.AutoSize = false;
            timeLabel.Size = new Size(1280, 200);
            timeLabel.Font = fonts.Fixedsys24;
            Controls.Add(timeLabel);

            SizeChanged += (sender, args) =>
            {
                timeLabel.Left = (ClientSize.Width - timeLabel.Width) / 2;
                timeLabel.Top = (ClientSize.Height - timeLabel.Height) / 2 - 64;
                retryButton.Left = (ClientSize.Width - retryButton.Width) / 2;
                retryButton.Top = timeLabel.Bottom + 32;
            };
        }
    }
}