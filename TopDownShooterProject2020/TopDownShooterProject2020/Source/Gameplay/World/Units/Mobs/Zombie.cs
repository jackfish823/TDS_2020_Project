﻿#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class Zombie : Mob
    {
        public Zombie(Vector2 position, int ownerId) 
            : base("2d\\Units\\Mobs\\lvl1_zombie_Walk_000", position, new Vector2(150, 150), ownerId)
        {
          
            this.speed = 2;
            
        }

        public override void Update(Vector2 offset, Player enemy)
        {
            base.Update(offset, enemy);
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}
