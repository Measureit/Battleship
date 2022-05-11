using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleship.Domain
{
    public class Ship
    {
        public IEnumerable<Point> Positions => _positions.Keys.AsEnumerable();
        private readonly IDictionary<Point, bool> _positions;
        public Ship(List<Point> positions)
        {
            _positions = positions.ToDictionary(point => point, point => false);
        }

        public void Shot(Point shot) => _positions[shot] = true;

        public bool IsSunk => _positions.Values.All(x => x);

    }
}
