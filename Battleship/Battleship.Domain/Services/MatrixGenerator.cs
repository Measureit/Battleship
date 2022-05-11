using System.Collections.Generic;
using System.Drawing;

namespace Battleship.Domain.Services
{
    public interface IMatrixGenerator
    {
        public IEnumerable<Point> GenerateMatrix(int rows, int columns);
    }

    public class MatrixGenerator : IMatrixGenerator
    {
        public IEnumerable<Point> GenerateMatrix(int rows, int columns)
        {
            for (int x = 1; x < rows + 1; x++)
            {
                for (int y = 1; y < columns + 1; y++)
                    yield return new Point(x, y);
            }
        }
    }
}
