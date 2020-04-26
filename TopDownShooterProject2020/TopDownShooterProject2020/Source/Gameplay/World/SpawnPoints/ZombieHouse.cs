#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace TopDownShooterProject2020
{
    public class ZombieHouse : SpawnPoint
    {
        public ZombieHouse(Vector2 position, Vector2 frames, int ownerId, XElement data) 
            : base ("2d\\Misc\\Spawner", position, new Vector2(120, 120), frames, ownerId, data)
        {
            this.health = 15;
            this.maxHealth = this.health;
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {         
            base.Update(offset, enemy, grid);
        }

        public override void SpawnMob()
        {
            int num = Globals.random.Next(0, 100);

            Mob tempMob = null;
            int total = 0;

            for (int i = 0; i < mobChoices.Count; i++)
            {
                total += mobChoices[i].rate;

                if (num < total)
                {
                    Type sType = Type.GetType("TopDownShooterProject2020." + mobChoices[i].mobString, true);

                    tempMob = (Mob)(Activator.CreateInstance(sType, this.position, this.ownerId));
                        
                    break;
                }               
            }
             
            
             


            if (tempMob != null)
            {
                GameGlobals.PassMob(tempMob);
            }
        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}
