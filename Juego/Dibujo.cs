using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juego
{
    class Dibujo
    {
        float[] vertices;
        int VertexBufferObject;
        int VertexArrayObject;
        public Shader shader;
        public Texturas textura;
        public Vector3 pos;
        public Vector3 tras;
        public Vector3 rot;
        public Vector3 sca;
        float ancho;
        float largo;
        float alto;
        public Dibujo()
        {
            pos = new Vector3(0f, 0f, -50f);
            tras = pos;
            sca = new Vector3(1, 1, 1);
            rot = (0, 0, 0);
            ancho = 2.0f;
            largo = 2.0f;
            alto = 0.0f;
        }
        public Dibujo(Vector3 posicion, float Ancho, float Largo, float Alto, Vector3 t, Vector3 r, Vector3 s)
        {
            pos = posicion;
            ancho = Ancho;
            largo = Largo;
            alto = Alto;
            tras = t;
            rot = r;
            sca = s;
        }

        void Load()
        {
            shader = new Shader();
            textura = new Texturas(Properties.Resources.texturas);
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, 5 * sizeof(float), 0);
            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, true, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(0);
            shader.Use();
        }

        public void fondo(float width, float heigh, Vector2 X, Vector2 Y)
        {
            vertices = new float[]
            {
                -width, -heigh, 0.0f,  X.X, Y.X,
                -width,  heigh, 0.0f,  X.X, Y.Y,
                 width,  heigh, 0.0f,  X.Y, Y.Y,

                -width, -heigh, 0.0f,  X.X, Y.X,
                 width,  heigh, 0.0f,  X.Y, Y.Y,
                 width, -heigh, 0.0f,  X.Y, Y.X,
            };
            Load();
        }

        float[] Base(Vector3 p, float an, float lar, float alt, Vector2 X, Vector2 Y)
        {
            float[] vert = new float[]
            {
                //base superior
                p.X,     p.Y,     p.Z-an, X.X, Y.Y,
                p.X,     p.Y,     p.Z,    X.X, Y.X,
                p.X+lar, p.Y,     p.Z,    X.Y, Y.X,

                p.X+lar, p.Y,     p.Z,    X.Y, Y.X,
                p.X,     p.Y,     p.Z-an, X.X, Y.Y,
                p.X+lar, p.Y,     p.Z-an, X.Y, Y.Y,
                //base inferior
                p.X,     p.Y-alt, p.Z-an, X.X, Y.Y,
                p.X,     p.Y-alt, p.Z,    X.X, Y.X,
                p.X+lar, p.Y-alt, p.Z,    X.Y, Y.X,

                p.X+lar, p.Y-alt, p.Z,    X.Y, Y.X,
                p.X,     p.Y-alt, p.Z-an, X.X, Y.Y,
                p.X+lar, p.Y-alt, p.Z-an, X.Y, Y.Y,
                //borde frontal
                p.X,     p.Y,     p.Z,    X.X, Y.Y,
                p.X,     p.Y-alt, p.Z,    X.X, Y.X,
                p.X+lar, p.Y,     p.Z,    X.Y, Y.Y,

                p.X,     p.Y-alt, p.Z,    X.X, Y.X,
                p.X+lar, p.Y,     p.Z,    X.Y, Y.Y,
                p.X+lar, p.Y-alt, p.Z,    X.Y, Y.X,
                //borde trasero
                p.X,     p.Y,     p.Z-an, X.X, Y.Y,
                p.X,     p.Y-alt, p.Z-an, X.X, Y.X,
                p.X+lar, p.Y,     p.Z-an, X.Y, Y.Y,

                p.X,     p.Y-alt, p.Z-an, X.X, Y.X,
                p.X+lar, p.Y,     p.Z-an, X.Y, Y.Y,
                p.X+lar, p.Y-alt, p.Z-an, X.Y, Y.X,
                //borde lateral izquierdo
                p.X,     p.Y,     p.Z,    X.X, Y.Y,
                p.X,     p.Y-alt, p.Z,    X.X, Y.X,
                p.X,     p.Y,     p.Z-an, X.Y, Y.Y,

                p.X,     p.Y,     p.Z-an, X.Y, Y.Y,
                p.X,     p.Y-alt, p.Z,    X.X, Y.X,
                p.X,     p.Y-alt, p.Z-an, X.Y, Y.X,
                //borde lateral derecho
                p.X+lar, p.Y,     p.Z,    X.X, Y.Y,
                p.X+lar, p.Y-alt, p.Z,    X.X, Y.X,
                p.X+lar, p.Y,     p.Z-an, X.Y, Y.Y,

                p.X+lar, p.Y,     p.Z-an, X.Y, Y.Y,
                p.X+lar, p.Y-alt, p.Z,    X.X, Y.X,
                p.X+lar, p.Y-alt, p.Z-an, X.Y, Y.X,
            };
            return vert;
        }

        public void mesa(Vector2 X, Vector2 Y)
        {
            float lar;
            if (ancho > largo)
            {
                lar = ancho;
            }
            else
            {
                lar = largo;
            }
            float[] basemesa = Base(pos, ancho, largo, alto * 10 / 100, X, Y);
            float[] pata1 = Base(new Vector3(pos.X + largo * 10 / 100, pos.Y - alto * 10 / 100, pos.Z - ancho * 10 / 100), lar * 10 / 100, lar * 10 / 100, alto - alto * 10 / 100, X, Y);
            float[] pata2 = Base(new Vector3(pos.X + largo * 10 / 100, pos.Y - alto * 10 / 100, pos.Z - ancho + ancho * 10 / 100 + lar * 10 / 100), lar * 10 / 100, lar * 10 / 100, alto - alto * 10 / 100, X, Y);
            float[] pata3 = Base(new Vector3(pos.X + largo - largo * 10 / 100 - lar * 10 / 100, pos.Y - alto * 10 / 100, pos.Z - ancho * 10 / 100), lar * 10 / 100, lar * 10 / 100, alto - alto * 10 / 100, X, Y);
            float[] pata4 = Base(new Vector3(pos.X + largo - largo * 10 / 100 - lar * 10 / 100, pos.Y - alto * 10 / 100, pos.Z - ancho + ancho * 10 / 100 + lar * 10 / 100), lar * 10 / 100, lar * 10 / 100, alto - alto * 10 / 100, X, Y);
            vertices = basemesa.Concat(pata1).ToArray().Concat(pata2).ToArray().Concat(pata3).ToArray().Concat(pata4).ToArray();
            Load();
        }

        public void silla(Vector2 X, Vector2 Y)
        {
            float lar;
            if (ancho > largo)
            {
                lar = ancho;
            }
            else
            {
                lar = largo;
            }
            float[] basesilla = Base(pos, ancho, largo, alto * 5 / 100, X, Y);
            float[] espaldar = Base(new Vector3(pos.X, pos.Y, pos.Z - ancho + ancho * 10 / 100), ancho * 10 / 100, largo, -alto * 50 / 100, X, Y);
            float[] pata1 = Base(new Vector3(pos.X + largo * 10 / 100, pos.Y - alto * 5 / 100, pos.Z - ancho * 10 / 100), lar * 15 / 100, lar * 15 / 100, alto * 45 / 100 - alto * 5 / 100, X, Y);
            float[] pata2 = Base(new Vector3(pos.X + largo * 10 / 100, pos.Y - alto * 5 / 100, pos.Z - ancho + ancho * 10 / 100 + lar * 15 / 100), lar * 15 / 100, lar * 15 / 100, alto * 45 / 100 - alto * 5 / 100, X, Y);
            float[] pata3 = Base(new Vector3(pos.X + largo - largo * 10 / 100 - lar * 15 / 100, pos.Y - alto * 5 / 100, pos.Z - ancho * 10 / 100), lar * 15 / 100, lar * 15 / 100, alto * 45 / 100 - alto * 5 / 100, X, Y);
            float[] pata4 = Base(new Vector3(pos.X + largo - largo * 10 / 100 - lar * 15 / 100, pos.Y - alto * 5 / 100, pos.Z - ancho + ancho * 10 / 100 + lar * 15 / 100), lar * 15 / 100, lar * 15 / 100, alto * 45 / 100 - alto * 5 / 100, X, Y);
            vertices = basesilla.Concat(pata1).ToArray().Concat(pata2).ToArray().Concat(pata3).ToArray().Concat(pata4).ToArray().Concat(espaldar).ToArray();
            Load();
        }
        public void robot(Vector2 X, Vector2 Y)
        {
            float lar;
            if (largo > ancho)
            {
                lar = largo;
            }
            else
            {

                lar = ancho;
            }
            float[] cabeza = Base(pos, alto * 15 / 100, alto * 15 / 100, alto * 15 / 100, X, Y);
            float[] cuello = Base(new Vector3(pos.X + alto * 15 * 40 / 10000, pos.Y - alto * 15 / 100, pos.Z - alto * 15 * 40 / 10000), alto * 15 / 500, alto * 15 / 500, alto * 5 / 100, X, Y);
            float[] torso = Base(new Vector3(pos.X - (largo - (alto * 15 / 100)) / 2, pos.Y - alto * 20 / 100, pos.Z), ancho, largo, alto * 40 / 100, X, Y);
            float[] brazo_derecho = Base(new Vector3(pos.X - (largo - (alto * 15 / 100)) / 2 - ancho * 50 / 100, pos.Y - alto * 20 / 100, pos.Z - ancho * 25 / 100), ancho * 50 / 100, ancho * 50 / 100, alto * 30 / 100, X, Y);
            float[] brazo_izquierdo = Base(new Vector3(pos.X + (alto * 15 / 100) + (largo - (alto * 15 / 100)) / 2, pos.Y - alto * 20 / 100, pos.Z - ancho * 25 / 100), ancho * 50 / 100, ancho * 50 / 100, alto * 30 / 100, X, Y);
            float[] pierna_derecha = Base(new Vector3(pos.X - (largo - (alto * 15 / 100)) / 2, pos.Y - alto * 60 / 100, pos.Z - ancho * 25 / 100), ancho * 50 / 100, ancho * 50 / 100, alto * 40 / 100, X, Y);
            float[] pierna_izquierda = Base(new Vector3(pos.X - (largo - (alto * 15 / 100)) / 2 + largo - ancho * 50 / 100, pos.Y - alto * 60 / 100, pos.Z - ancho * 25 / 100), ancho * 50 / 100, ancho * 50 / 100, alto * 40 / 100, X, Y);
            float[] pie_derecho = Base(new Vector3(pos.X - (largo - (alto * 15 / 100)) / 2, pos.Y - alto * 95 / 100, pos.Z + ancho * 25 / 100), ancho * 50 / 100, ancho * 50 / 100, alto * 5 / 100, X, Y);
            float[] pie_izquierdo = Base(new Vector3(pos.X - (largo - (alto * 15 / 100)) / 2 + largo - ancho * 50 / 100, pos.Y - alto * 95 / 100, pos.Z + ancho * 25 / 100), ancho * 50 / 100, ancho * 50 / 100, alto * 5 / 100, X, Y);
            vertices = cabeza.Concat(cuello).ToArray().Concat(torso).ToArray().Concat(brazo_derecho).ToArray().Concat(brazo_izquierdo).ToArray().Concat(pierna_derecha).ToArray().Concat(pierna_izquierda).ToArray().Concat(pie_izquierdo).ToArray().Concat(pie_derecho).ToArray();
            Load();
        }
        public void unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            shader.Dispose();
        }

        public void render(float width, float height)
        {
            Matrix4 view = Matrix4.CreateTranslation(tras.X, tras.Y, tras.Z);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), width / height, 0.1f, 100.0f);
            Matrix4 modelX;
            Matrix4 modelY;
            Matrix4 modelZ;
            modelX = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(rot.X));
            modelY = Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(rot.Y));
            modelZ = Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(rot.Z));
            Matrix4 scale = Matrix4.CreateScale(sca);
            shader.SetMatrix4("modelX", modelX);
            shader.SetMatrix4("modelY", modelY);
            shader.SetMatrix4("modelZ", modelZ);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("scale", scale);
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length);
        }
    }
}
