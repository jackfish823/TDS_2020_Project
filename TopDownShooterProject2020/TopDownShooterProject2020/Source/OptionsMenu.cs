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
using TopDownShooterProject2020.Source.Engine.DataTypes.Forms;
#endregion

namespace TopDownShooterProject2020
{

    public class OptionsMenu
    {
        BasicButton exitButton, saveButton, backButton;
        Basic2d background;
        public List<ArrowSelector> arrowSelectors = new List<ArrowSelector>();

        SpriteFont font;

        public GameState prevGameState;

        PassObject ApplyOptions;
        public OptionsMenu(PassObject ApplyOptions, GameState prevGameState)
        {
            this.ApplyOptions = ApplyOptions; 
            exitButton = new BasicButton("2d\\Misc\\Button_grn1", new Vector2(Globals.screenWidth/2, Globals.screenHeight - 100), new Vector2(272/1.5f, 65/1.5f), PathGlobals.ARIAL_FONT, "Exit", ExitClick, null);
            saveButton = new BasicButton("2d\\Misc\\Button_grn1", new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 170), new Vector2(272 / 1.5f, 65 / 1.5f), PathGlobals.ARIAL_FONT, "Save", SaveClick, null);
            backButton = new BasicButton("2d\\Misc\\Button_grn1", new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 240), new Vector2(272 / 1.5f, 65 / 1.5f), PathGlobals.ARIAL_FONT, "Back", BackClick, null);

            this.prevGameState = prevGameState;

            font = Globals.content.Load<SpriteFont>(PathGlobals.ARIAL_FONT);

            arrowSelectors.Add(new ArrowSelector(new Vector2(Globals.screenWidth/2, 300), new Vector2(128, 32), "Full Screen"));
            arrowSelectors[arrowSelectors.Count - 1].AddOption(new FormOption("No", 0));
            arrowSelectors[arrowSelectors.Count - 1].AddOption(new FormOption("Yes", 1));

            arrowSelectors.Add(new ArrowSelector(new Vector2(Globals.screenWidth / 2, 400), new Vector2(128, 32), "Music Volume"));
            for(int i = 0; i<31; i++)
            {
                arrowSelectors[arrowSelectors.Count - 1].AddOption(new FormOption("" + i, i));
            }
            arrowSelectors[arrowSelectors.Count - 1].selected = (int)arrowSelectors[arrowSelectors.Count - 1].options.Count-1;

            arrowSelectors.Add(new ArrowSelector(new Vector2(Globals.screenWidth / 2, 500), new Vector2(128, 32), "Sound Volume"));
            for (int i = 0; i < 31; i++)
            {
                arrowSelectors[arrowSelectors.Count - 1].AddOption(new FormOption("" + i, i));
            }
            arrowSelectors[arrowSelectors.Count - 1].selected = (int)arrowSelectors[arrowSelectors.Count - 1].options.Count - 1;
            background = new Basic2d("2d\\Misc\\background2", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(Globals.screenWidth, Globals.screenHeight));

            XDocument xml = Globals.save.GetFile("XML\\options.xml");

            LoadData(xml);
        }

        public virtual void Update()
        {
            for(int i = 0; i < arrowSelectors.Count; i++)
            {
                arrowSelectors[i].Update(Vector2.Zero);
            }

            saveButton.Update(Vector2.Zero);

            exitButton.Update(Vector2.Zero);

            if(prevGameState == GameState.Game)
            {
                backButton.Update(Vector2.Zero);
            }
        }

        public virtual void ExitClick(object info)
        {
            Globals.gameState = GameState.MainMenu;
            
        }
        public virtual void BackClick(object info)
        {
            Globals.gameState = GameState.Game;
        }
        public virtual void SaveClick(object info)
        {
            SaveOptions();
        }

        public virtual FormOption GetOptionValue(string name)
        {
            for(int i = 0; i< arrowSelectors.Count; i++)
            {
                if(arrowSelectors[i].title == name)
                {
                    return arrowSelectors[i].GetCurrentOption();
                }
            }

            return null;
        }

        public virtual void LoadData (XDocument data)
        {
            if(data != null)
            {
                List<string> allOptions = new List<string>();

                for(int i = 0; i < arrowSelectors.Count; i++)
                {
                    allOptions.Add(arrowSelectors[i].title);
                }

                for(int i = 0; i < allOptions.Count; i++)
                {
                    List<XElement> OptionList = (from t in data.Element("Root").Element("Options").Descendants("Option")
                                                 where t.Element("name").Value == allOptions[i]
                                                 select t).ToList<XElement>();

                    if (OptionList.Count > 0)
                    {
                        for (int j = 0; j < arrowSelectors.Count; j++)
                        {
                            if (arrowSelectors[j].title == allOptions[i])
                            {
                                arrowSelectors[j].selected = Convert.ToInt32(OptionList[0].Element("selected").Value, Globals.culture);
                            }
                        }
                    }
                }

                
            }
        }

        public virtual void SaveOptions()
        {
            XDocument xml = new XDocument(new XElement("Root",
                                                    new XElement("Options", "")));

            for(int i = 0; i < arrowSelectors.Count; i++)
            {
                xml.Element("Root").Element("Options").Add(arrowSelectors[i].ReturnXML());
            }

            Globals.save.HandleSaveFormates(xml, "options.xml");

            ApplyOptions(null);
        }

        public virtual void Draw()
        {
            background.Draw(Vector2.Zero);

            Vector2 strDimesions = font.MeasureString("Options");
            Globals.spriteBatch.DrawString(font, "Options", new Vector2(Globals.screenWidth / 2 - strDimesions.X / 2, 100), Color.GreenYellow);

            saveButton.Draw(Vector2.Zero);
            exitButton.Draw(Vector2.Zero);

            if (prevGameState == GameState.Game)
            {
                backButton.Draw(Vector2.Zero);
            }

            for (int i = 0; i < arrowSelectors.Count; i++)
            {
                arrowSelectors[i].Draw(Vector2.Zero, font);
            }


        }

    }
}
