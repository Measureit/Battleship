using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Battleship.Domain.Services;
using Xunit;

namespace Battleship.Domain.UnitTest.Services
{
    public class ShipGeneratorTest
    {
        public static IEnumerable<object[]> Matrix =>
            new List<object[]>
            {
                new object[] {
                    1,
                    1,
                    new List<int>() { 1 }
                },
                new object[] {
                    2,
                    2,
                    new List<int>() { 2 }
                },
                new object[] {
                    2,
                    2,
                    new List<int>() { 2 }
                },
                new object[] {
                    10,
                    10,
                    new List<int>() { 4,5,5 }
                },
            };

        [Theory]
        [MemberData(nameof(Matrix))]
        public void ShipsGenerator_GenerateShips(int rows, int columns, IList<int> ships)
        {
            // Arrange
            var shipsGenerator = new ShipsGenerator();

            // Act
            var result = shipsGenerator.GenerateShips(rows, columns, ships);

            // Assert
            // Are Unique
            Assert.Equal(result.SelectMany(x => x.Positions).Count(), result.SelectMany(x => x.Positions).Distinct().Count());
            // Points in range
            Assert.True(result.SelectMany(s => s.Positions).All(p => p.X <= rows && p.Y <= columns));
        }
    }
}
