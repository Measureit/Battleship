using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Battleship.Domain.UnitTest
{
    public class ShipTest
    {
        public static IEnumerable<object[]> Positions =>
            new List<object[]>
            {
                new object[] { new List<Point>() {  new(1, 2), new(1, 3), new(1, 4) } },
                new object[] { new List<Point>() {  new(1, 2), new(4, 3), new(1, 4) } },
                new object[] { new List<Point>() {  new(2, 2) } }
            };

        [Theory]
        [MemberData(nameof(Positions))]
        public void Ship_ShotAllElements_ShipIsSunk(List<Point> positions)
        {
            // Arrange
            var ship = new Ship(positions);

            // Act
            foreach (var position in positions)
            {
                ship.Shot(position);
            }

            // Assert
            Assert.True(ship.IsSunk);
        }

        [Theory]
        [MemberData(nameof(Positions))]
        public void Ship_NoShot_ShipIsNotSunk(List<Point> positions)
        {
            // Arrange
            var ship = new Ship(positions);

            // Act

            // Assert
            Assert.False(ship.IsSunk);
        }

        [Fact]
        public void Ship_WithoutPosition_ShipIsSunk()
        {
            // Arrange
            var ship = new Ship(new List<Point>());

            // Act

            // Assert
            Assert.True(ship.IsSunk);
        }
    }
}