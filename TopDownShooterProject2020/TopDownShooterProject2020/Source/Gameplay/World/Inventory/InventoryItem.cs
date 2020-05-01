#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TopDownShooterProject2020.Source.Gameplay.World.Players;
#endregion


namespace TopDownShooterProject2020
{
    public class InventoryItem
    {
        protected Basic2d icon;
        public int amount;
        private string name;
        protected int price, cooldownMsec;

        public InventoryItem(int amount, string name)
        {
            this.amount = amount;
            this.name = name;
            price = 0;
            cooldownMsec = 0;
        }

        public virtual void Use(MainCharacter mainCharacter)
        {
            mainCharacter.CurrentSkill.Active = true;
            mainCharacter.CurrentSkill.selectionType = 1;
            mainCharacter.skillBar.cooldownTimer.Msec = cooldownMsec;
        }

        public string Name { get => name;  }
        public Basic2d Icon { get => icon;  }
        public int Price { get => price; }

        public virtual XElement ReturnXML()
        {
            XElement xml = new XElement("InventoryItem",
                                new XAttribute("type", this.GetType()),
                                new XElement("amount", amount));

            return xml;
        }
    }
}
