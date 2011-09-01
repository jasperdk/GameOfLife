using Game;
using NUnit.Framework;

namespace GameOfLifeTest
{
  [TestFixture]
  public class GameOfLifeTest
  {

    [Test]
    public void CanCreateGameOfLife()
    {
      var gol = new GameOfLife(3, 3);
      Assert.That(gol.Map.Lines.Count, Is.EqualTo(3));
    }

    [Test]
    public void CanInitalizeGameOfLifeWith3by3()
    {
      var gol = new GameOfLife(3, 3);
      gol.Initialize(
        "   ",
        "   ",
        "   "
        );

      Assert.That(gol.Map.Lines[0], Is.EqualTo("   "));
      Assert.That(gol.Map.Lines[1], Is.EqualTo("   "));
      Assert.That(gol.Map.Lines[2], Is.EqualTo("   "));

    }

    [Test]
    public void CanInitalizeGameOfLifeWith4by4()
    {
      var gol = new GameOfLife(4, 4);
      gol.Initialize(
        "    ",
        "    ",
        "    ",
        "    "
        );

      Assert.That(gol.Map.Lines[0], Is.EqualTo("    "));
      Assert.That(gol.Map.Lines[1], Is.EqualTo("    "));
      Assert.That(gol.Map.Lines[2], Is.EqualTo("    "));
      Assert.That(gol.Map.Lines[3], Is.EqualTo("    "));

    }
  }
}
