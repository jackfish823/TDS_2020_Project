#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace TopDownShooterProject2020
{
    public class HealthKitItem : InventoryItem 
    {

        public HealthKitItem(int amount)
            : base(amount, "Health Kit")          
        {
            icon = new Basic2d("2d\\Icons\\health_kit_icon", new Vector2(0,0), new Vector2(40, 40));
            price = 50;
            cooldownMsec = 2000;

        }


        public override void Use(MainCharacter mainCharacter)
        {
            Globals.soundControl.PlaySound("Heal", true);

            mainCharacter.CurrentSkill = new Heal(mainCharacter);
                    base.Use(mainCharacter);
                
                        
        }
    }
}
