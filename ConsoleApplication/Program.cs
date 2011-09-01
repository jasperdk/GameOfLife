using System;
using System.Threading;
using Game;

namespace ConsoleApplication
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //var gol = new GameOfLifeView(new GameOfLife(3, 3));
      //gol.Seed(
      //  " * ",
      //  " * ",
      //  " * ");

      var gol = new GameOfLifeView(new GameOfLife(10, 10));
      gol.Initialize(
        " *        ",
        " *        ",
        " *        ",
        "          ",
        "    *     ",
        "    *     ",
        "    *     ",
        "    *     ",
        "    *     ",
        "    *     "
        );
      while (true)
      {
        Console.Clear();
        foreach (var line in gol.Lines)
        {
          Console.WriteLine(line);
        }
        Thread.Sleep(1000);
        gol.GameOfLife.Tick();
      }
    }
  }
}
