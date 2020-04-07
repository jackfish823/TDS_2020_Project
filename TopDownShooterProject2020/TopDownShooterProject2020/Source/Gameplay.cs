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
        enum GameState
        {
            Play                
        }

        World world; // Main world

        GameState playState = GameState.Play;

        public Gameplay()
        {
            this.playState = GameState.Play; 

            ResetWorld(null);

        }
        public virtual void Update()
        {
            if (playState == GameState.Play)
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
            if (playState == GameState.Play)
            {
                this.world.Draw(Vector2.Zero);
            }

        }

    }
}
