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
    public class FireExplosionItem : InventoryItem 
    {

        public FireExplosionItem(int amount)
            : base(amount, "Fire Explosion")          
        {
            icon = new Basic2d("2d\\Misc\\fire_explosion_ICON", new Vector2(0,0), new Vector2(40, 40));
            price = 20;
            cooldownMsec = 2000;
        }


        public override void Use(MainCharacter mainCharacter)
        {

                mainCharacter.CurrentSkill = new FireExplosion(mainCharacter);
                base.Use(mainCharacter);
            
        }
    }
}
