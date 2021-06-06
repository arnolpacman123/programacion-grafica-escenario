using OpenTK.Mathematics;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Juego
{
    class Escenario
    {
        public Dictionary<string, Dibujo> dibujos;

        public Escenario()
        {
            dibujos = new Dictionary<string, Dibujo>();
        }
        public void addDibujo(String nombre,Dibujo act)
        {
            dibujos.Add(nombre, act);
        }
        public void Load()
        {
            
        }

        public void Unload()
        {
            foreach(KeyValuePair<string, Dibujo> act in dibujos)
            {
                act.Value.unload();
            }
        }
        public void changeItem(String key, Object tras, Object rot, Object sca)
        {
            Dibujo aux = dibujos.GetValueOrDefault(key);
            if(tras != null)
            {
                aux.tras = (Vector3) tras;
            }
            if (rot != null)
            {
                aux.rot = (Vector3)rot;
            }
            if (sca != null)
            {
                aux.sca = (Vector3)sca;
            }
        }
        public void render(Vector2i Size)
        {
            foreach (KeyValuePair<string, Dibujo> act in dibujos)
            {
                act.Value.render(Size.X, Size.Y);
            }
        }
    }
}
