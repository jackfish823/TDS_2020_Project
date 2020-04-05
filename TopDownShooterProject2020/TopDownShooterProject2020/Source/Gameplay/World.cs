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
    public class World
    {
        public Vector2 offset;

        public MainCharacter mainCharacter;

        public List<BasicProjectile> projectiles = new List<BasicProjectile>(); // List of projectiles
        public World()
        {
            mainCharacter = new MainCharacter("2d\\Idle_gun_000", new Vector2(300, 300), new Vector2(200, 200));

            GameGlobals.PassProjectile = AddProjectile; // Calling the delegate function and sending AddProjectile

            offset = new Vector2(0, 0);
        }

        public virtual void Update()
        {
            this.mainCharacter.Update();

            for(int i = 0; i < this.projectiles.Count; i++) // Running all over the projectiles list, not using for each because i might add stuff later
            {
                this.projectiles[i].Update(offset, null);

                if (this.projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void AddProjectile (object info) // PASS ONLY PROJECTILE else it will break
        {
            this.projectiles.Add((BasicProjectile)info); // if you pass anything to here it will  be casted into a projectile and added to the projectile list
        }

        public virtual void Draw(Vector2 offeset)
        {
            this.mainCharacter.Draw(offeset);


            for (int i = 0; i < this.projectiles.Count; i++) // Running all over the projectiles list, not using for each because i might add stuff later
            {
                this.projectiles[i].Draw(offset);
            }
        }


    }
}
