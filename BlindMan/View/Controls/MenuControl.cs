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
            
            var tutorialButton = new Button();
            tutorialButton.Text = "Open Tutorial";
            tutorialButton.AutoSize = true;
            tutorialButton.Click += (sender, args) => gameModel.GameState = GameState.Tutorial;
            Controls.Add(tutorialButton);

            SizeChanged += (sender, args) =>
            {
                startButton.Left = (ClientSize.Width - startButton.Width) / 2;
                startButton.Top = (ClientSize.Height - startButton.Height) / 2;
                
                tutorialButton.Left = (ClientSize.Width - tutorialButton.Width) / 2;
                tutorialButton.Top = startButton.Bottom + 20;
            };
        }
    }
}