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
        // Basic offset for drawing
        public Vector2 offset;

        // User Interface
        public UI ui;

        // Players
        public User user;
        public AIPlayer aIPlayer;

        // Projectiles
        public List<BasicProjectile> projectiles = new List<BasicProjectile>(); 

        // Reset World (Delegate) #explain more about delegates#
        PassObject ResetWorld;

        // World Constructor, Gets ResetWorld Function which just creates a new world
        public World(PassObject ResetWorld)
        {
            // Reseting The World (creating new) 
            this.ResetWorld = ResetWorld;

            // Delegates and assigning the functions 
            GameGlobals.PassProjectile = AddProjectile; 
            GameGlobals.PassMob        = AddMob;
            GameGlobals.passSpawnPoint = AddSpawnPoint;
            GameGlobals.CheckScroll    = CheckScroll; // #1 delete when creating camera class


            // Players
            this.user = new User(1);
            this.aIPlayer = new AIPlayer(2);

            // For later, offset now is 0
            this.offset = new Vector2(0, 0); 

            // User Interface
            ui = new UI();
        }

        public virtual void Update()
        {
            // If the main character is not dead, Update
            if (!this.user.mainCharacter.dead)
            {
                // Players
                this.user.Update(this.aIPlayer, offset);
                this.aIPlayer.Update(this.user, offset);
            
                // Projectiles list
                for (int i = 0; i < this.projectiles.Count; i++)
                {                
                    this.projectiles[i].Update(this.offset, this.aIPlayer.units.ToList<Unit>()); // Needs to be changed later, passing generic units list not only AI

                    if (this.projectiles[i].done)
                    {
                        this.projectiles.RemoveAt(i);
                        i--;
                    }
                }

                
            }

            // If the main character is dead, stops updating and displays message
            else
            {          
                if (Globals.keyboard.GetPressed("Enter"))
                {
                    this.ResetWorld(null);
                    GameGlobals.Score = 0;
                }
            }

            // User interface
            ui.Update(this);
        }


        // Delegates
        // Adds mob to the unit list
        public virtual void AddMob(object info) 
        {
            Unit tempUnit = (Unit)info;

            if (this.user.id == tempUnit.ownerId)
            {
                this.user.AddUnit(tempUnit);
            }

            else if (this.aIPlayer.id == tempUnit.ownerId)
            {
                this.aIPlayer.AddUnit(tempUnit);
            }

           // this.aIPlayer.AddUnit((Mob)info); 
        }


        // Adds projectile to the projectiles list
        public virtual void AddProjectile (object info) 
        {
            this.projectiles.Add((BasicProjectile)info); 
        }

        public virtual void AddSpawnPoint(object info)
        {
            SpawnPoint tempSpawnPoint = (SpawnPoint)info;

            if (this.user.id == tempSpawnPoint.ownerId)
            {
                this.user.AddSpawnPoint(tempSpawnPoint);
            }

            else if (this.aIPlayer.id == tempSpawnPoint.ownerId)
            {
                this.aIPlayer.AddSpawnPoint(tempSpawnPoint);
            }
        }


        // Camera scrolling #1 delete when making a camera class
        public virtual void CheckScroll (object info) 
        {
            Vector2 tempPosition = (Vector2)info;

            if (tempPosition.X < -this.offset.X + (Globals.screenWidth * 0.4f))
            {
                this.offset = new Vector2(offset.X + this.user.mainCharacter.speed, this.offset.Y);
            }

            if (tempPosition.X > -this.offset.X + (Globals.screenWidth * 0.6f))
            {
                this.offset = new Vector2(offset.X - this.user.mainCharacter.speed, this.offset.Y);
            }

            if (tempPosition.Y < -this.offset.Y + (Globals.screenHeight * 0.4f))
            {
                this.offset = new Vector2(offset.X, this.offset.Y + this.user.mainCharacter.speed);
            }

            if (tempPosition.Y > -this.offset.Y + (Globals.screenHeight * 0.6f))
            {
                this.offset = new Vector2(offset.X, this.offset.Y - this.user.mainCharacter.speed);
            }
        }



        // Drawing all the things in world, gets offset and draws compared to the offset
        public virtual void Draw(Vector2 offeset) 
        {             
            // Players
            this.user.Draw(this.offset);
            this.aIPlayer.Draw(this.offset);


            // Projectiles 
            for (int i = 0; i < this.projectiles.Count; i++) // Projectiles
            {
                this.projectiles[i].Draw(this.offset);
            }

            // UI
            this.ui.Draw(this);
        }


    }
}
