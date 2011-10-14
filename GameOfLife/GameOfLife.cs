using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
  /// <summary>
  /// Implementation of Game of life as described here http://en.wikipedia.org/wiki/Conway's_Game_of_Life
  /// </summary>
  public class GameOfLife
  {

    private readonly GameMap _gameMap;

    public GameOfLife(int rows, int columns)
    {
      if (rows < 1 || columns < 1)
        throw new ArgumentException("Invalid game configuration!");
      _gameMap = new GameMap(rows, columns);
    }

    /// <summary>
    /// Current generation
    /// </summary>
    public GameMap GameMap
    {
      get { return _gameMap; }
    }

    /// <summary>
    /// Set initial pattern 
    /// </summary>
    /// <param name="initialPattern"></param>
    public void Seed(Cell[,] initialPattern)
    {
      if (initialPattern.GetLength(0) != _gameMap.RowCount || initialPattern.GetLength(1) != _gameMap.ColumnCount)
        throw new ArgumentException("Invalid game configuration!");

      _gameMap.Seed(initialPattern);
    }

    /// <summary>
    /// Greate new generation of game
    /// </summary>
    public void Tick()
    {
      var nextGenerationgameMap = _gameMap.Clone() as GameMap;

      foreach (var cell in _gameMap)
      {
        var livingNeighbours = GetLivingNeighboursCount(cell);
        ExecuteKillRule(nextGenerationgameMap, cell, livingNeighbours);
        ExecuteWakeupRule(nextGenerationgameMap, cell, livingNeighbours);
      }

      _gameMap.Seed(nextGenerationgameMap);
    }

    private static void ExecuteWakeupRule(GameMap nextGenerationgameMap, Cell cell, int livingNeighbours)
    {
      if (cell.IsDead && livingNeighbours == 3)
      {
        nextGenerationgameMap.GetCell(cell.Row, cell.Column).WakeUp();
      }
    }

    private static void ExecuteKillRule(GameMap nextGenerationgameMap, Cell cell, int livingNeighbours)
    {
      if (cell.IsAlive && (livingNeighbours < 2 || livingNeighbours > 3))
      {
        nextGenerationgameMap.GetCell(cell.Row, cell.Column).Kill();
      }
    }

    private int GetLivingNeighboursCount(Cell cell)
    {
      return GetNeighbours(cell).Where(c => c.IsAlive).Count();
    }

    private IEnumerable<Cell> GetNeighbours(Cell cell)
    {
      var neighbours = new List<Cell>();

      GetNeighboursFromRowAbove(cell, neighbours);
      GetNeighboursFromSameRow(cell, neighbours);
      GetNeighboursFromRowBelow(cell, neighbours);

      return neighbours;
    }

    private void GetNeighboursFromRowBelow(Cell cell, List<Cell> neighbours)
    {
      if (cell.Row < _gameMap.RowCount)
      {
        AddNeighboursFromRow(neighbours, cell.Row + 1, cell.Column);
      }
    }

    private void GetNeighboursFromSameRow(Cell cell, List<Cell> neighbours)
    {
      if (cell.Column > 1)
        neighbours.Add(_gameMap.GetCell(cell.Row, cell.Column - 1));
      if (cell.Column < _gameMap.ColumnCount)
        neighbours.Add(_gameMap.GetCell(cell.Row, cell.Column + 1));
    }

    private void GetNeighboursFromRowAbove(Cell cell, List<Cell> neighbours)
    {
      if (cell.Row > 1)
      {
        AddNeighboursFromRow(neighbours, cell.Row - 1, cell.Column);
      }
    }

    private void AddNeighboursFromRow(List<Cell> neighbours, int rowNumber, int columnNumber)
    {
      if (columnNumber > 1)
        neighbours.Add(_gameMap.GetCell(rowNumber, columnNumber - 1));
      neighbours.Add(_gameMap.GetCell(rowNumber, columnNumber));
      if (columnNumber < _gameMap.ColumnCount)
        neighbours.Add(_gameMap.GetCell(rowNumber, columnNumber + 1));
    }
  }
}
