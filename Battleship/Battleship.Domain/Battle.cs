using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;

namespace Battleship.Domain
{
    public interface IBattle
    {
        IObservable<IDictionary<Point, CellStateEnum>> Start(IObservable<Point> input);
    }

    public class Battle : IBattle
    {
        private readonly IDictionary<Point, CellStateEnum> _matrix;
        private readonly IEnumerable<Ship> _ships;
        public Battle(IEnumerable<Point> matrix, IEnumerable<Ship> ships)
        {
            _matrix = matrix.ToDictionary(x => x, y => CellStateEnum.Untouched);
            _ships = ships;
        }

        public IObservable<IDictionary<Point, CellStateEnum>> Start(IObservable<Point> input)
        {
            return
                Observable
                    .Return(_matrix)
                    .Concat(
                        input
                            .Where(shot => _matrix.ContainsKey(shot))
                            .Do(Fire)
                            .Select(shot => _matrix))
                    .TakeUntil(a => _ships.Any(x => x.IsSunk));

        }

        public void Fire(Point shot)
        {
            var ship = _ships.SingleOrDefault(ship => ship.Positions.Contains(shot));
            ship?.Shot(shot);
            _matrix[shot] = ship switch
            {
                null => CellStateEnum.Missed,
                var s when s.IsSunk => CellStateEnum.Sunk,
                var s when !s.IsSunk => CellStateEnum.Hit
            };
        }
    }
}
