using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleship.Domain.Services
{
    public interface IShipsGenerator
    {
        IEnumerable<Ship> GenerateShips(int rows, int columns, IEnumerable<int> ships);
    }

    public class ShipsGenerator : IShipsGenerator
    {
        private readonly Random random = new Random();

        private IEnumerable<Point> GenerateShipPoints(int rows, int columns, int size)
        {
            int direction = random.Next(1, 3);
            int row = random.Next(1, rows );
            int col = random.Next(1, columns);

            if (direction % 2 != 0)
            {
                for (int i = 0; i < size; i++)
                {
                    yield return new Point(row - size > 0 ? row - i : row + i, col);
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    yield return new Point(row, col - size > 0 ? col - i : col + i);
                }
            }
        }

        public IEnumerable<Ship> GenerateShips(int rows, int columns, IEnumerable<int> ships)
        {
            var result = new List<Ship>(ships.Count());
            foreach (var size in ships)
            {
                IEnumerable<Point> positions;
                do
                {
                    positions = GenerateShipPoints(rows, columns, size).ToList();
                } while (result.SelectMany(x => x.Positions).Intersect(positions).Any());

                result.Add(new Ship(positions));
            }
            return result;
        }
    }
}