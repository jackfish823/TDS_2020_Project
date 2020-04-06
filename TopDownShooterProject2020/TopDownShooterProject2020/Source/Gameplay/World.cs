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
        public int killsCounter; // Counts the kills

        public Vector2 offset;

        public MainCharacter mainCharacter;

        public UI ui;

        public List<BasicProjectile> projectiles = new List<BasicProjectile>(); // List of projectiles
        public List<Mob> mobs = new List<Mob>(); // List of mobs
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>(); // List of spawn points
        public World()
        {
            killsCounter = 0;

            this.mainCharacter = new MainCharacter(PathGlobals.MAIN_CHARACTER_TEXTURE, new Vector2(300, 300), new Vector2(200, 200));

            // Calling the Delegate functions and sending the functions from this class
            GameGlobals.PassProjectile = AddProjectile; 
            GameGlobals.PassMob        = AddMob;
            GameGlobals.CheckScroll    = CheckScroll; // #1 delete if camera 


            this.offset = new Vector2(0, 0); // for later


            this.spawnPoints.Add(new SpawnPoint(PathGlobals.ZOMBIE_SPAWN_TEXTURE, new Vector2(62, 79), new Vector2(120, 120))); // Adding spawn point #1
            this.spawnPoints[this.spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

            this.spawnPoints.Add(new SpawnPoint(PathGlobals.ZOMBIE_SPAWN_TEXTURE, new Vector2(576, 67), new Vector2(120, 120))); // Adding spawn point #2

            ui = new UI();
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
                    killsCounter++; // Adds a kill to the counter
                    this.mobs.RemoveAt(i);
                    i--;
                }
            }

            ui.Update(this);
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

        public virtual void CheckScroll (object info) // #1 delete if camera
        {
            Vector2 tempPosition = (Vector2)info;

            if (tempPosition.X < -this.offset.X + (Globals.screenWidth * 0.4f))
            {
                this.offset = new Vector2(offset.X + this.mainCharacter.speed, this.offset.Y);
            }

            if (tempPosition.X > -this.offset.X + (Globals.screenWidth * 0.6f))
            {
                this.offset = new Vector2(offset.X - this.mainCharacter.speed, this.offset.Y);
            }

            if (tempPosition.Y < -this.offset.Y + (Globals.screenHeight * 0.4f))
            {
                this.offset = new Vector2(offset.X, this.offset.Y + this.mainCharacter.speed);
            }

            if (tempPosition.Y > -this.offset.Y + (Globals.screenHeight * 0.6f))
            {
                this.offset = new Vector2(offset.X, this.offset.Y - this.mainCharacter.speed);
            }
        }




        public virtual void Draw(Vector2 offeset) // Drawing all the things in world
        {
            this.mainCharacter.Draw(this.offset);
            
            for (int i = 0; i < this.spawnPoints.Count; i++) // Spawn points
            {
                this.spawnPoints[i].Draw(this.offset);
            }

            for (int i = 0; i < this.projectiles.Count; i++) // Projectiles
            {
                this.projectiles[i].Draw(this.offset);
            }

            for (int i = 0; i < this.mobs.Count; i++) // Mobs
            {
                this.mobs[i].Draw(this.offset);
            }


            ui.Draw(this);
        }


    }
}
