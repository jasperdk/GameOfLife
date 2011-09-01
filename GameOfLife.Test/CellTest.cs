using Game;
using NUnit.Framework;

namespace GameOfLife.Test
{
  [TestFixture]
  public class CellTest
  {
    [Test]
    public void CanCreateCell()
    {
      var cell = new Cell(1, 2, Cell.Alive);
      Assert.That(cell.Row, Is.EqualTo(1));
      Assert.That(cell.Column, Is.EqualTo(2));
      Assert.That(cell.State, Is.EqualTo(Cell.Alive));
    }

    [Test]
    [TestCase(Cell.Alive, true)]
    [TestCase(Cell.Dead, false)]
    public void CanTestIsAlive(char state, bool result)
    {
      var cell = new Cell(1, 2, state);
      Assert.That(cell.IsAlive, Is.EqualTo(result));
    }

    [Test]
    [TestCase(Cell.Alive, false)]
    [TestCase(Cell.Dead, true)]
    public void CanTestIsDead(char state, bool result)
    {
      var cell = new Cell(1, 2, state);
      Assert.That(cell.IsDead, Is.EqualTo(result));
    }

    [Test]
    [TestCase(Cell.Alive)]
    [TestCase(Cell.Dead)]
    public void CanKillCell(char state)
    {
      var cell = new Cell(1, 2, state);
      cell.Kill();
      Assert.That(cell.IsDead, Is.EqualTo(true));
    }

    [Test]
    [TestCase(Cell.Alive)]
    [TestCase(Cell.Dead)]
    public void CanWakeupCell(char state)
    {
      var cell = new Cell(1, 2, state);
      cell.WakeUp();
      Assert.That(cell.IsAlive, Is.EqualTo(true));
    }

  }
}
