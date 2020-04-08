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
    public class MiniZombie : Mob
    {
        public BaseTimer spawnTimer;
        public MiniZombie(Vector2 position, int ownerId) 
            : base("2d\\Units\\Mobs\\zombie_walk_000", position, new Vector2(100, 100), ownerId)
        {          
            this.speed = 2.5f;
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
