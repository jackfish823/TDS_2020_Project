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
    public delegate void PassObject(object i); // Passing an object and doesnt return anything (object can by anything)
    public delegate object PassOnjectAndReturn(object i); // Passing an object and returning an object too (object can by anything)

    public class Globals
    {
        public static int screenHeight, screenWidth; // Creatign a global variable for the screen height and width

        public static Random random = new Random(); // Creating global random variable

        public static ContentManager content; // Creating global content variable
        public static SpriteBatch spriteBatch; // Creating global spriteBatch variable

        public static Effect antiAliasingEffect; // Basically we have 3 main places where we draw things: UI, BasicProjectile and AttackableObject (fathers), so there is where we gonna add it



        // inputs
        public static BaseKeyboard keyboard; // Creating global keyboard variable
        public static BaseMouse mouse; // Creating global mouse variable

        public static GameTime gameTime; // Creating global game time variable

        public static float GetDistance(Vector2 position, Vector2 target) // Finding distance between 2 positions (vectors)
        {
            return (float)Math.Sqrt(Math.Pow(position.X - target.X, 2) + Math.Pow(position.Y - target.Y, 2)); // Basic equation for finding distance between 2 points the distance formula
        }

        public static float GetAngle(Vector2 vector) // Gets a vector and calculates the the angle of the vector with tangens
        {
            return (float)Math.Atan2((double)(vector.Y), (double)(vector.X)) + (float)Math.PI / 2;
        }

        public static Vector2 RadialMovement(Vector2 position, Vector2 focus, float speed) // Returns the next position it (position) should be to move towards the focus compared to speed
        {
            float distance = Globals.GetDistance(position, focus); // Getting distance between the position you are in (position) and what ur trying to get to (focus)

            if (distance <= speed) // If the distance is shorter then how fast you can go
            {
                return focus - position; // Just go that smaller distance
            }
            else // If it isnt
            {
                return (focus - position) * speed / distance; // Gets the directional vector and the portion of the distance the speed is and multiplies them so it will move to the directional vector exactly how fast it should (slower then speed - distance of speed) 
            }
        }       
        public static float RotateToward(Vector2 position, Vector2 focus) // Gets the position and where it need to rotate towards and uses x function in math bluh bluh  xxxx + add illustration and equation and explenation
        {
            float angle = (float)Math.Atan2((double)(focus.Y - position.Y), (double)(focus.X - position.X)) + (float)Math.PI / 2;
            return angle;
        }

       
        

        
    }
}
