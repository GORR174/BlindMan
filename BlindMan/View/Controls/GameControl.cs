﻿using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BlindMan
{
    public class GameControl : BaseControl
    {
        Images images = new Images();
        private int fps;

        public GameControl(GameModel gameModel) : base(gameModel)
        {
            images.Load();

            StartGameUpdaterTimer(gameModel);
            StartFpsCounterTimer();
        }

        private void Draw(Graphics graphics)
        {
            graphics.DrawEntity(images.Player, gameModel.Player);
        }

        private void StartGameUpdaterTimer(GameModel model)
        {
            var deltaTimeWatch = new Stopwatch();
            deltaTimeWatch.Start();
            var updateTimer = new Timer();
            updateTimer.Interval = 1000 / GameSettings.GameTPS;
            updateTimer.Tick += (sender, args) =>
            {
                var deltaTime = (float) deltaTimeWatch.Elapsed.TotalSeconds;
                model.Update(deltaTime);
                deltaTimeWatch.Restart();
                Invalidate();
            };
            updateTimer.Start();
        }

        private void StartFpsCounterTimer()
        {
            var fpsTimer = new Timer();
            fpsTimer.Interval = 1000;
            fpsTimer.Tick += (sender, args) =>
            {
                Parent.Text = GameSettings.GameName + "   [FPS: " + fps + "]";
                fps = 0;
            };
            fpsTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            fps++;
            var graphics = e.Graphics;

            var graphicsState = graphics.Save();
            
            var widthRatio = ClientSize.Width / (float) GameSettings.GameWidth;
            var heightRatio = ClientSize.Height / (float) GameSettings.GameHeight;
            
            graphics.ScaleTransform(widthRatio, heightRatio);
            
            Draw(graphics);
            
            graphics.Restore(graphicsState);
        }
    }
}