using System.Collections.Generic;
using System.Linq;

namespace Game
{
  public class GameOfLife
  {
    private Cell[][] _cells;

    public GameOfLife(int x, int y)
    {
      _cells = new Cell[x][];
      for (int i = 0; i < x; i++)
      {
        _cells[i] = new Cell[y];
      }
    }

    public Cell[][] Cells
    {
      get { return _cells; }
    }

    public void Initialize(Cell[][] cells)
    {
      _cells = cells;
    }

    public void Proces()
    {
      var newCells = CopyCells();

      foreach (var cellLine in _cells)
      {
        foreach (var cell in cellLine)
        {
          var livingNeighbours = GetLivingNeighboursCount(cell);
          ExecuteKillRule(cell, livingNeighbours, newCells);
          ExecuteWakeupRule(cell, livingNeighbours, newCells);
        }
      }

      _cells = newCells;
    }

    private static void ExecuteWakeupRule(Cell cell, int livingNeighbours, Cell[][] newCells)
    {
      if (cell.IsDead && livingNeighbours == 3)
      {
        newCells[cell.X][cell.Y].WakeUp();
      }
    }

    private static void ExecuteKillRule(Cell cell, int livingNeighbours, Cell[][] newCells)
    {
      if (cell.IsAlive && (livingNeighbours < 2 || livingNeighbours > 3))
      {
        newCells[cell.X][cell.Y].Kill();
      }
    }

    private int GetLivingNeighboursCount(Cell cell)
    {
      return GetNeighbours(cell).Where(c => c.IsAlive).Count();
    }

    private IList<Cell> GetNeighbours(Cell cell)
    {
      var neighbours = new List<Cell>();
      if (cell.X > 0)
      {
        if (cell.Y > 0)
          neighbours.Add(_cells[cell.X - 1][cell.Y - 1]);
        neighbours.Add(_cells[cell.X - 1][cell.Y]);
        if (cell.Y < _cells[cell.X - 1].Length - 1)
          neighbours.Add(_cells[cell.X - 1][cell.Y + 1]);
      }

      if (cell.Y > 0)
        neighbours.Add(_cells[cell.X][cell.Y - 1]);
      if (cell.Y < _cells[cell.X].Length - 1)
        neighbours.Add(_cells[cell.X][cell.Y + 1]);

      if (cell.X < _cells.Length - 1)
      {
        if (cell.Y > 0)
          neighbours.Add(_cells[cell.X + 1][cell.Y - 1]);
        neighbours.Add(_cells[cell.X + 1][cell.Y]);
        if (cell.Y < _cells[cell.X].Length - 1)
          neighbours.Add(_cells[cell.X + 1][cell.Y + 1]);
      }

      return neighbours;
    }

    private Cell[][] CopyCells()
    {
      var cells = new Cell[_cells.Length][];
      for (var indexX = 0; indexX < _cells.Length; indexX++)
      {
        cells[indexX] = new Cell[_cells[indexX].Length];

        for (var indexY = 0; indexY < _cells[indexX].Length; indexY++)
        {
          cells[indexX][indexY] = new Cell(_cells[indexX][indexY]);
        }
      }

      return cells;
    }
  }
}
