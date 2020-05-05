namespace BlindMan.Domain
{
    public class LabyrinthModel
    {
        public LabyrinthElements[,] Labyrinth { get; }

        public int Width => Labyrinth.GetLength(1);
        public int Height => Labyrinth.GetLength(0);

        public LabyrinthModel(LabyrinthElements[,] labyrinth)
        {
            Labyrinth = labyrinth;
        }

        public enum LabyrinthElements
        {
            WALL,
            CELL
        }
    }
}