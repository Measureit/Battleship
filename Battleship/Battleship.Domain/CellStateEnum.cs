using System.Drawing;

namespace Battleship.Domain
{
    public enum CellStateEnum
    {
        Untouched = 0,
        Sunk = 1,
        Missed = 2,
        Hit = 3,
        Unknown = 4
    }
}