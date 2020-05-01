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
    public class SuperChargeItem : InventoryItem 
    {

        public SuperChargeItem(int amount)
            : base(amount, "Supercharge")          
        {
            icon = new Basic2d("2d\\Icons\\super_charge_icon", new Vector2(0,0), new Vector2(40, 40));
            price = 150;
            cooldownMsec = 5000;

        }


        public override void Use(MainCharacter mainCharacter)
        {         

                    mainCharacter.CurrentSkill = new Supercharge(mainCharacter);
                    base.Use(mainCharacter);
                
                        
        }
    }
}
