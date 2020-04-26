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
    public class KeyBindList
    {
        public List<KeyBind> keyBinds = new List<KeyBind>();
        public KeyBindList(XDocument Xml)
        {
            List<XElement> bindsXML = (from t in Xml.Descendants("Key")
                                            select t).ToList<XElement>();

            for(int i = 0; i < bindsXML.Count; i++)
            {
                keyBinds.Add(new KeyBind(bindsXML[i].Attribute("name").Value, bindsXML[i].Element("value").Value));
            }
            
        }

        public virtual string GetKeyByName(string name)
        {
            for(int i = 0; i < keyBinds.Count; i++)
            {
                if(keyBinds[i].name == name)
                {
                    return keyBinds[i].key;
                }
            }

            return "";
        }

        public virtual KeyBind GetKeyBindByName(string name)
        {
            for (int i = 0; i < keyBinds.Count; i++)
            {
                if (keyBinds[i].name == name)
                {
                    return keyBinds[i];
                }
            }

            return null;
        }

        public virtual XElement ReturnXML()
        {
            XElement xml = new XElement("Keys", "");

            for(int i = 0; i < keyBinds.Count; i++)
            {
                xml.Add(keyBinds[i].ReturnXML());
            }

            return xml;
        }

    }
}
