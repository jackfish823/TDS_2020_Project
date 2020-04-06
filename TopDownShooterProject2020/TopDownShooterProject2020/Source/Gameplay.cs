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
    public class Gameplay
    {
        int playState; // Turn into an enum

        World world; // Main world


        public Gameplay()
        {
            this.playState = 0; 

            ResetWorld(null);

        }
        public virtual void Update()
        {
            if (playState == 0)
            {
                this.world.Update();
            }


        }

        public virtual void ResetWorld(object info)
        {
            world = new World(ResetWorld);
        }

        public virtual void Draw()  
        {
            if (playState == 0)
            {
                this.world.Draw(Vector2.Zero);
            }

        }

    }
}
