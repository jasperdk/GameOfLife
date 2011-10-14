using Game;
using NUnit.Framework;

namespace GameOfLife.Test
{
  [TestFixture]
  public class GameMapTest
  {
    [Test]
    [Ignore]
    public void CanCloneGameMap()
    {
      var gameMap = new GameMap(5, 10);

      var newGameMap = gameMap.Clone() as GameMap;

      Assert.That(newGameMap, Is.Not.EqualTo(gameMap));
      Assert.That(newGameMap, Is.Not.Null);
      Assert.That(newGameMap.RowCount, Is.EqualTo(5));
      Assert.That(newGameMap.ColumnCount, Is.EqualTo(10));
    }

    [Test]
    public void CanEnumerateGameMapWithNulls()
    {
      var gameMap = new GameMap(5, 10);

      foreach (var cell in gameMap)
      {
        Assert.That(cell, Is.Null);
      }
    }

    [Test]
    public void CanEnumerateGameMapWith()
    {
      var gameMap = new GameMap(5, 5);
      gameMap.Seed(GameOfLifeTest.CreateCells("     ",
        "*****",
        "     ",
        "     ",
        "*   *"));

      var cellcount = 0;
      foreach (var cell in gameMap)
      {
        Assert.That(cell, Is.Not.Null);
        cellcount++;
      }
      Assert.That(cellcount, Is.EqualTo(25));
    }

    [Test]
    public void CanSeedGameMap()
    {
      var gameMap = new GameMap(2, 2);
      gameMap.Seed(GameOfLifeTest.CreateCells(" *",
        "* "));

      Assert.That(gameMap.GetCell(1, 1).IsDead, Is.True);
      Assert.That(gameMap.GetCell(1, 2).IsAlive, Is.True);
      Assert.That(gameMap.GetCell(2, 1).IsAlive, Is.True);
      Assert.That(gameMap.GetCell(2, 2).IsDead, Is.True);
    }

    [Test]
    public void CanSeedGameMapFromAnotherGameMap()
    {
      var gameMap = new GameMap(2, 2);
      var newGameMap = new GameMap(2, 2);
      newGameMap.Seed(GameOfLifeTest.CreateCells(" *",
        "* "));

      gameMap.Seed(newGameMap);

      Assert.That(gameMap.GetCell(1, 1).IsDead, Is.True);
      Assert.That(gameMap.GetCell(1, 2).IsAlive, Is.True);
      Assert.That(gameMap.GetCell(2, 1).IsAlive, Is.True);
      Assert.That(gameMap.GetCell(2, 2).IsDead, Is.True);

    }
  }
}
