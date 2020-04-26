#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterProject2020.Source.Gameplay.World.Inventory;
using TopDownShooterProject2020.Source.Gameplay.World.Players;
#endregion

namespace TopDownShooterProject2020
{
    public class Gold : InventoryItem 
    {

        public Gold(int amount, string name)
            : base(amount, name)          
        {
            icon = new Basic2d("2d\\Misc\\money_icon", new Vector2(Globals.screenWidth - 250, Globals.screenHeight - 30), new Vector2(20, 20));
        }

    }
}
