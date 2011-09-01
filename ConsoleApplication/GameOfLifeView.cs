using Game;

namespace ConsoleApplication
{
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

    public string[] Lines
    {
      get
      {
        var lines = new string[_gameOfLife.Cells.Length];
        for (int index = 0; index < _gameOfLife.Cells.Length; index++)
        {
          foreach (var cell in _gameOfLife.Cells[index])
          {
            lines[index] += cell.ToString();
          }
        }

        return lines;
      }
    }

    public void Initialize(params string[] lines)
    {
      var cells = new Cell[lines.Length][];
      for (int row = 0; row < lines.Length; row++)
      {
        cells[row] = new Cell[lines[row].Length];
        for (int column = 0; column < lines[row].Length; column++)
        {
          cells[row][column] = new Cell(row, column, lines[row][column]);
        }
      }

      _gameOfLife.Seed(cells);
    }
  }
}