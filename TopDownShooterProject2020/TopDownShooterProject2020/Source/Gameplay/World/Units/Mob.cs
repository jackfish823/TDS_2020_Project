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
        public Mob(string path, Vector2 position, Vector2 dimensions) : base(path, position, dimensions)
        {
            speed = 4;
            
        }

        public virtual void Update(Vector2 offset, MainCharacter mainCharacter)
        {

            AI(mainCharacter);

            base.Update(offset);
        }

        public virtual void AI(MainCharacter mainCharacter)
        {
            this.position += Globals.RadialMovement(this.position, mainCharacter.position, this.speed);
            this.rotation = Globals.RotateToward(this.position, mainCharacter.position);
        }

        public override void Draw(Vector2 offeset)
        {
            base.Draw(offeset, new Vector2(246, 392)); // because the texture isnt square the origin is its head
        }
    }
}
