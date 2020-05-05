using System;
using System.Collections.Generic;
using System.Drawing;
using static BlindMan.Domain.LabyrinthModel;

namespace BlindMan.Domain
{
    public class LabyrinthGenerator
    {
        private LabyrinthElements[,] GenerateDefaultLabyrinth(int width, int height)
        {
            var labyrinthElements = new LabyrinthElements[height, width];
            for (var row = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++)
                {
                    if (row % 2 != 0 && column % 2 != 0 && row < height - 1 && column < width - 1)
                        labyrinthElements[row, column] = LabyrinthElements.CELL;
                    else
                        labyrinthElements[row, column] = LabyrinthElements.WALL;
                }
            }

            return labyrinthElements;
        }
        
        public LabyrinthModel CreateLabyrinth(int width, int height)
        {
            var labyrinthElements = GenerateDefaultLabyrinth(width, height);

            GenerateLabyrinth(width, height, labyrinthElements, new Point(1, 1));

            return new LabyrinthModel(labyrinthElements);
        }

        private static void GenerateLabyrinth(int width, int height, LabyrinthElements[,] labyrinthElements, Point startPoint)
        {
            Point? toOpen = startPoint;
            var visited = new HashSet<Point>();
            visited.Add(toOpen.Value);
            var stack = new Stack<Point>();
            var random = new Random();

            do
            {
                var neighbours = new List<Point>();

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((Math.Abs(i) + Math.Abs(j)) == 1)
                        {
                            var neighbourX = toOpen.Value.X + (j * 2);
                            var neighbourY = toOpen.Value.Y + (i * 2);
                            if (neighbourX > width - 1 || neighbourX < 0)
                                continue;
                            if (neighbourY > height - 1 || neighbourY < 0)
                                continue;
                            if (!visited.Contains(new Point(neighbourX, neighbourY)))
                                neighbours.Add(new Point(neighbourX, neighbourY));
                        }
                    }
                }

                if (neighbours.Count > 0)
                {
                    stack.Push(toOpen.Value);
                    var nextCell = neighbours[random.Next(neighbours.Count)];

                    var wallToRemoveX = (toOpen.Value.X + nextCell.X) / 2;
                    var wallToRemoveY = (toOpen.Value.Y + nextCell.Y) / 2;
                    labyrinthElements[wallToRemoveY, wallToRemoveX] = LabyrinthElements.CELL;

                    toOpen = nextCell;
                    visited.Add(toOpen.Value);
                }
                else if (stack.Count > 0)
                {
                    toOpen = stack.Pop();
                    visited.Add(toOpen.Value);
                }
                else
                {
                    toOpen = null;
                }
            } while (toOpen != null);
        }
    }
}