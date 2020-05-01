#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class BasicButton : Basic2d
    {
        public bool isPressed, isHovered; // create enum
        public string text;

        public Color hoverColor;

        public SpriteFont font;

        public object info;

        public PassObject ButtonClicked;

        public Color textColor;

        bool hovertest = false;



        public BasicButton(string path, Vector2 position, Vector2 dimensions, String fontPath, string text, PassObject ButtonClicked, object info) 
            : base (path, position, dimensions)
        {
            this.info = info;
            this.text = text;
            this.ButtonClicked = ButtonClicked;

            if (fontPath != "")
            {
                this.font = Globals.content.Load<SpriteFont>(fontPath);
            }

            this.isPressed = false;
            this.hoverColor = new Color(199, 255, 212);



            textColor = Color.Black;
        }

        public BasicButton(string path, Vector2 position, Vector2 dimensions, String fontPath, string text, PassObject ButtonClicked, object info, Color textColor)
            : base(path, position, dimensions)
        {
            this.info = info;
            this.text = text;
            this.ButtonClicked = ButtonClicked;

            if (fontPath != "")
            {
                this.font = Globals.content.Load<SpriteFont>(fontPath);
            }

            this.isPressed = false;
            this.hoverColor = new Color(199, 255, 212);


            

            this.textColor = textColor;
        }

        public override void Update(Vector2 offset)
        {
            if(Hover(offset))
            {
                if (!hovertest)
                {
                    Globals.soundControl.PlaySound("MenuButtonHover", true);
                    hovertest = true;
                }
                this.isHovered = true;

                if(Globals.mouse.LeftClick())
                {
                    Globals.soundControl.PlaySound("MenuButtonPress", true);

                    this.isHovered = false;
                    this.isPressed = true;
                }
                else if(Globals.mouse.LeftClickRelese())
                {
                    RunButtonClick();
                }


            }
            else
            {
                hovertest = false;
                this.isHovered = false;
            }
           
            if (!Globals.mouse.LeftClick() && !Globals.mouse.LeftClickHold())
            {
                
                this.isPressed = false;
            }

            base.Update(offset);
        }

        public virtual void Reset()
        {
            isPressed = false;
            isHovered = false;
        }

        public virtual void RunButtonClick()
        {
            if(ButtonClicked != null)
            {
                ButtonClicked(info);
            }

            Reset();
        }
        public override void Draw(Vector2 offeset)
        {
            Color tempColor = Color.White;

            if(isPressed)
            {
                tempColor = Color.Gray;
            }
            else if(isHovered)
            {
                tempColor = hoverColor;
            }

            Globals.antiAliasingEffect.Parameters["xSize"].SetValue(1.0f); // For texts we just pass in 1 as value so they wont get changed (1 > 0.6) because texts gets funky when doing this on them (we have to pass all of these for the fx to work so just makr it 1 wont change anything)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue(1.0f); // Although we dont wanna change anything we have to pass everything else
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(tempColor.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(offeset);

            if(font != null)
            {
                Globals.CleanShader();
                Vector2 textDimensions = font.MeasureString(text);
                Globals.spriteBatch.DrawString(font, text, position + offeset + new Vector2(-textDimensions.X / 2, -textDimensions.Y / 2), textColor);
            }
           // Globals.CleanShader();
        }
    }
}
