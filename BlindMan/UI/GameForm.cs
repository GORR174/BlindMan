using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BlindMan
{
    public class GameForm : Form
    {
        Images images = new Images();
        private GameModel gameModel;
        
        public GameForm(GameModel gameModel)
        {
            this.gameModel = gameModel;
            images.Load();
            DoubleBuffered = true;
            Text = GameSettings.GameName;
            Size = new Size(GameSettings.GameWidth, GameSettings.GameHeight);
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var timer = new Timer();
            timer.Interval = 1000 / GameSettings.GameTPS;
            timer.Tick += (sender, args) =>
            {
                var deltaTime = (float) stopwatch.Elapsed.TotalSeconds;
                gameModel.Update(deltaTime);
                stopwatch.Restart();
                Invalidate();
            };
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            graphics.DrawEntity(images.Player, gameModel.Player);
        }
    }
}