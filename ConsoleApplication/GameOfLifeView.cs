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
      for (int indexX = 0; indexX < lines.Length; indexX++)
      {
        cells[indexX] = new Cell[lines[indexX].Length];
        for (int indexY = 0; indexY < lines[indexX].Length; indexY++)
        {
          cells[indexX][indexY] = new Cell(indexX, indexY, lines[indexX][indexY]);
        }
      }

      _gameOfLife.Seed(cells);
    }
  }
}