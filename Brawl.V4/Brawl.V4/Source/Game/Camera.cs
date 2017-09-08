using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Game
{
    class Camera
    {
        public static Vector3 camTarget;          //Where it looks
        public static Vector3 camPosition;        //Where it is
        private static Vector3 cameraRotation;

        public static Matrix projectionMatrix;
        public static Matrix viewMatrix;
        public static Matrix worldMatrix;

        private static Point lastMouse;            //The last position of the mouse cursor

        private static Vector3 get3DPos(Vector3 rot)
        {
            float d = (float)Math.Sin(rot.Y);

            return new Vector3((float)Math.Cos(rot.X) * d, (float)Math.Cos(rot.Y), (float)Math.Sin(rot.X) * d);

        }
        public static void initialize(GraphicsDeviceManager graphics)
        {
            //Sets up cam facing and view
            camPosition = new Vector3(0f, 0f, 0);
            cameraRotation = new Vector3(0f, 0f, 0f);
            camTarget = get3DPos(cameraRotation);

            //View matrix
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                               MathHelper.ToRadians(90f), graphics.
                               GraphicsDevice.Viewport.AspectRatio, 0.01f, 1000f);

            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, new Vector3(0f, 1f, 0f));
            worldMatrix = Matrix.CreateWorld(camTarget, Vector3.Forward, Vector3.Up);

            //Mouse position
            lastMouse = Mouse.GetState().Position;
           
        }

        //Clamp utility
        private static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        private static Vector3 normalize2D(Vector3 v)
        {
            Vector2 flat = new Vector2(v.X, v.Z);
            flat.Normalize();

            Vector3 result = new Vector3(flat.X, 0, flat.Y);

            return result;

        }

        //Where is the camera facing relative to the cameras position
        public static Vector3 getDer(Vector3 der, float speed)
        {
            if (der == Vector3.Up) return der * speed;
            else if (der == Vector3.Forward) return normalize2D(camTarget) * speed;

            else if (der == Vector3.Left)
            {
                Vector3 target = get3DPos(cameraRotation + new Vector3((float)(Math.PI / 2f), 0, 0));

                return normalize2D(target) * speed;

            }
            return new Vector3();
        }

        public static void update()
        {
            //Mouse delta & camera ajustments
            Point dMouse = lastMouse - Mouse.GetState().Position;
            //Mouse.SetPosition(Brawl.gameSize.X / 2, Brawl.gameSize.Y / 2);
            //Mouse.SetPosition(0, 0);
            lastMouse = Mouse.GetState().Position;

            cameraRotation.X -= dMouse.X / (float)100;
            cameraRotation.Y -= dMouse.Y / (float)100;

            cameraRotation.Y = Clamp(cameraRotation.Y, 0.01f, (float)Math.PI - 0.01f);

            camTarget = get3DPos(cameraRotation);

            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget + camPosition,
                         Vector3.Up);

            viewMatrix *= Matrix.CreateScale(5);

        }
    }
}
