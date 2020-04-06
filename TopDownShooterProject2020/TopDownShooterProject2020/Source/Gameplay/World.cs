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
        public List<Mob> mobs = new List<Mob>(); // List of mobs
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>(); // List of spawn points
        public World()
        {
            this.mainCharacter = new MainCharacter(PathGlobals.MAIN_CHARACTER_TEXTURE, new Vector2(300, 300), new Vector2(200, 200));

            // Delegates
            GameGlobals.PassProjectile = AddProjectile; // Calling the delegate function and sending AddProjectile
            GameGlobals.PassMob = AddMob;

            this.offset = new Vector2(0, 0); // for later


            this.spawnPoints.Add(new SpawnPoint(PathGlobals.ZOMBIE_SPAWN_TEXTURE, new Vector2(62, 79), new Vector2(120, 120))); // Adding spawn point #1
            this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

            this.spawnPoints.Add(new SpawnPoint(PathGlobals.ZOMBIE_SPAWN_TEXTURE, new Vector2(576, 54), new Vector2(120, 120))); // Adding spawn point #2
            
        }
        public virtual void Update()
        {
            this.mainCharacter.Update(this.offset);

            for (int i = 0; i< this.spawnPoints.Count; i++) // Running all over the SpawnPoints list, not using for each because i might add stuff later
            {
                this.spawnPoints[i].Update(this.offset);
            }


            for(int i = 0; i < this.projectiles.Count; i++) // Running all over the projectiles list, not using for each because i might add stuff later
            {
                this.projectiles[i].Update(this.offset, mobs.ToList<Unit>());

                if (this.projectiles[i].done)
                {
                    this.projectiles.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < this.mobs.Count; i++) // Running all over the mobs list, not using for each because i might add stuff later
            {
                this.mobs[i].Update(this.offset, this.mainCharacter);

                if (this.mobs[i].dead)
                {
                    this.mobs.RemoveAt(i);
                    i--;
                }
            }
        }


        // Delegates
        public virtual void AddMob(object info)
        {
            this.mobs.Add((Mob)info);
        }

        public virtual void AddProjectile (object info) // PASS ONLY PROJECTILE else it will break
        {
            this.projectiles.Add((BasicProjectile)info); // if you pass anything to here it will  be casted into a projectile and added to the projectile list
        }

        public virtual void Draw(Vector2 offeset) // Drawing all the things in world
        {
            this.mainCharacter.Draw(offeset);
            
            for (int i = 0; i < this.spawnPoints.Count; i++) // Spawn points
            {
                this.spawnPoints[i].Draw(this.offset);
            }

            for (int i = 0; i < this.projectiles.Count; i++) // Projectiles
            {
                this.projectiles[i].Draw(offset);
            }

            for (int i = 0; i < this.mobs.Count; i++) // Mobs
            {
                this.mobs[i].Draw(offset);
            }

        }


    }
}
