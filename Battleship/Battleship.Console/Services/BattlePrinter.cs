using Battleship.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Console.Services
{
    internal class BattlePrinter : IObserver<IDictionary<Point, CellStateEnum>>
    {
        private const int HeaderPadding = 3;
        public void OnCompleted()
        {
            System.Console.WriteLine("Game Finished!");
        }

        public void OnError(Exception error)
        {
            System.Console.WriteLine(error.Message);
        }

        public void OnNext(IDictionary<Point, CellStateEnum> cells)
        {
            System.Console.Clear();
            var rows = cells.GroupBy(x => x.Key.X);
            PrintHeader(rows.First().Select(x => x.Key));
            foreach (var row in rows)
            {
                System.Console.Write(row.Key.ToString().PadRight(HeaderPadding));
                System.Console.WriteLine(row
                    .OrderBy(x => x.Key.Y)
                    .Aggregate(string.Empty,
                        (acc, cell) => $"{acc}[{PositionPrinter(cell.Value)}]"));
            }
        }

        private void PrintHeader(IEnumerable<Point> columns)
        {
            System.Console.WriteLine(columns
                .OrderBy(x => x.Y)
                .Aggregate(string.Empty.PadRight(HeaderPadding),
                    (acc, cell) => $"{acc} {YToColumnName(cell.Y)} "));
        }

        private Func<CellStateEnum, string> PositionPrinter = (state) => state switch
        {
            CellStateEnum.Untouched => " ",
            CellStateEnum.Sunk => "X",
            CellStateEnum.Missed => "0",
            CellStateEnum.Hit => "X",
        };

        private Func<int, char> YToColumnName = (y) => (char)(y + 64);
    }
}
