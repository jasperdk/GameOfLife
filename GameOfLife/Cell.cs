namespace Game
{
  public enum CellState { Dead, Alive }

  public class Cell
  {
    public const char Alive = '*';
    public const char Dead = ' ';

    private readonly int _x;
    private readonly int _y;

    private CellState _state;

    public Cell(int x, int y, char state)
    {
      _x = x;
      _y = y;
      if (state == Alive)
        _state = CellState.Alive;
      else
        _state = CellState.Dead;
    }

    public Cell(Cell cell)
      : this(cell.X, cell.Y, cell.State)
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

    public int X
    {
      get
      {
        return _x;
      }
    }

    public int Y
    {
      get
      {
        return _y;
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