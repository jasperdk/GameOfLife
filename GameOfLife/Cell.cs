﻿namespace Game
{
  public enum CellState { Dead, Alive }

  /// <summary>
  /// Single cell in the game of life
  /// </summary>
  public class Cell
  {
    public const char Alive = '*';
    public const char Dead = ' ';

    private readonly int _row;
    private readonly int _column;

    private CellState _state;

    /// <summary>
    /// Create cell with initial state
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="state"></param>
    public Cell(int row, int column, char state)
    {
      _row = row;
      _column = column;
      _state = state == Alive ? CellState.Alive : CellState.Dead;
    }

    /// <summary>
    /// Create cell with same state as <paramref name="cell"/>
    /// </summary>
    /// <param name="cell"></param>
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