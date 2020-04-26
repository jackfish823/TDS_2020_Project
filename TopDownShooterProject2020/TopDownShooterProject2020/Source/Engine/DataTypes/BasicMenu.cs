#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterProject2020.Source.Gameplay.World.Players;
#endregion


namespace TopDownShooterProject2020
{
    public class BasicMenu
    {
        protected bool active, hasCloseButton;

        public Vector2 position, dimensions, topLeft;

        public Animated2d background;

        public BasicButton closeButton;

        public SpriteFont font;

        public PassObject CloseAction;

        public BasicMenu(Vector2 position, Vector2 dimensions, PassObject CloseAction)
        {
            this.position = position;
            this.dimensions = dimensions;
            this.CloseAction = CloseAction;

            hasCloseButton = true;

            background = new Animated2d("2d\\Misc\\background2", new Vector2(0, 0), dimensions, Globals.oneFrameOnly, Color.White);
            closeButton = new BasicButton("2d\\Misc\\exit_icon", new Vector2(background.dimensions.X/2, -background.dimensions.Y/2), new Vector2(30, 30), "", "", Close, null);
            font = Globals.content.Load<SpriteFont>(PathGlobals.ARIAL_FONT);
        }

        #region Properties
        public virtual bool Active { get => active; set => active = value; }



        #endregion

        public virtual void Update()
        {
            if(Active)
            {
                topLeft = position - dimensions / 2;

                if (hasCloseButton)
                {
                    closeButton.Update(position);
                }
            }
        }

        public virtual void Close(object info)
        {
            Active = false;
        }

        public virtual void Draw()
        {
            if(Active)
            {
                background.Draw(position);
                if(hasCloseButton)
                {
                    closeButton.Draw(position);
                }
            }
        }
    }
}
