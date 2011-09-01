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
      foreach (var cellLine in gol.Cells)
      {
        Assert.That(cellLine.Length, Is.EqualTo(3));
      }
    }

    [Test]
    public void CanInitalizeGameOfLifeWith3By3()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Initialize(
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
    public void CanInitalizeGameOfLifeWith4By4()
    {
      var gol = new Game.GameOfLife(4, 4);
      gol.Initialize(
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
      gol.Initialize(CreateCells(
        "   ",
        " * ",
        "   ")
        );

      gol.Proces();

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("   "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
    }

    [Test]
    public void CanKillCellWithOneNeighbour()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Initialize(
        CreateCells(
          " * ",
          " * ",
          "   ")
        );

      gol.Proces();

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("   "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
    }

    [Test]
    public void CanLeaveCellWithTwoNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Initialize(
        CreateCells(
          "  *",
          " * ",
          "*  ")
        );

      gol.Proces();

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo(" * "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
    }

    [Test]
    public void CanLeaveCellWithThreeNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Initialize(
        CreateCells(
          "*  ",
          " **",
          "*  ")
        );

      gol.Proces();

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo(" * "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("** "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo(" * "), "3");
    }

    [Test]
    public void CanKillCellWithFourNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Initialize(
        CreateCells(
          "* *",
          " * ",
          "* *")
        );

      gol.Proces();

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo(" * "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo("* *"), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo(" * "), "3");
    }

    [Test]
    public void CanWakeCellWithThreeNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Initialize(
        CreateCells(
          "* *",
          "   ",
          "*  ")
        );

      gol.Proces();

      Assert.That(CellsToString(gol.Cells[0]), Is.EqualTo("   "), "1");
      Assert.That(CellsToString(gol.Cells[1]), Is.EqualTo(" * "), "2");
      Assert.That(CellsToString(gol.Cells[2]), Is.EqualTo("   "), "3");
    }

    [Test]
    public void CanDoNothingWithFourNeighbours()
    {
      var gol = new Game.GameOfLife(3, 3);
      gol.Initialize(
        CreateCells(
          "* *",
          "   ",
          "* *")
        );

      gol.Proces();

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
      for (int indexX = 0; indexX < s.Length; indexX++)
      {
        cells[indexX] = new Cell[s[indexX].Length];
        for (int indexY = 0; indexY < s[indexX].Length; indexY++)
        {
          cells[indexX][indexY] = new Cell(indexX, indexY, s[indexX][indexY]);
        }
      }

      return cells;
    }
  }
}
