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
    public class Unit : Basic2d
    {
        public bool dead;

        public float speed, hitDistance;
        public Unit(string path, Vector2 position, Vector2 dimensions) : base (path, position, dimensions)
        {
            this.dead = false;
            this.speed = 2;
            this.hitDistance = 35.0f;
        }

        public override void Update(Vector2 offset)
        {       
           
            base.Update(offset);
        }

        public virtual void GetHit() // For now if unit get hit it dies
        {
            dead = true;
        }
        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset);
        }
    }
}
