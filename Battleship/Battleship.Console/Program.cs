using Battleship.Console.Services;
using Battleship.Domain.Builders;
using System;
using System.Linq;
using System.Reactive.Linq;

var parser = new CoordinateParser();

var input = Observable
    .FromAsync(Console.In.ReadLineAsync)
    .Select(parser.Parse)
    .Repeat()
    .Publish()
    .RefCount();

var printer = new BattlePrinter();
new BattleBuilder()
    .WithRows(10)
    .WithColumns(10)
    .AddShip(5) // Battleship
    .AddShip(4) // 1 x Destroyers
    .AddShip(4) // 1 x Destroyers
    .Build()
    .Start(input)
    .Subscribe(new BattlePrinter());