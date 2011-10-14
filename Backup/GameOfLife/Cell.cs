namespace Game
{
  public enum CellState { Dead, Alive }

  public class Cell
  {
    public const char Alive = '*';
    public const char Dead = ' ';

    private readonly int _row;
    private readonly int _column;

    private CellState _state;

    public Cell(int row, int column, char state)
    {
      _row = row;
      _column = column;
      if (state == Alive)
        _state = CellState.Alive;
      else
        _state = CellState.Dead;
    }

    public Cell(Cell cell)
      : this(cell.Row, cell.Column, cell.State)
    {
    }

    public char State
    {
      get
      {
        if (_state == CellState.Alive)
          return Alive;
        return Dead;
      }
    }

    public int Row
    {
      get
      {
        return _row;
      }
    }

    public int Column
    {
      get
      {
        return _column;
      }
    }

    public bool IsAlive
    {
      get { return _state == CellState.Alive; }
    }

    public bool IsDead
    {
      get { return _state == CellState.Dead; }
    }

    public override string ToString()
    {
      return State.ToString();
    }

    public void Kill()
    {
      if (IsAlive)
        _state = CellState.Dead;
    }

    public void WakeUp()
    {
      if (IsDead)
        _state = CellState.Alive;
    }
  }
}