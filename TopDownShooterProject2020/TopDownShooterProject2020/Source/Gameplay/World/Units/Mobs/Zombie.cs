#region Includes
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
        public Zombie(Vector2 position) 
            : base("2d\\Units\\Mobs\\zombie_walk_000", position, new Vector2(200, 200))
        {
          
            this.speed = 3;
            
        }

        public override void Update(Vector2 offset, MainCharacter mainCharacter)
        {
            base.Update(offset, mainCharacter);
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}
