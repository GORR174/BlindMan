using System.Drawing;
using BlindMan.Enitities;

namespace BlindMan
{
    public static class GraphicsExt
    {
        public static void DrawEntity(this Graphics graphics, Image texture, Entity entity)
        {
            graphics.DrawImage(texture, entity.X, entity.Y, entity.Width, entity.Height);
        }
    }
}