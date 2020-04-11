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
        public SpriteFont arialFont; // SpriteFont variable from monogames framework for fonts sprites

        public QuantityDisplayBar healthBar;
        public UI()
        {
            this.arialFont = Globals.content.Load<SpriteFont>(PathGlobals.ARIAL_FONT);
            this.healthBar = new QuantityDisplayBar(new Vector2(104,16), 2, Color.Green);
        }

        public void Update(World world)
        {
            this.healthBar.Update(world.user.mainCharacter.health, world.user.mainCharacter.maxHealth); // Updating the bar with current health and max health
        }

        public void Draw(World world)
        {
            #region MakeFunction(resets the effect)
            Globals.antiAliasingEffect.Parameters["xSize"].SetValue(1.0f); // For texts we just pass in 1 as value so they wont get changed (1 > 0.6) because texts gets funky when doing this on them (we have to pass all of these for the fx to work so just makr it 1 wont change anything)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue(1.0f); // Although we dont wanna change anything we have to pass everything else
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
            #endregion  
            // Check why the filter sets the color to white, fix with texts


            // Drawing kill counter
            string killsCountString = $"Score: {GameGlobals.Score} Kills";
            Globals.spriteBatch.DrawString(arialFont, killsCountString, new Vector2(Globals.screenWidth - 150, Globals.screenHeight - 38), Color.Red);

            // Drawing Death message
            if (world.user.mainCharacter.dead || world.user.buildings.Count() <= 0)
            {
                string enterToReset = "Youre lost! Press Enter To Restart"; // Can create function for these 3 lines
                Vector2 stringDimensions = arialFont.MeasureString(enterToReset);
                Globals.spriteBatch.DrawString(arialFont, enterToReset, new Vector2(Globals.screenWidth / 2 - stringDimensions.X / 2, Globals.screenHeight/2), Color.Black);
            }

            // Fix text placement to scale (not just numbers)
            // Drawing current weapon
            BasicWeapon tempCurrentWeapon = world.user.mainCharacter.weapons[(int)(world.user.mainCharacter.currentWeaponSlot)];
            string currentWeapon = tempCurrentWeapon.GetType().Name;
            //Globals.spriteBatch.DrawString(arialFont, currentWeapon, new Vector2(150, Globals.screenHeight - 40), Color.Red);            
            tempCurrentWeapon.Draw(new Vector2(140, Globals.screenHeight - 40), new Vector2(0, tempCurrentWeapon.weaponIcon.texture.Height/2));

            // Drawing ammo count
            if (tempCurrentWeapon.reloadTime.Test())
            {
                string currentAmmo = $"{tempCurrentWeapon.currentBullets} / {tempCurrentWeapon.magazineSize}";
                Globals.spriteBatch.DrawString(arialFont, currentAmmo, new Vector2(150 + tempCurrentWeapon.weaponIcon.dimensions.X, Globals.screenHeight - 20 - tempCurrentWeapon.weaponIcon.dimensions.Y / 2), Color.Red);
            }
            else
                Globals.spriteBatch.DrawString(arialFont, "Reloading...", new Vector2(150 + tempCurrentWeapon.weaponIcon.dimensions.X, Globals.screenHeight - 20 - tempCurrentWeapon.weaponIcon.dimensions.Y / 2), Color.Red);



            // Drawing health bar
            healthBar.Draw(new Vector2(20, Globals.screenHeight - 35));
        }
    }
}
