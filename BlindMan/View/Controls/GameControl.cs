using System;
using System.Drawing;
using System.Windows.Forms;
using BlindMan.Domain;

namespace BlindMan.View.Controls
{
    public class GameControl : BaseControl
    {
        Images images = new Images();
        Fonts fonts = new Fonts();
        private int fps;
        private Timer fpsTimer;
        private Timer updateTimer;

        private const int TileSize = 40;
        
        public GameControl(GameModel gameModel) : base(gameModel)
        {
            images.Load();
            fonts.Load();

            gameModel.StartGame();
            StartGameUpdaterTimer(gameModel);
            StartFpsCounterTimer();
            
            BackColor = Color.Black;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            gameModel.KeyDown(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Draw(Graphics graphics)
        {
            var lab = gameModel.Labyrinth;
            
            for (var i = 0; i < lab.Height; i++)
            {
                for (var j = 0; j < lab.Width; j++)
                {
                    var tilePosition = new Point(j, i);
                    if (IsVisibleByPlayer(tilePosition))
                    {
                        if (lab.Labyrinth[i, j] == LabyrinthModel.LabyrinthElements.Wall)
                            DrawTileRectangle(graphics, Brushes.MidnightBlue, tilePosition);
                        else
                            DrawTileRectangle(graphics, Brushes.Beige, tilePosition);
                    }
                }
            }
            
            lab.PortalPositions.ForEach(portalPosition =>
            {
                if (IsVisibleByPlayer(portalPosition))
                    DrawTile(graphics, images.Portal, portalPosition);
            });

            if (IsVisibleByPlayer(lab.ExitPosition))
            {
                if (!gameModel.Player.HasKey)
                    DrawTile(graphics, images.DoorClosed, lab.ExitPosition);
                else
                    DrawTile(graphics, images.DoorOpen, lab.ExitPosition);
            }

            if (IsVisibleByPlayer(lab.KeyPosition) && !gameModel.Player.HasKey)
                DrawTile(graphics, images.Key, lab.KeyPosition);
            if (IsVisibleByPlayer(lab.GlassesPosition) && !gameModel.Player.HasGlasses)
                DrawTile(graphics, images.Glasses, lab.GlassesPosition);
            
            DrawTile(graphics, images.Player, new Point(gameModel.Player.X, gameModel.Player.Y));

            if (gameModel.Player.IsDisarming)
                graphics.DrawString("Disarming...", fonts.Fixedsys14, Brushes.Crimson, gameModel.Player.X * 40 - 30,
                    gameModel.Player.Y * 40 + 40);
            
            graphics.DrawString(gameModel.GameTime, fonts.Fixedsys24, Brushes.White, GameSettings.GameWidth / 2f, 2, new StringFormat()
            {
                Alignment = StringAlignment.Center
            });
        }

        private void DrawTile(Graphics graphics, Image image, Point position)
        {
            graphics.DrawImage(image, position.X * TileSize, position.Y * TileSize, TileSize, TileSize);
        }
        
        private void DrawTileRectangle(Graphics graphics, Brush brush, Point position)
        {
            graphics.FillRectangle(brush, position.X * TileSize, position.Y * TileSize, TileSize, TileSize);
        }

        private bool IsVisibleByPlayer(Point objectPosition)
        {
            var player = gameModel.Player;
            return Math.Pow(Math.Abs(player.X - objectPosition.X), 2) +
                Math.Pow(Math.Abs(player.Y - objectPosition.Y), 2) < player.Vision;
        }

        private void StartGameUpdaterTimer(GameModel model)
        {
            updateTimer = new Timer();
            updateTimer.Interval = 1000 / GameSettings.GameTPS;
            updateTimer.Tick += (sender, args) => Invalidate();
            updateTimer.Start();
        }

        private void StartFpsCounterTimer()
        {
            fpsTimer = new Timer();
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

        protected override void Dispose(bool disposing)
        {
            fpsTimer.Stop();
            fpsTimer.Dispose();
            updateTimer.Stop();
            updateTimer.Dispose();
            base.Dispose(disposing);
        }
    }
}