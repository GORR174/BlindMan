using System.Drawing;
using System.Windows.Forms;
using BlindMan.Domain;

namespace BlindMan.View.Controls
{
    public class TutorialControl : BaseControl
    {
        public TutorialControl(GameModel gameModel) : base(gameModel)
        {
            BackgroundImage = Image.FromFile("Assets/Textures/tutorial.png");
            BackgroundImageLayout = ImageLayout.Stretch;

            var backButton = new PictureBox();
            backButton.Image = Image.FromFile("Assets/Textures/back_button.png");
            backButton.SizeMode = PictureBoxSizeMode.StretchImage;
            backButton.Width = 148;
            backButton.Height = 70;
            backButton.Left = 64;
            backButton.Click += (sender, args) => gameModel.GameState = GameState.Menu;
            Controls.Add(backButton);
            
            SizeChanged += (sender, args) => { backButton.Top = ClientSize.Height - 100; };
        }
    }
}