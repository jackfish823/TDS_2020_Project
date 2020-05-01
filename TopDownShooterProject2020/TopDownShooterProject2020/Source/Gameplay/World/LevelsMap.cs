#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TopDownShooterProject2020.Source.Engine.Output;
#endregion


namespace TopDownShooterProject2020
{
    public class LevelsMap
    {
        public Basic2d background;

        public List<BasicButton> levels = new List<BasicButton>();

        public PassObject ChangeGameLevel;

        private int latestUnlockedLevel;

        public LevelsMap(PassObject ChangeGameLevel)
        {
            this.ChangeGameLevel = ChangeGameLevel;

            background = new Basic2d("2d\\background2", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));

            LoadData();
        }

        public virtual void Update()
        {
            for (int i = 0; i < levels.Count; i++)
            {
                levels[i].Update(Vector2.Zero);
            }
        }

        public virtual void LevelClicked(object info)
        {
            //int levelID = (int)info;
            if (latestUnlockedLevel == Convert.ToInt32(info, Globals.culture))
            {
                ChangeGameLevel(info);
            }
            else if (latestUnlockedLevel + 1 == Convert.ToInt32(info, Globals.culture))
            {
                Globals.messageList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200), new Vector2(500, 60), "Level Is Locked!", 1000, Color.LightSeaGreen, false));
            }
            else
            {
                Globals.messageList.Add(new Message(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200), new Vector2(500, 60), "You've Already Done This Level", 1000, Color.LightSeaGreen, false));
            }

        }
        public virtual void LoadData()
        {
            XDocument xml;

            if (File.Exists(Globals.appDataFilePath + "\\" + Globals.save.gameName + "\\XML\\LevelsSave.xml"))
            {
                xml = Globals.save.GetFile("\\XML\\LevelsSave.xml");
            }
            else
            {
                // make the file - start xml (basic) (default)
                XDocument keyBindXML = XDocument.Parse("<Root><Levels>" +
                    "<Level id=\"0\"><name>A Fresh Start</name><file>Level1</file><Position><x>250</x><y>250</y></Position><passed>true</passed></Level>" +
                    "<Level id=\"1\"><name>Small Steps</name><file>Level2</file><Position><x>500</x><y>350</y></Position><passed>false</passed></Level>" +
                    "<Level id=\"2\"><name>Starting To Be Good Huh?</name><file>Level3</file><Position><x>650</x><y>300</y></Position><passed>false</passed></Level>" +
                    "<Level id=\"3\"><name>About To Be There!</name><file>Level4</file><Position><x>800</x><y>400</y></Position><passed>false</passed></Level>" +
                    "<Level id=\"4\"><name>The End</name><file>Level5</file><Position><x>1100</x><y>450</y></Position><passed>false</passed></Level>" +
                    "</Levels></Root>");

                // save the file
                Globals.save.HandleSaveFormates(keyBindXML, "LevelsSave.xml");

                // load the file
                xml = Globals.save.GetFile("\\XML\\LevelsSave.xml");

            }

            List<XElement> levelList = (from t in xml.Descendants("Level")
                                        select t).ToList<XElement>();

            for (int i = 0; i < levelList.Count; i++)
            {
                string levelIconPath = "";
                Vector2 levelIconSize = new Vector2();

                if (bool.Parse(levelList[i].Element("passed").Value))
                {
                    levelIconPath = "2d\\Misc\\shieldIcon";
                    levelIconSize = new Vector2(69, 85);
                }
                else if ((i - 1) >= 0)
                {
                    if (bool.Parse(levelList[i - 1].Element("passed").Value))
                    {
                        levelIconPath = "2d\\Misc\\current_level";
                        levelIconSize = new Vector2(77, 77);
                        latestUnlockedLevel = i;
                    }
                    else
                    {
                        levelIconPath = "2d\\Misc\\locket_level";
                        levelIconSize = new Vector2(77, 77);
                    }
                }


                levels.Add(new BasicButton(levelIconPath, new Vector2(Convert.ToInt32(levelList[i].Element("Position").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[i].Element("Position").Element("y").Value, Globals.culture)), levelIconSize, PathGlobals.ARIAL_FONT, "", LevelClicked, levelList[i].Attribute("id").Value));

            }
        }

        public virtual void Draw()
        {
            background.Draw(Vector2.Zero);

            for (int i = 0; i < levels.Count; i++)
            {
                levels[i].Draw(Vector2.Zero);
            }
        }

    }
}
