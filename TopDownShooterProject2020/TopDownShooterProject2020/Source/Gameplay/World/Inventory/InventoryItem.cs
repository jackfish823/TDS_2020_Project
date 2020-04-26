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
using TopDownShooterProject2020.Source.Gameplay.World.Players;
#endregion


namespace TopDownShooterProject2020.Source.Gameplay.World.Inventory
{
    public class InventoryItem
    {
        public Basic2d icon;
        public int amount;
        public string name;
        public InventoryItem(int amount, string name)
        {
            this.amount = amount;
            this.name = name;
        }


    }
}
