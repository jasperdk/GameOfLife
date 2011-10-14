using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using NUnit.Framework;

namespace GameOfLifeTest
{
  [TestFixture]
  public class GameOfLifeTest
  {
    [Test]
    public void CanCreateGameOfLife()
    {
      var gol = new Game.GameOfLife(3, 3);
      Assert.That(gol.Cells.Length, Is.EqualTo(3));
      foreach (var cellRow in gol.Cells)
      {
        Assert.That(cellRow.Length, Is.EqualTo(3));
      }
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

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("   "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
    }

    [Test]
    public void CanHandleWrongSeed()
    {
      var gol = new Game.GameOfLife(3, 3);
      Assert.Throws<ArgumentException>(delegate
                                         {
                                           gol.Seed(
                                             CreateCells(
                                               "   ",
                                               "  ",
                                               " ")
                                             );
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

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("    "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("    "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("    "), "3");
      Assert.That(CellsToString(gol.Cells[3]), Is.EqualTo("    "), "4");
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

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("   "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
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

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("   "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
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

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo(" * "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
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

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo(" * "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("** "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo(" * "), "3");
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

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo(" * "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("* *"), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo(" * "), "3");
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

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo(" * "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
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

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("   "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
    }

    private static string CellsToString(IEnumerable<Cell> cells)
    {
      return cells.Aggregate(string.Empty, (current, cell) => current + cell.ToString());
    }

    private static Cell[][] CreateCells(params string[] s)
    {
      var cells = new Cell[s.Length][];
      for (int row = 0; row < s.Length; row++)
      {
        cells[row] = new Cell[s[row].Length];
        for (int column = 0; column < s[row].Length; column++)
        {
          cells[row][column] = new Cell(row, column, s[row][column]);
        }
      }

      return cells;
    }
  }
}
