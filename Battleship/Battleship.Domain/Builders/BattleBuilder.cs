using Battleship.Domain.Services;
using System.Collections.Generic;

namespace Battleship.Domain.Builders
{
    public class BattleBuilder
    {
        private int _rows = 10;
        private int _columns = 10;
        private readonly ICollection<int> _ships = new List<int>();
        private readonly IMatrixGenerator _matrixGenerator = new MatrixGenerator();
        private readonly IShipsGenerator _shipsGenerator = new ShipsGenerator();
        public IBattle Build()
            => new Battle(
                _matrixGenerator.GenerateMatrix(_rows, _columns), 
                _shipsGenerator.GenerateShips(_rows, _columns, _ships));

        public BattleBuilder WithRows(int rows)
        {
            _rows = rows;
            return this;
        }

        public BattleBuilder WithColumns(int columns)
        {
            _columns = columns;
            return this;
        }

        public BattleBuilder AddShip(int size)
        {
            _ships.Add(size);
            return this;
        }
    }
}
