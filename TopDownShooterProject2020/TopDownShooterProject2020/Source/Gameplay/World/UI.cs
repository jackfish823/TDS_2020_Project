﻿#region Includes
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

        public QuantityDisplayBar healthBar;
        public UI()
        {
            this.font = Globals.content.Load<SpriteFont>(PathGlobals.ARIAL_FONT);
            this.healthBar = new QuantityDisplayBar(new Vector2(104,16), 2, Color.Green);
        }

        public void Update(World world)
        {
            this.healthBar.Update(world.user.mainCharacter.health, world.user.mainCharacter.maxHealth); // Updating the bar with current health and max health
        }

        public void Draw(World world)
        {
            #region MakeFuncOfThat 
            //* If you want to print string from the middle and nor left to right, you get dimensions of the string and remove half of it from the place you want to be the middle if th string like below
            // Vector2 stringDimensions = font.MeasureString(tempString);
            // Globals.spriteBatch.DrawString(font, tempString, new Vector2(Globals.screenWidth/2 - strDims.X/2, Globals.screenHeight - 40),Color.Red); */
            #endregion

            // Drawing kill counter
            string killsCountString = $"Score: {GameGlobals.Score} Kills";
            Globals.spriteBatch.DrawString(font, killsCountString, new Vector2(Globals.screenWidth - 200, Globals.screenHeight - 35), Color.Red);

            // Drawing health bar
            healthBar.Draw(new Vector2(20, Globals.screenHeight - 40));

            // Drawing Death message
            if (world.user.mainCharacter.dead)
            {
                string enterToReset = "Youre Dead! xD xD LMAO, Press Enter To Restart"; // Can create function for these 3 lines
                Vector2 stringDimensions = font.MeasureString(enterToReset);
                Globals.spriteBatch.DrawString(font, enterToReset, new Vector2(Globals.screenWidth / 2 - stringDimensions.X / 2, Globals.screenHeight/2), Color.Red);
            }
        }
    }
}
