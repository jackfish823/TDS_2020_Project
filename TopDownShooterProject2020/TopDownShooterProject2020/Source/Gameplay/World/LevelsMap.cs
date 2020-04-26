#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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

        public LevelsMap(PassObject ChangeGameLevel)
        {
            this.ChangeGameLevel = ChangeGameLevel;

            background = new Basic2d("2d\\background2", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(Globals.screenWidth, Globals.screenHeight));

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
            ChangeGameLevel(info);
        }
        public virtual void LoadData()
        {   
            XDocument xml = XDocument.Load("XML\\Levels.xml");

            List<XElement> levelList = (from t in xml.Descendants("Level")
                                            select t).ToList<XElement>();

            for (int i = 0; i < levelList.Count; i++)
            {
                levels.Add(new BasicButton("2d\\Misc\\shieldIcon", new Vector2(Convert.ToInt32(levelList[i].Element("Position").Element("x").Value, Globals.culture), Convert.ToInt32(levelList[i].Element("Position").Element("y").Value, Globals.culture)), new Vector2(69, 85), PathGlobals.ARIAL_FONT, levelList[i].Attribute("id").Value, LevelClicked, levelList[i].Attribute("id").Value));
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
