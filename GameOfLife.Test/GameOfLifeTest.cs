using System;
using Game;
using NUnit.Framework;

namespace GameOfLife.Test
{
  [TestFixture]
  public class GameOfLifeTest
  {
    [Test]
    public void CanCreateGameOfLife()
    {
      var gol = new Game.GameOfLife(3, 3);
      Assert.That(gol.GameMap.RowCount, Is.EqualTo(3));
      Assert.That(gol.GameMap.ColumnCount, Is.EqualTo(3));
    }

    [Test]
    public void CanInitalizeGameOfLifeWith3By3()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Seed(
        CreateCells(
          "   ",
          "   ",
          "   ")
        );

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo("   "), "On or more cells is waken up in the first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo("   "), "On or more cells is waken up in the second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo("   "), "On or more cells is waken up in the third row");
    }

    [Test]
    public void CanHandleWrongSeed()
    {

      Assert.Throws<ArgumentException>(delegate
                                         {
                                           new Game.GameOfLife(1, 0);
                                         }
        );
    }

    [Test]
    public void CanInitalizeGameOfLifeWith4By4()
    {
      var gol = new Game.GameOfLife(4, 4);
      gol.Seed(
        CreateCells(
          "    ",
          "    ",
          "    ",
          "    ")
        );

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo("    "), "On or more cells is waken up in the first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo("    "), "On or more cells is waken up in the second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo("    "), "On or more cells is waken up in the third row");
      Assert.That(RowToString(gol.GameMap, 4), Is.EqualTo("    "), "On or more cells is waken up in the fourth row");
    }

    [Test]
    public void CanKillCellWithNoNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Seed(CreateCells(
        "   ",
        " * ",
        "   ")
        );

      gol.Tick();

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo("   "), "On or more cells is waken up in the first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo("   "), "On or more cells is waken up in the second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo("   "), "On or more cells is waken up in the third row");
    }

    [Test]
    public void CanKillCellWithOneNeighbour()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Seed(
        CreateCells(
          " * ",
          " * ",
          "   ")
        );

      gol.Tick();

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo("   "), "On or more cells is waken up in the first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo("   "), "On or more cells is waken up in the second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo("   "), "On or more cells is waken up in the third row");
    }

    [Test]
    public void CanLeaveCellWithTwoNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Seed(
        CreateCells(
          "  *",
          " * ",
          "*  ")
        );

      gol.Tick();

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo("   "), "Error in first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo(" * "), "Error in second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo("   "), "Error in third row");
    }

    [Test]
    public void CanLeaveCellWithThreeNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Seed(
        CreateCells(
          "*  ",
          " **",
          "*  ")
        );

      gol.Tick();

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo(" * "), "Error in first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo("** "), "Error in second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo(" * "), "Error in third row");
    }

    [Test]
    public void CanKillCellWithFourNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Seed(
        CreateCells(
          "* *",
          " * ",
          "* *")
        );

      gol.Tick();

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo(" * "), "Error in first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo("* *"), "Error in second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo(" * "), "Error in third row");
    }

    [Test]
    public void CanWakeCellWithThreeNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Seed(
        CreateCells(
          "* *",
          "   ",
          "*  ")
        );

      gol.Tick();

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo("   "), "Error in first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo(" * "), "Error in second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo("   "), "Error in third row");
    }

    [Test]
    public void CanDoNothingWithFourNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Seed(
        CreateCells(
          "* *",
          "   ",
          "* *")
        );

      gol.Tick();

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo("   "), "Error in first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo("   "), "Error in second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo("   "), "Error in third row");
    }

    [Test]
    public void CanHandleAllAlive()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Seed(
        CreateCells(
          "***",
          "***",
          "***")
        );

      gol.Tick();

      Assert.That(RowToString(gol.GameMap, 1), Is.EqualTo("* *"), "Error in first row");
      Assert.That(RowToString(gol.GameMap, 2), Is.EqualTo("   "), "Error in second row");
      Assert.That(RowToString(gol.GameMap, 3), Is.EqualTo("* *"), "Error in third row");
    }

    private static string RowToString(GameMap gameMap, int row)
    {
      string output = "";
      for (int column = 1; column <= gameMap.ColumnCount; column++)
      {
        output += gameMap.GetCell(row, column);
      }

      return output;
    }

    public static Cell[,] CreateCells(params string[] s)
    {
      var cells = new Cell[s.Length, s[0].Length];
      for (int row = 1; row <= s.Length; row++)
      {
        for (int column = 1; column <= s[row - 1].Length; column++)
        {
          cells[row - 1, column - 1] = new Cell(row, column, s[row - 1][column - 1]);
        }
      }

      return cells;
    }
  }
}
