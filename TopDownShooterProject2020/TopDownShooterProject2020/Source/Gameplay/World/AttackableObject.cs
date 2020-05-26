
#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterProject2020.Source.Gameplay.World.Players;
#endregion

namespace TopDownShooterProject2020
{
    public class AttackableObject : Animated2d
    {
        public bool dead, done, throbbing;

        public int ownerId, goldDrop, throbSpeed; // the owner of that object

        public float speed, hitDistance, health, maxHealth;

        public BaseTimer throbTimer = new BaseTimer(500);

        public Color throbColor;

        public AttackableObject(string path, Vector2 position, Vector2 dimensions, Vector2 frames, int ownerId) 
            : base (path, position, dimensions, frames, Color.White)
        {
            this.ownerId = ownerId;
            this.dead = false;
            this.speed = 2;
            this.health = 1;
            this.maxHealth = this.health;
            this.goldDrop = 1;
            this.hitDistance = 35.0f;
            this.throbbing = false;
            throbColor = Color.Red;
            throbSpeed = 1;
        }

        public virtual void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {       
           if(throbbing)
            {
                throbTimer.UpdateTimer();
                if(throbTimer.Test())
                {
                    throbbing = false;
                    throbTimer.ResetToZero();
                }
            }
            base.Update(offset);
        }

        public virtual void GetHit(AttackableObject attacker, float damage) // For now if unit get hit it dies
        {
            this.health -= damage; // Add here armor malipulation ect. todo player stats maybe as object
            throbbing = true;
                       
            throbTimer.ResetToZero();

            if(this.health <= 0)
            {
                dead = true;
                GameGlobals.PassGold(new PlayerValuePacket(attacker.ownerId, goldDrop));
            }
        }
        public override void Draw(Vector2 offeset)
        {
            if(throbbing)
            {
                Globals.throbEffect.Parameters["sinLoc"].SetValue((float)Math.Sin(((float)throbTimer.Timer/(float)throbTimer.Msec + (float)Math.PI)*((float)Math.PI * throbSpeed)));
                Globals.throbEffect.Parameters["filterColor"].SetValue(throbColor.ToVector4());
                Globals.throbEffect.CurrentTechnique.Passes[0].Apply(); 
            }
            else
            {
                Globals.CleanShader();
            }

            base.Draw(offeset);
            Globals.CleanShader();

        }
    }
}
