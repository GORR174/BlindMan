using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static BlindMan.Domain.LabyrinthModel;

namespace BlindMan.Domain
{
    public class LabyrinthGenerator
    {
        private HashSet<Point> possibleExits;
        private HashSet<Point> possibleKeys;
        
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
            possibleExits = new HashSet<Point>();
            possibleKeys = new HashSet<Point>();
            var labyrinthElements = GenerateDefaultLabyrinth(width, height);

            var playerPosition = GetRandomPlayerPosition(width, height);
            
            GenerateLabyrinth(width, height, labyrinthElements, playerPosition);

            var exitPosition = possibleExits.ToList()[new Random().Next(possibleExits.Count)];

            if (possibleKeys.Contains(exitPosition))
                possibleKeys.Remove(exitPosition);

            var keysToRemove = possibleKeys.Where(key => 
                    Math.Abs(key.X) + Math.Abs(playerPosition.X) < 6
                    && Math.Abs(key.Y) + Math.Abs(playerPosition.Y) < 6
                    && Math.Abs(key.X) + Math.Abs(exitPosition.X) < 3
                    && Math.Abs(key.Y) + Math.Abs(exitPosition.Y) < 3)
                .ToList();
            
            keysToRemove.ForEach(key => possibleKeys.Remove(key));
            
            var keyPosition = possibleKeys.ToList()[new Random().Next(possibleKeys.Count)];

            return new LabyrinthModel(labyrinthElements, playerPosition)
            {
                ExitPosition = exitPosition,
                KeyPosition = keyPosition
            };
        }

        private Point GetRandomPlayerPosition(int width, int height)
        {
            var possiblePlayerPositions = new List<Point>
            {
                new Point(1, 1),
                new Point(width - 2, 1),
                new Point(width - 2, height - 2),
                new Point(1, height - 2),
                new Point(width / 2, height / 2 - 1)
            };

            return possiblePlayerPositions[new Random().Next(possiblePlayerPositions.Count)];
        }

        private void GenerateLabyrinth(int width, int height, LabyrinthElements[,] labyrinthElements, Point startPoint)
        {
            Point? toOpen = startPoint;
            var visited = new HashSet<Point>();
            visited.Add(toOpen.Value);
            var stack = new Stack<Point>();
            var random = new Random();

            do
            {
                var neighbours = GetNeighbours(toOpen.Value, width, height, visited);

                if (neighbours.Count > 0)
                {
                    stack.Push(toOpen.Value);
                    var nextCell = neighbours[random.Next(neighbours.Count)];

                    var wallToRemoveX = (toOpen.Value.X + nextCell.X) / 2;
                    var wallToRemoveY = (toOpen.Value.Y + nextCell.Y) / 2;
                    labyrinthElements[wallToRemoveY, wallToRemoveX] = LabyrinthElements.CELL;

                    toOpen = nextCell;
                    visited.Add(toOpen.Value);
                    possibleKeys.Add(toOpen.Value);
                    if (GetNeighbours(toOpen.Value, width, height, visited).Count == 0)
                        possibleExits.Add(toOpen.Value);
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

        private List<Point> GetNeighbours(Point point, int width, int height, HashSet<Point> visited)
        {
            var neighbours = new List<Point>();

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((Math.Abs(i) + Math.Abs(j)) == 1)
                    {
                        var neighbourX = point.X + (j * 2);
                        var neighbourY = point.Y + (i * 2);
                        if (neighbourX > width - 1 || neighbourX < 0)
                            continue;
                        if (neighbourY > height - 1 || neighbourY < 0)
                            continue;
                        if (!visited.Contains(new Point(neighbourX, neighbourY)))
                            neighbours.Add(new Point(neighbourX, neighbourY));
                    }
                }
            }

            return neighbours;
        }
    }
}