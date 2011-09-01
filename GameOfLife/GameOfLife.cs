using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
  public class GameOfLife
  {
    private Cell[][] _cells;

    public GameOfLife(int rows, int columns)
    {
      _cells = new Cell[rows][];
      for (int i = 0; i < rows; i++)
      {
        _cells[i] = new Cell[columns];
      }
    }

    public Cell[][] Cells
    {
      get { return _cells; }
    }

    public void Seed(Cell[][] cells)
    {
      var columns = cells[0].Length;
      if (cells.Any(cellRow => cellRow.Length != columns))
      {
        throw new ArgumentException("All rows must contain the same number of columns!");
      }

      _cells = cells;
    }

    public void Tick()
    {
      var newCells = CopyCells();

      foreach (var cellRow in _cells)
      {
        foreach (var cell in cellRow)
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
        newCells[cell.Row][cell.Column].WakeUp();
      }
    }

    private static void ExecuteKillRule(Cell cell, int livingNeighbours, Cell[][] newCells)
    {
      if (cell.IsAlive && (livingNeighbours < 2 || livingNeighbours > 3))
      {
        newCells[cell.Row][cell.Column].Kill();
      }
    }

    private int GetLivingNeighboursCount(Cell cell)
    {
      return GetNeighbours(cell).Where(c => c.IsAlive).Count();
    }

    private IEnumerable<Cell> GetNeighbours(Cell cell)
    {
      var neighbours = new List<Cell>();

      //Get neighbours from row above
      if (cell.Row > 0)
      {
        AddNeighboursFromRow(neighbours,cell.Row - 1, cell.Column);
      }

      //Get neighbours from same row
      if (cell.Column > 0)
        neighbours.Add(_cells[cell.Row][cell.Column - 1]);
      if (cell.Column < _cells[cell.Row].Length - 1)
        neighbours.Add(_cells[cell.Row][cell.Column + 1]);

      //Get neighbours from row below
      if (cell.Row < _cells.Length - 1)
      {
        AddNeighboursFromRow(neighbours,cell.Row + 1, cell.Column);
      }

      return neighbours;
    }

    private void AddNeighboursFromRow(List<Cell> neighbours, int rowNumber, int columnNumber)
    {
      if (columnNumber > 0)
        neighbours.Add(_cells[rowNumber][columnNumber - 1]);
      neighbours.Add(_cells[rowNumber][columnNumber]);
      if (columnNumber < _cells[rowNumber].Length - 1)
        neighbours.Add(_cells[rowNumber][columnNumber + 1]);
    }

    private Cell[][] CopyCells()
    {
      var cells = new Cell[_cells.Length][];
      for (var row = 0; row < _cells.Length; row++)
      {
        cells[row] = new Cell[_cells[row].Length];

        for (var column = 0; column < _cells[row].Length; column++)
        {
          cells[row][column] = new Cell(_cells[row][column]);
        }
      }

      return cells;
    }
  }
}
