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
            base.Draw(offeset);
        }
    }
}