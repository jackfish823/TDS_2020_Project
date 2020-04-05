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

       
    }
}
