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

        public MainCharacter mainCharacter;
        public World()
        {
            mainCharacter = new MainCharacter("2d\\Idle_gun_000", new Vector2(300, 300), new Vector2(200, 200));
        }

        public virtual void Update()
        {
            mainCharacter.Update();
        }

        public virtual void Draw(Vector2 offeset)
        {
            mainCharacter.Draw(offeset);
        }


    }
}
