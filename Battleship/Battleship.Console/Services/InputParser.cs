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
            var x = int.Parse(trimedLine.Substring(1));
            return new Point(x, y);
        }
    }
}
