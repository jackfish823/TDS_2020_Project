#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace TopDownShooterProject2020
{
    public class Globals
    {
        public static int screenHeight, screenWidth; // Creatign a global variable for the screen height and width
            

        public static ContentManager content; // Creating global content variable
        public static SpriteBatch spriteBatch; // Creating global spriteBatch variable


        // inputs
        public static BaseKeyboard keyboard; // Creating global keyboard variable
        public static BaseMouse mouse;

        public static float GetDistance(Vector2 position, Vector2 target) // Finding distance between 2 positions (vectors)
        {
            return (float)Math.Sqrt(Math.Pow(position.X - target.X, 2) + Math.Pow(position.Y - target.Y, 2)); // Basic equation for finding distance between 2 points the distance formula
        }

        public static float RotateToward(Vector2 position, Vector2 focus) // Gets the position and where it need to rotate towards and uses x function in math bluh bluh  xxxx + add illustration and equation and explenation
        {
            float h, sineTheta, angle;
            if (position.Y-focus.Y != 0)
            {
                h = (float) Math.Sqrt(Math.Pow(position.X - focus.X, 2) + Math.Pow(position.Y-focus.Y, 2));
                sineTheta = (float)(Math.Abs(position.Y-focus.Y)/h);
            }
            else
            {
                h = position.X - focus.X;
                sineTheta = 0;
            }

            angle = (float)Math.Asin(sineTheta);

            // Drawing diagonial lines here.

            // Quadrant 2
            if(position.X-focus.X > 0 && position.Y-focus.Y > 0)
            {
                angle = (float)(Math.PI*3/2 + angle);
            }
            // Quadrant 3
            else if(position.X-focus.X > 0 && position.Y-focus.Y < 0)
            {
                angle = (float)(Math.PI*3/2 - angle);
            }
            // Quadrant 1
            else if(position.X-focus.X < 0 && position.Y-focus.Y > 0)
            {
                angle = (float)(Math.PI/2 - angle);
            }
            else if(position.X-focus.X < 0 && position.Y-focus.Y < 0)
            {
                angle = (float)(Math.PI/2 + angle);
            }
            else if(position.X-focus.X > 0 && position.Y-focus.Y == 0)
            {
                angle = (float)Math.PI*3/2;
            }
            else if(position.X-focus.X < 0 && position.Y-focus.Y == 0)
            {
                angle = (float)Math.PI/2;
            }
            else if(position.X-focus.X == 0 && position.Y-focus.Y > 0)
            {
                angle = (float)0;
            }
            else if(position.X-focus.X == 0 && position.Y-focus.Y < 0)
            {
                angle = (float)Math.PI;
            }

            return angle;
        }
    }
}
