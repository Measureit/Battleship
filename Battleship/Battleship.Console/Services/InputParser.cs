using System.Drawing;
using System.Linq;

namespace Battleship.Console.Services
{
    internal class CoordinateParser
    {
        public Point Parse(string line)
        {
            var trimedLine = line.Trim();
            int y = char.ToUpper(trimedLine.First()) - 64;
            return int.TryParse(trimedLine.Substring(1), out int x) ? new Point(x, y) : new Point(-1, y);
        }
    }
}
