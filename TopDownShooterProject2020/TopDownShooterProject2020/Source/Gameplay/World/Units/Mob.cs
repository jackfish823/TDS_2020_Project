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
    public class Mob : Unit
    {
        public Mob(string path, Vector2 position, Vector2 dimensions, int ownerId) : base(path, position, dimensions, ownerId)
        {
            speed = 4;
            
        }

        public override void Update(Vector2 offset, Player enemy)
        {

            AI(enemy.mainCharacter);

            base.Update(offset);
        }

        public virtual void AI(MainCharacter mainCharacter) // a walk and hit character ai (not shoot and stuff) he will walk straight towards him, for shooting can add walk as song as its not x distance from him and if yes stop and shoot of not keep going
        {
            this.position += Globals.RadialMovement(this.position, mainCharacter.position, this.speed);
            this.rotation = Globals.RotateToward(this.position, mainCharacter.position);

            if (Globals.GetDistance(this.position, mainCharacter.position) < 15) // If the mod hits the mainCharacter
            {
                mainCharacter.GetHit(1); // Just an example, can create var inside the mob for its strength
                this.dead = true; 
            }
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset, new Vector2(246, 392)); // because the texture isnt square the origin is its head
        }
    }
}
