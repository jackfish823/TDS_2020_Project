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
        public SpriteFont arialFont; // SpriteFont variable from monogames framework for fonts sprites

        public BasicButton resetButton;

        public QuantityDisplayBar healthBar;

        public Basic2d pauseOverlay, goldIcon;

        private WeaponsBar weaponsBar;

        public UI(PassObject ResetWorld, MainCharacter mainCharacter)
        {
            this.pauseOverlay = new Basic2d("2d\\Misc\\pause_overplay", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));
            this.arialFont = Globals.content.Load<SpriteFont>(PathGlobals.ARIAL_FONT);
            this.healthBar = new QuantityDisplayBar(new Vector2(450, 20), 2, Color.White);
            this.goldIcon = new Basic2d("2d\\Misc\\money_icon", new Vector2(Globals.screenWidth - 250, Globals.screenHeight - 30), new Vector2(20 ,20));

            this.resetButton = new BasicButton("2d\\Misc\\Button", new Vector2(0, 0), new Vector2(921/3, 152/3), PathGlobals.ARIAL_FONT, "Restart", ResetWorld, null);

            weaponsBar = new WeaponsBar(mainCharacter, new Vector2(293, 130));
        }

        public void Update(World world)
        {
            this.healthBar.Update(world.user.mainCharacter.health, world.user.mainCharacter.maxHealth); // Updating the bar with current health and max health
            weaponsBar.Update(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 60), world);

            world.user.mainCharacter.skillBar.Update(Vector2.Zero);

            if (world.user.mainCharacter.dead || world.user.buildings.Count() <= 0)
            {              
                resetButton.Update(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));
            }


        }

        public void Draw(World world)
        {
            Globals.CleanShader();

            // Drawing Gold
            goldIcon.Draw(Vector2.Zero);
            string tempString = world.user.mainCharacter.Inventory.Items[0].amount.ToString();
            Globals.spriteBatch.DrawString(arialFont, tempString, new Vector2(goldIcon.position.X + goldIcon.dimensions.X + 10, goldIcon.position.Y - goldIcon.dimensions.Y/2), Color.Red);
            

            // Drawing kill counter
            tempString = $"Score: {GameGlobals.Score} Kills";
            Globals.spriteBatch.DrawString(arialFont, tempString, new Vector2(Globals.screenWidth - 140, goldIcon.position.Y - goldIcon.dimensions.Y / 2), Color.Red);

            // Drawing Death message
            if (world.user.mainCharacter.dead || world.user.buildings.Count() <= 0)
            {
                string enterToReset = "Youre lost! Press Enter To Restart"; // Can create function for these 3 lines
                Vector2 stringDimensions = arialFont.MeasureString(enterToReset);
                Globals.spriteBatch.DrawString(arialFont, enterToReset, new Vector2(Globals.screenWidth / 2 - stringDimensions.X / 2, Globals.screenHeight/2), Color.Black);

                resetButton.Draw(new Vector2(Globals.screenWidth / 2 , Globals.screenHeight / 2 + 100));
            }         

            // Drawing health bar
            healthBar.Draw(new Vector2(Globals.screenWidth/2 - healthBar.BarBackground.dimensions.X / 2, 30));

            weaponsBar.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 90));

            world.user.mainCharacter.skillBar.Draw(Vector2.Zero);


            // Showing paused overlay
            if (GameGlobals.paused)
            {
                Globals.CleanShader();
                pauseOverlay.Draw(Vector2.Zero);
            }
        }
    }
}
