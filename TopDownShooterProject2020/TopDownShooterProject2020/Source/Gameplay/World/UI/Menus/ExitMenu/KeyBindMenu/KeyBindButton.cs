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
    public class KeyBindButton : BasicButton
    {
        public bool selected;
        public string keyBindString, previousKey;
        public PassObject Updated;
        public KeyBindButton (string path, Vector2 position, Vector2 dimensions, String fontPath, string text, PassObject ButtonClicked, object info, string keyBindString, PassObject Updated) 
            : base (path, position, dimensions, fontPath, text, ButtonClicked, info)
        {
            selected = false;

            this.keyBindString = keyBindString;
            previousKey = "";

            this.Updated = Updated;
        }

        public override void Update(Vector2 offset)
        {
            base.Update(offset);

            if(selected)
            {
                if(Globals.keyboard.pressedKeys.Count > 0)
                {
    
                    SetNew(Globals.keyboard.pressedKeys[0].key);
                }
            }
        }

        public virtual void SetNew(string text)
        {
            this.text = text;

            KeyBind tempKeyBind = GameGlobals.keyBinds.GetKeyBindByName(keyBindString);

            if (tempKeyBind != null)
            {
                previousKey = tempKeyBind.key;
                tempKeyBind.key = text;

                if (Updated != null)
                {
                    Updated(this);
                }

                selected = false;
            }
        }

        public override void RunButtonClick()
        {
            selected = true;

            base.RunButtonClick();
        }

        public override void Draw(Vector2 offeset)
        {
            Globals.CleanShader();
            Vector2 textDimensions = font.MeasureString(keyBindString);
            Globals.spriteBatch.DrawString(font, keyBindString, position + offeset + new Vector2(-200, -textDimensions.Y / 2), Color.GreenYellow);

            base.Draw(offeset);
        }
    }
}
