using System.Drawing;

namespace BlindMan.Domain
{
    public class LabyrinthModel
    {
        public LabyrinthElements[,] Labyrinth { get; }

        public int Width => Labyrinth.GetLength(1);
        public int Height => Labyrinth.GetLength(0);
        
        public Point PlayerPosition { get; }

        public LabyrinthModel(LabyrinthElements[,] labyrinth, Point playerPosition)
        {
            Labyrinth = labyrinth;
            PlayerPosition = playerPosition;
        }

        public enum LabyrinthElements
        {
            WALL,
            CELL
        }
    }
}