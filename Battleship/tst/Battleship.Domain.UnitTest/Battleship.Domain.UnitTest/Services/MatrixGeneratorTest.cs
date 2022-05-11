using System.Collections.Generic;
using System.Drawing;
using Battleship.Domain.Services;
using Xunit;

namespace Battleship.Domain.UnitTest.Services
{
    public class MatrixGeneratorTest 
    {
        public static IEnumerable<object[]> Matrix =>
              new List<object[]>
              {
                new object[] {
                    1,
                    1,
                    new List<Point>() {  new(1, 1) }
                },
                new object[] {
                    2,
                    2,
                    new List<Point>() {  new(1, 1), new(1, 2), new(2, 1), new(2, 2) }
                },
              };

        [Theory]
        [MemberData(nameof(Matrix))]
        public void MatrixGenerator_GenerateMatrix(int rows, int colums, IList<Point> expected)
        {
            // Arrange
            var matrixGenerator = new MatrixGenerator();

            // Act
            var result = matrixGenerator.GenerateMatrix(rows, colums);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
