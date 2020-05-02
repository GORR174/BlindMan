using System.Windows.Forms;

namespace BlindMan
{
    public class MenuControl : BaseControl
    {
        public MenuControl(GameModel gameModel) : base(gameModel)
        {
            var startButton = new Button();
            startButton.Text = "Start";
            startButton.Left = (ClientSize.Width - startButton.Width) / 2;
            startButton.Top = (ClientSize.Height - startButton.Height) / 2;
            startButton.Click += (sender, args) => gameModel.GameState = GameState.Game;
            Controls.Add(startButton);
        }
    }
}