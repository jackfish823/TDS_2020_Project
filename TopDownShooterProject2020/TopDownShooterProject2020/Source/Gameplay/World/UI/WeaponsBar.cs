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
    public class WeaponsBar
    {
        private Basic2d bar;
        private MainCharacter mainCharacter;
        private SpriteFont arialFont;

        private BasicWeapon currentWeapon;
        private BasicButton inventoryButton;

        private World lastWorld;

        public WeaponsBar(MainCharacter mainCharacter, Vector2 barSize)
        {
            this.mainCharacter = mainCharacter;
            this.arialFont = Globals.content.Load<SpriteFont>(PathGlobals.ARIAL_FONT);

            bar = new Basic2d("2d\\Misc\\weapons_bar", new Vector2(0, 0), barSize);


            inventoryButton = new BasicButton("2d\\Icons\\backpack_icon2", new Vector2(0, 0), new Vector2(70, 70), PathGlobals.ARIAL_FONT, "", ToggleCharacterMenu, null);
      
        }

        public void Update(Vector2 offset, World world)
        {
            currentWeapon = mainCharacter.weapons[(int)mainCharacter.currentWeaponSlot];
            lastWorld = world;

            inventoryButton.Update(offset + new Vector2(+97.5f, -12));

        }

        private void ToggleCharacterMenu(object info)
        {
            lastWorld.characterMenu.Active = !lastWorld.characterMenu.Active;
        }
        public void Draw(Vector2 offset)
        {
            if(currentWeapon != null)
            {
                bar.Draw(offset);
                currentWeapon.weaponIcon.Draw(offset);

                string tempStr;
                Vector2 strDims;
                if (currentWeapon.reloadTime.Test())
                {
                    tempStr = $"{currentWeapon.currentBullets}";
                    strDims = arialFont.MeasureString(tempStr);
                    Globals.spriteBatch.DrawString(arialFont, tempStr, offset + new Vector2(-97.5f - strDims.X / 2, -12), Color.Red);
                }
                else
                {
                    tempStr = "Reloading...";
                    strDims = arialFont.MeasureString(tempStr);
                    Globals.spriteBatch.DrawString(arialFont, "Reloading...", offset + new Vector2(-97.5f - strDims.X / 2, -12), Color.Red);
                }

                inventoryButton.Draw(offset + new Vector2(+99f, 0));

            }
                  
        }
    }
}
