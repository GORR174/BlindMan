using System.Diagnostics;
using System.Windows.Forms;

namespace BlindMan
{
    public class GameControl : BaseControl
    {
        Images images = new Images();

        public GameControl(GameModel gameModel) : base(gameModel)
        {
            images.Load();

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