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
    public class ExitMenu : BasicMenu
    {
        public int state;

        public KeyBindMenu keyBindMenu;

        public List<BasicButton> buttons = new List<BasicButton>();

        public PassObject Exit, Options;
        public ExitMenu(PassObject Exit)
            : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(400, 550), null)
        {
            state = 0;

            this.background = new Animated2d("2d\\Misc\\Menu1", new Vector2(0, 0), dimensions, Globals.oneFrameOnly, Color.White);

            this.Exit = Exit;
            buttons.Add(new BasicButton("2d\\Misc\\button_grn1", new Vector2(0, 0), new Vector2(200, 40), PathGlobals.ARIAL_FONT, "Return", ReturnClick, PlayState.Play));
            buttons.Add(new BasicButton("2d\\Misc\\button_grn1", new Vector2(0, 0), new Vector2(200, 40), PathGlobals.ARIAL_FONT, "Levels Map", ExitClick, PlayState.LevelsMap));
            buttons.Add(new BasicButton("2d\\Misc\\button_grn1", new Vector2(0, 0), new Vector2(200, 40), PathGlobals.ARIAL_FONT, "Settings", OptionsClick, GameState.OptionMenu));
            buttons.Add(new BasicButton("2d\\Misc\\button_grn1", new Vector2(0, 0), new Vector2(200, 40), PathGlobals.ARIAL_FONT, "Key Binds", KeyBindClick, GameState.OptionMenu));
            buttons.Add(new BasicButton("2d\\Misc\\button_grn1", new Vector2(0, 0), new Vector2(200, 40), PathGlobals.ARIAL_FONT, "Main Menu", OptionsClick, GameState.MainMenu));

            hasCloseButton = false;

            keyBindMenu = new KeyBindMenu(ReturnFromKeyBinds);
        }

        public override void Update()
        {
            if (state == 0)
            {
                base.Update();

                if (Active)
                {
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        buttons[i].Update(topLeft + new Vector2(dimensions.X / 2, 150 + 60 * i));
                    }
                }
            }
            else if (state == 1)
            {
                keyBindMenu.Update();
            }
                 
        }

        public virtual void ExitClick(object info)
        {
            Exit(info);
        }

        public virtual void OptionsClick(object info)
        {
            Globals.gameState = (GameState)info;
        }

        public virtual void ReturnClick(object info)
        {
            Active = false;
        }

        public virtual void KeyBindClick(object info)
        {
            state = 1;
            keyBindMenu.Active = true;
        }

        public virtual void ReturnFromKeyBinds(object info)
        {
            state = 0;

            keyBindMenu.Active = false;
        }

        public override void Draw()
        {
            base.Draw();          

            if(state == 0)
            { 
                if (Active)
                {
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        buttons[i].Draw(topLeft + new Vector2(dimensions.X / 2, 150 + 60 * i));
                    }

                    Globals.CleanShader();
                    string tempString = "Menu";
                    Vector2 strDimensions = font.MeasureString(tempString);
                    Globals.spriteBatch.DrawString(font, tempString, topLeft + new Vector2(background.dimensions.X / 2 - strDimensions.X / 2, 75), Color.GreenYellow);
                }

            }
            else if (state == 1)
            {
                keyBindMenu.Draw();
            }
        }
    }
}
