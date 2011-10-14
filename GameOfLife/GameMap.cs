using System;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
  /// <summary>
  /// Representation of the game map
  /// </summary>
  public class GameMap : IEnumerable<Cell>, ICloneable
  {
    private Cell[,] _cells;
    internal Cell[,] Cells { get { return _cells; } }

    public int RowCount { get { return _cells.GetLength(0); } }
    public int ColumnCount { get { return _cells.GetLength(1); } }

    public GameMap(int rowCount, int columnCount)
    {
      _cells = new Cell[rowCount, columnCount];
    }

    /// <summary>
    /// Seed the game map with an initial pattern
    /// </summary>
    /// <param name="initialPattern"></param>
    public void Seed(Cell[,] initialPattern)
    {
      if (initialPattern.GetLength(0) != RowCount || initialPattern.GetLength(1) != ColumnCount)
        throw new ArgumentException("Invalid game configuration!");
      _cells = initialPattern;
    }

    /// <summary>
    /// Seed game map with the state of another gameMap
    /// </summary>
    /// <param name="gameMap"></param>
    public void Seed(GameMap gameMap)
    {
      if (gameMap.RowCount != RowCount || gameMap.ColumnCount != ColumnCount)
        throw new ArgumentException("Invalid game configuration!");
      _cells = gameMap.Cells;
    }

    public Cell GetCell(int row, int column)
    {
      return _cells[row - 1, column - 1];
    }

    #region ICloneable implementation
    public object Clone()
    {
      var cells = new Cell[RowCount, ColumnCount];
      foreach (var cell in _cells)
      {
        if (cell != null)
          cells[cell.Row - 1, cell.Column - 1] = new Cell(cell);
      }
      var gameMap = new GameMap(RowCount, ColumnCount);
      gameMap.Seed(cells);
      return gameMap;
    }
    #endregion

    #region IEnumarator implementation
    public IEnumerator<Cell> GetEnumerator()
    {
      var enumerator = new List<Cell>();
      foreach (var cell in _cells)
      {
        if (cell != null)
          enumerator.Add(new Cell(cell));
      }
      return enumerator.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
    #endregion
  }
}