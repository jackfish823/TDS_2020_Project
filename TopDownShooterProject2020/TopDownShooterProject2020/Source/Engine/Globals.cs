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
using System.Runtime.CompilerServices;
using TopDownShooterProject2020.Source.Engine.Output;
#endregion

namespace TopDownShooterProject2020
{
    public delegate void PassObject(object i); // Passing an object and doesnt return anything (object can by anything)
    public delegate object PassOnjectAndReturn(object i); // Passing an object and returning an object too (object can by anything)
    public enum GameState { MainMenu, Game, OptionMenu };

    public class Globals
    {
        public static GameState gameState = GameState.MainMenu;

        public static int screenHeight, screenWidth; // Creatign a global variable for the screen height and width

        public static Random random = new Random(); // Creating global random variable

        public static ContentManager content; // Creating global content variable
        public static SpriteBatch spriteBatch; // Creating global spriteBatch variable

        public static Effect antiAliasingEffect, throbEffect, lightEffect; // Basically we have 3 main places where we draw things: UI, BasicProjectile and AttackableObject (fathers), so there is where we gonna add it

        public static Save save;

        public static string appDataFilePath;

        public static OptionsMenu optionsMenu;

        public static Vector2 baseVector = new Vector2(0, -1);
        public static Vector2 oneFrameOnly = new Vector2(1, 1);

        public static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");

        public static List<Message> messageList = new List<Message>();

        public static bool toggleLinesDebug = false;

        // inputs
        public static BaseKeyboard keyboard; // Creating global keyboard variable
        public static BaseMouse mouse; // Creating global mouse variable
        public static SoundControl soundControl;


        public static GameTime gameTime; // Creating global game time variable


        public static float GetDistance(Vector2 position, Vector2 target) // Finding distance between 2 positions (vectors)
        {
            return (float)Math.Sqrt(Math.Pow(position.X - target.X, 2) + Math.Pow(position.Y - target.Y, 2)); // Basic equation for finding distance between 2 points the distance formula
        }
        public static float GetLength(Vector2 vector)
        {
            return (float)Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2)); 
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
        public static void DrawLine(Texture2D texture, Vector2 start, Vector2 end, Color color, Vector2 offset)
        {
            start = new Vector2(start.X + offset.X, start.Y + offset.Y);

            end = new Vector2(end.X + offset.X, end.Y + offset.Y);

            spriteBatch.Draw(texture, start, null, color,
                             (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                             new Vector2(0f, (float)texture.Height / 2),
                             new Vector2(Vector2.Distance(start, end)/2, 1f),
                             SpriteEffects.None, 0f);
        }
        // FROM HERE ITS TO DRAW CIRCLE , FIX
        public static void DrawLineSegment(Texture2D texture, Vector2 point1, Vector2 point2, Color color, int lineWidth)
        {

            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            spriteBatch.Draw(texture, point1, null, color,
            angle, Vector2.Zero, new Vector2(length, (float)lineWidth/2),
            SpriteEffects.None, 0f);
        }
        public static void DrawPolygon(Texture2D texture, Vector2[] vertex, int count, Color color, int lineWidth)
        {
            if (count > 0)
            {
                for (int i = 0; i < count - 1; i++)
{
                    DrawLineSegment(texture, vertex[i], vertex[i + 1], color, lineWidth);
                }
                DrawLineSegment(texture, vertex[count - 1], vertex[0], color, lineWidth);
            }
        }
        public static void DrawCircle(Texture2D texture, Vector2 center, float radius, Color color, int lineWidth, int segments, Vector2 offset)
        {
            center = center + offset;           

            Vector2[] vertex = new Vector2[segments];

            double increment = Math.PI * 2.0 / segments;
            double theta = 0.0;

            for (int i = 0; i < segments; i++)
            {
                vertex[i] = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                theta += increment;
            }

            DrawPolygon(texture, vertex, segments, color, lineWidth);
        }


        public static void CleanShader()
        {
            Globals.antiAliasingEffect.Parameters["xSize"].SetValue(1.0f); // For texts we just pass in 1 as value so they wont get changed (1 > 0.6) because texts gets funky when doing this on them (we have to pass all of these for the fx to work so just makr it 1 wont change anything)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue(1.0f); // Although we dont wanna change anything we have to pass everything else
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
