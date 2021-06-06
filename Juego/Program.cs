using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;
using OpenTK.Windowing.Common;

namespace Juego
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings config = new GameWindowSettings();
            config.RenderFrequency = 30.0;
            config.UpdateFrequency = 60.0;
            NativeWindowSettings nativo = new NativeWindowSettings();
            nativo.Size = new Vector2i(800, 600);
            nativo.Title = "Programacion Grafica";
            using(Pantalla game = new Pantalla(config, nativo))
            {
                game.Run();
            }
        }
    }
}
