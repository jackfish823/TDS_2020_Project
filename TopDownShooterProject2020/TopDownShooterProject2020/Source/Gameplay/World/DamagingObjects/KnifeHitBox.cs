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
    public class KnifeHitBox : MeleeBox // inhereting from BasicProjectile
    {
       
        public KnifeHitBox(Vector2 position, AttackableObject owner) 
            : base(position, owner)
        {
           
        }

        public override void Update(Vector2 offset, List<AttackableObject> objects) //objects for short (attackble objects)
        {
            base.Update(offset, objects);
        }
      
    
        public override void Draw(Vector2 offset)
        {            
            base.Draw(offset, new Vector2(this.texture.Width / 2, this.texture.Height));
        }
    }
}   


