using Microsoft.Reactive.Testing;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Xunit;

namespace Battleship.Domain.UnitTest
{
    public class BattleTest
    {
        public static IEnumerable<object[]> Matrix =>
            new List<object[]>
            {
                new object[] {
                    new List<Point>() {  new(1, 1), new(1, 2), new(2, 1), new(2, 2) }
                },
            };

        [Theory]
        [MemberData(nameof(Matrix))]
        public void Battle_OnStart_EmitEmptyMatrix(List<Point> matrix)
        {
            // Arrange
            var ships = new List<Ship>() { new Ship(new List<Point>() { new(1, 1) }) };
            var battle = new Battle(matrix, ships);
            var scheduler = new TestScheduler();
            var observer = scheduler.CreateObserver<IDictionary<Point, CellStateEnum>>();
            var input = new Subject<Point>();

            // Act
            battle
                .Start(input)
                .Subscribe(observer);

            // Assert
            Assert.Equal(observer.Messages.Count, 1);
        }

        [Theory]
        [MemberData(nameof(Matrix))]
        public void Battle_FireOnTarget_ShipSunk(List<Point> matrix)
        {
            // Arrange
            var shipPosition = new Point(1, 1);
            var ship = new Ship(new List<Point>() { shipPosition });
            var battle = new Battle(matrix, new List<Ship> { ship });
            var scheduler = new TestScheduler();
            var observer = scheduler.CreateObserver<IDictionary<Point, CellStateEnum>>();
            var input = new Subject<Point>();

            // Act
            battle
                .Start(input)
                .Subscribe(observer);

            input.OnNext(shipPosition);

            // Assert
            Assert.Equal(observer.Messages.Count, 2);
            Assert.Equal(observer
                .Messages
                .Last()
                .Value
                .Value[shipPosition], CellStateEnum.Sunk);
        }

        [Theory]
        [MemberData(nameof(Matrix))]
        public void Battle_FireOnTarget_ShipContinues(List<Point> matrix)
        {
            // Arrange
            var shipPositions = new List<Point>() { new(1, 1), new(1, 2) };
            var ship = new Ship(shipPositions);
            var battle = new Battle(matrix, new List<Ship> { ship });
            var scheduler = new TestScheduler();
            var observer = scheduler.CreateObserver<IDictionary<Point, CellStateEnum>>();
            var input = new Subject<Point>();

            // Act
            battle
                .Start(input)
                .Subscribe(observer);

            input.OnNext(shipPositions.First());

            // Assert
            Assert.Equal(observer.Messages.Count, 2);
            Assert.Equal(observer
                .Messages
                .Last()
                .Value
                .Value[shipPositions.First()], CellStateEnum.Hit);

            Assert.Equal(observer
                .Messages
                .Last()
                .Value
                .Value[shipPositions.Last()], CellStateEnum.Untouched);
        }

        [Theory]
        [MemberData(nameof(Matrix))]
        public void Battle_FireMissed_EmitMissedPoint(List<Point> matrix)
        {
            // Arrange
            var firePoint = new Point(2, 2);
            var shipPosition = new List<Point>() { new(1, 1)};
            var ship = new Ship(shipPosition);
            var battle = new Battle(matrix, new List<Ship> { ship });
            var scheduler = new TestScheduler();
            var observer = scheduler.CreateObserver<IDictionary<Point, CellStateEnum>>();
            var input = new Subject<Point>();

            // Act
            battle
                .Start(input)
                .Subscribe(observer);

            input.OnNext(new(2,2));

            // Assert
            Assert.Equal(observer.Messages.Count, 2);
            Assert.Equal(observer
                .Messages
                .Last()
                .Value
                .Value[firePoint], CellStateEnum.Missed);
        }
    }
}