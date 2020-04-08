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
    public class AttackableObject : Basic2d
    {
        public bool dead;

        public int ownerId; // the owner of that object

        public float speed, hitDistance, health, maxHealth;
        public AttackableObject(string path, Vector2 position, Vector2 dimensions, int ownerId) : base (path, position, dimensions)
        {
            this.ownerId = ownerId;
            this.dead = false;
            this.speed = 2;
            this.health = 1;
            this.maxHealth = this.health;
            this.hitDistance = 35.0f;
        }

        public virtual void Update(Vector2 offset, Player enemy)
        {       
           
            base.Update(offset);
        }

        public virtual void GetHit(float damage) // For now if unit get hit it dies
        {
            this.health -= damage; // Add here armor malipulation ect. todo player stats maybe as object
            
            if(this.health <= 0)
            {
                dead = true;
            }
        }
        public override void Draw(Vector2 offeset)
        {
            Globals.antiAliasingEffect.Parameters["xSize"].SetValue((float)this.texture.Bounds.Height); // Illustrate this (float) -> .fx file (float)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue((float)this.texture.Bounds.Width);
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue((float)((int)this.dimensions.X)); // Concacinate it to int because pixels cant have parts in them (the above they alrady concacenated)
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue((float)((int)this.dimensions.Y));
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(offeset);
        }
    }
}
