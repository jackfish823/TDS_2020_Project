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
    public class KeyBind
    {
        public string name, key;
        public KeyBind(string name, string key)
        {
            this.name = name;
            this.key = key;
        }

        public string Key
        {
            set
            {
                key = value;
            }
        }

        public virtual XElement ReturnXML()
        {
            XElement xml = new XElement("Key",
                                new XAttribute("name", name),
                                new XElement("value", key));

            return xml;
        }

    }
}
