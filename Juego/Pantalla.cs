using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;
using System.Linq;
using OpenTK.Mathematics;
using System.Drawing;
using System.Drawing.Imaging;

namespace Juego
{
    class Pantalla : GameWindow
    {
        private double time;
        Escenario escena = new Escenario();
        Dibujo fondo = new Dibujo();
        Dibujo mesa = new Dibujo(new Vector3(-0.5f, 0.4f, 0f), 1f, 1f, 0.6f, new Vector3(0.0f, -0.5f, -2f), new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        Dibujo silla = new Dibujo(new Vector3(-0.15f, 0.15f, -0.7f), 0.4f, 0.4f, 0.85f, new Vector3(0.0f, -0.5f, -2f), new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        Dibujo robot = new Dibujo(new Vector3(0.0f, 1.0f, -0.5f), 0.1f, 0.2f, 0.6f, new Vector3(0.0f, -0.5f, -2f), new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        public Pantalla(GameWindowSettings config, NativeWindowSettings nativo) : base(config, nativo) { }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            fondo.fondo(float.Parse(Size.X.ToString())/25, float.Parse(Size.Y.ToString()) / 25, new Vector2(0.1f, 1.0f), new Vector2(0.0f, 1.0f));
            mesa.mesa(new Vector2(0.0f, 0.1f), new Vector2(0.8f, 1.0f));
            silla.silla(new Vector2(0.0f, 0.1f), new Vector2(0.6f, 0.8f));
            robot.robot(new Vector2(0.0f, 0.1f), new Vector2(0.2f, 0.4f));
            escena.addDibujo("fondo",fondo);
            escena.addDibujo("mesa",mesa);
            escena.addDibujo("silla",silla);
            escena.addDibujo("robot",robot);
        }
        protected override void OnUnload()
        {
            base.OnUnload();
            escena.Unload();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            time += 30.0 * e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.5f, 0.0f, 0.5f, 1.0f);
            GL.Viewport(0, 0, Size.X, Size.Y);
            escena.changeItem("mesa", null, new Vector3(5, float.Parse(time.ToString()), 0), null);
            escena.changeItem("silla", null, new Vector3(5, float.Parse(time.ToString()), 0), null);
            escena.changeItem("robot", null, new Vector3(5, float.Parse(time.ToString()), 0), null);
            escena.render(Size);
            Context.SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                this.Close();
            }
            base.OnUpdateFrame(e);
        }
    }
}
