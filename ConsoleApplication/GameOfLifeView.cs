using Game;

namespace ConsoleApplication
{
  /// <summary>
  /// Game of life-view model for a simple line based representation
  /// </summary>
  public class GameOfLifeView
  {
    private readonly GameOfLife _gameOfLife;

    public GameOfLifeView(GameOfLife gameOfLife)
    {
      _gameOfLife = gameOfLife;
    }

    public GameOfLife GameOfLife
    {
      get
      {
        return _gameOfLife;
      }
    }

    /// <summary>
    /// Get current state of the game of life
    /// </summary>
    public string[] Lines
    {
      get
      {
        var lines = new string[_gameOfLife.GameMap.RowCount];
        for (int row = 1; row <= _gameOfLife.GameMap.RowCount; row++)
        {
          for (int column = 1; column <= _gameOfLife.GameMap.ColumnCount; column++)
          {
            lines[row - 1] += _gameOfLife.GameMap.GetCell(row, column).ToString();
          }
        }

        return lines;
      }
    }

    /// <summary>
    /// Seed game of life
    /// </summary>
    /// <param name="lines">each charater represents a cell in game of life. '*'=alive ' '=dead</param>
    public void Seed(params string[] lines)
    {
      var cells = new Cell[lines.Length, lines[0].Length];
      for (int row = 1; row <= lines.Length; row++)
      {
        for (int column = 1; column <= lines[row - 1].Length; column++)
        {
          cells[row - 1, column - 1] = new Cell(row, column, lines[row - 1][column - 1]);
        }
      }

      _gameOfLife.Seed(cells);
    }
  }
}