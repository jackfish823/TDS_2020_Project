#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TopDownShooterProject2020.Source.Gameplay.World.Players;
#endregion


namespace TopDownShooterProject2020
{ 
    public class KeyBindMenu : BasicMenu
    {
        public List<BasicButton> buttons = new List<BasicButton>();

        public List<KeyBindButton> keyBindButtons = new List<KeyBindButton>();

        public PassObject Exit, Options;
        public KeyBindMenu(PassObject Exit)
            : base(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(400, 550), null)
        {
            this.background = new Animated2d("2d\\Misc\\Menu1", new Vector2(0, 0), dimensions, Globals.oneFrameOnly, Color.White);

            this.Exit = Exit;

            hasCloseButton = false;

            for(int i = 0; i <GameGlobals.keyBinds.keyBinds.Count; i++)
            {
                keyBindButtons.Add(new KeyBindButton("2d\\Misc\\button_grn1", new Vector2(0, 0), new Vector2(30, 30), PathGlobals.ARIAL_FONT, GameGlobals.keyBinds.keyBinds[i].key, CheckSelected, null, GameGlobals.keyBinds.keyBinds[i].name, CheckDuplicates));
                keyBindButtons[keyBindButtons.Count - 1].info = keyBindButtons[keyBindButtons.Count - 1];
            }

            buttons.Add(new BasicButton("2d\\Misc\\button_grn1", new Vector2(0, 0), new Vector2(200, 40), PathGlobals.ARIAL_FONT, "Return", ExitClick, PlayState.LevelsMap));
 
        }

        public override void Update()
        {
            base.Update();

            if(Active)
            {
                for (int i = 0; i < keyBindButtons.Count; i++)
                {
                    keyBindButtons[i].Update(topLeft + new Vector2(dimensions.X - 75 - keyBindButtons[i].dimensions.X*1.25f, 150 + 38 * i));
                }

                for (int i = 0; i< buttons.Count; i++)
                {
                    buttons[i].Update(topLeft + new Vector2(dimensions.X/2, dimensions.Y - keyBindButtons[i].dimensions.Y + 60*i));
                }
            }          
        }

        public virtual void ExitClick(object info)
        {
            XDocument keyBindsDoc = new XDocument(new XElement("Root", ""));
            keyBindsDoc.Element("Root").Add(GameGlobals.keyBinds.ReturnXML());

            Globals.save.HandleSaveFormates(keyBindsDoc, "KeyBinds.xml");
            Exit(info);
        }

        public virtual void CheckDuplicates(object info)
        {
            KeyBindButton tempButton = (KeyBindButton)info;

            for (int i = 0; i < keyBindButtons.Count; i++)
            {
                if (keyBindButtons[i] != tempButton && keyBindButtons[i].text == tempButton.text)
                {
                    keyBindButtons[i].SetNew(tempButton.previousKey);

                }
            }
        }
        public virtual void CheckSelected(object info)
        {
            KeyBindButton tempButton = (KeyBindButton)info;

            for (int i = 0; i < keyBindButtons.Count; i++)
            {
                if(keyBindButtons[i] != tempButton)
                {
                    keyBindButtons[i].selected = false;
                }
            }
        }

        public virtual void OptionsClick(object info)
        {
            Globals.gameState = (GameState)info;
        }

        public override void Draw()
        {
            base.Draw();          

            if (Active)
            {
                for (int i = 0; i < keyBindButtons.Count; i++)
                {
                    keyBindButtons[i].Draw(topLeft + new Vector2(dimensions.X - 75 - keyBindButtons[i].dimensions.X * 1.25f, 150 + 38 * i));
                }

                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Draw(topLeft + new Vector2(dimensions.X / 2, dimensions.Y - keyBindButtons[i].dimensions.Y + 60 * i));
                }

                Globals.CleanShader();
                string tempString = "Menu";
                Vector2 strDimensions = font.MeasureString(tempString);
                Globals.spriteBatch.DrawString(font, tempString, topLeft + new Vector2(background.dimensions.X / 2 - strDimensions.X / 2, 75), Color.GreenYellow);
            }         
        }
    }
}
