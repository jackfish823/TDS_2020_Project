#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace TopDownShooterProject2020
{
    public class PlasmaCannonItem : InventoryItem 
    {

        public PlasmaCannonItem(int amount)
            : base(amount, "Plasma Cannon")          
        {
            icon = new Basic2d("2d\\Icons\\turret_icon", new Vector2(0,0), new Vector2(40, 40));
            price = 10;
        }


        public void Use(Vector2 offset, SquareGrid grid, int ownerId)
        {
            Vector2 tempLocation = grid.GetSlotFromPixel(Globals.mouse.newMousePosition, -offset); // Got the location from pixel
            List<GridLocation> locations = grid.GetSlotsFromLocationAndSize(tempLocation, new Vector2(3, 3));

            if (locations != null && !grid.CheckBlockFilled(locations) && !grid.CheckBlockImpassable(locations))
            {
                grid.FillBlock(locations);

                Building tempBuilding = new PlasmaCannon(new Vector2(0, 0), Globals.oneFrameOnly, ownerId);

                tempBuilding.position = grid.GetPositionFromLocation(tempLocation) + grid.slotDimensions / 2;
                //tempBuilding.position = grid.GetPositionFromLocation(tempLocation) + grid.slotDimensions / 2 + new Vector2(0, -tempBuilding.dimensions.Y * .25f);

                GameGlobals.PassBuilding(tempBuilding);
            }
        }
    }
}
