using System.Drawing;
using System.Windows.Forms;

namespace BlindMan
{
    public abstract class BaseControl : UserControl
    {
        protected GameModel gameModel;

        public BaseControl(GameModel gameModel)
        {
            Size = new Size(GameSettings.GameWidth, GameSettings.GameHeight);
            DoubleBuffered = true;
            this.gameModel = gameModel;
        }
    }
}