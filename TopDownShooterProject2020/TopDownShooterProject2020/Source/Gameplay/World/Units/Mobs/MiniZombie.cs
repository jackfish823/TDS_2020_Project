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

        // Gets a player which it his enemy, goes through all of its buildings and checks if they are turrets, if it finds a turret.  it goes to it and hit it, change it to only priority buildings buy also go for the enemy after (for stuff that doesnt loose the game)
        public override void AI(Player enemy)
        {
            Building temp = null;
            for (int i = 0; i < enemy.buildings.Count(); i++)
            {
                if (enemy.buildings[i].GetType().ToString() == "TopDownShooterProject2020.Turret") // If the enemy.building is turret class, change it in the end (like when we will load the classes form file)
                {
                    temp = enemy.buildings[i];
                }
            }

            if (temp != null)
            {
                this.position += Globals.RadialMovement(this.position, temp.position, this.speed);
                this.rotation = Globals.RotateToward(this.position, temp.position);

                if (Globals.GetDistance(this.position, temp.position) < 15) // If the mod hits the mainCharacter
                {
                    temp.GetHit(1); // Just an example, can create var inside the mob for its Damage
                    this.dead = true;
                }
            }       
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}
