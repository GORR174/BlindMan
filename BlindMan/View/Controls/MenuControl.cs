using System.Windows.Forms;
using BlindMan.Domain;

namespace BlindMan.View.Controls
{
    public class MenuControl : BaseControl
    {
        public MenuControl(GameModel gameModel) : base(gameModel)
        {
            var startButton = new Button();
            startButton.Text = "Start";
            startButton.Click += (sender, args) => gameModel.GameState = GameState.Game;
            Controls.Add(startButton);

            SizeChanged += (sender, args) =>
            {
                startButton.Left = (ClientSize.Width - startButton.Width) / 2;
                startButton.Top = (ClientSize.Height - startButton.Height) / 2;
            };
        }
    }
}