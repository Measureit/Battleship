using System;
using System.Collections.Generic;
using System.Drawing;

namespace Battleship.Domain.Services
{
    public interface IShipsGenerator
    {
        IEnumerable<Ship> GenerateShips(int rows, int columns, IEnumerable<int> ships);
    }
    public class ShipsGenerator : IShipsGenerator
    {
        private readonly Random random = new Random();
        public IEnumerable<Ship> GenerateShips(int rows, int columns, IEnumerable<int> ships)
        {

            //int direction = random.Next(1, size);
            //foreach(var size in ships)
            //{
            //    int row = random.Next(1, rows);
            //    int col = random.Next(1, columns  );
            //}
            //    

            //    if (direction % 2 != 0)
            //    {
            //        //left first, then right
            //        if (row - size > 0)
            //        {
            //            for (int i = 0; i < size; i++)
            //            {
            //                Position pos = new Position();
            //                pos.x = row - i;
            //                pos.y = col;
            //                positions.Add(pos);
            //            }
            //        }
            //        else // row
            //        {
            //            for (int i = 0; i < size; i++)
            //            {
            //                Position pos = new Position();
            //                pos.x = row + i;
            //                pos.y = col;
            //                positions.Add(pos);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        //top first, then bottom
            //        if (col - size > 0)
            //        {
            //            for (int i = 0; i < size; i++)
            //            {
            //                Position pos = new Position();
            //                pos.x = row;
            //                pos.y = col - i;
            //                positions.Add(pos);
            //            }
            //        }
            //        else // row
            //        {
            //            for (int i = 0; i < size; i++)
            //            {
            //                Position pos = new Position();
            //                pos.x = row;
            //                pos.y = col + i;
            //                positions.Add(pos);
            //            }
            //        }
            //    }
            //    return positions;
            //}
            return new List<Ship>() { new Ship(new List<Point>() { new Point(1, 1) }) };
        }
    }
}
