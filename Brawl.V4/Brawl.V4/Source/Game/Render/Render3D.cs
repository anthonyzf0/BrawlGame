using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Game.Render
{
    class Render3D
    {
        //Scale
        private Vector3 basicScale = Vector3.One;

        //All the models
        private Dictionary<String, Model> models = new Dictionary<string, Model>();
       
        
        public Render3D(ContentManager c)
        {
            loadModels(c);
        }
        
        //Loads all models in list
        private void loadModels(ContentManager c)
        {
            string[] LoadModels = { "MonoCube" };   //Loads these

            foreach (string model in LoadModels)
                models[model] = c.Load<Model>("Models/"+model);
        }

        //Draw a model
        public void draw(String mod, Vector3 loc, Vector3 scale)
        {
            foreach (ModelMesh mesh in models[mod].Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.AmbientLightColor = new Vector3(0, 0, 0);
                    effect.View = Camera.viewMatrix;
                    effect.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(loc);
                    effect.Projection = Camera.projectionMatrix;
                }
                mesh.Draw();
            }

        }

    }
}
