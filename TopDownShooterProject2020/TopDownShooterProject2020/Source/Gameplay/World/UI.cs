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
    public class UI
    {
        public SpriteFont font; // SpriteFont variable from monogames framework for fonts sprites

        public UI()
        {
            font = Globals.content.Load<SpriteFont>(PathGlobals.ARIAL_FONT);
        }

        public void Update(World world)
        {
            
        }

        public void Draw(World world)
        {
            string killsCountString = $"Kills counter: {world.killsCounter} Kills";

            Globals.spriteBatch.DrawString(font, killsCountString, new Vector2(15, Globals.screenHeight - 35), Color.Red);
            // If you want to print string from the middle and nor left to right, you get dimensions of the string and remove half of it from the place you want to be the middle if th string like below
            // Vector2 stringDimensions = font.MeasureString(tempString);
            // Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth/2 - strDims.X/2, Globals.screenHeight - 40),Color.Red);

        }
    }
}
