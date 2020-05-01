#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class Unit : AttackableObject
    {
        private Skill currentSkill;
        protected Vector2 moveTo;
        protected List<Vector2> pathNodes = new List<Vector2>();
        private Inventory inventory;

        public bool added;

        public Inventory Inventory { get => inventory; set => inventory = value; }
        public Skill CurrentSkill { get => currentSkill; set => currentSkill = value; }

        public Unit(string path, Vector2 position, Vector2 dimensions, Vector2 frames, int ownerId) 
            : base (path, position, dimensions, frames, ownerId)
        {
            moveTo = new Vector2(position.X, position.Y);

            inventory = new Inventory(this);

            added = false;
        }
        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {                 
            base.Update(offset, enemy, grid);
        }

        public virtual void AddToInventory(object info)
        {
            Inventory.AddToInventory((InventoryItem)info);
        }

        public virtual List<Vector2> FindPath(SquareGrid grid, Vector2 endSlot) // Here we will also smoothing of the the path
        {
            pathNodes.Clear();

            Vector2 tempStartSlot = grid.GetSlotFromPixel(position, Vector2.Zero);

            List<Vector2> tempPath = grid.GetPath(tempStartSlot, endSlot, true);

            if(tempPath == null || tempPath.Count == 0)
            {

            }

            return tempPath;
        }

        public virtual void MoveUnit()
        {
            if(position.X != moveTo.X || position.Y != moveTo.Y)
            {
                rotation = Globals.RotateToward(position, moveTo);
                position += Globals.RadialMovement(position, moveTo, speed);
            }
            else if(pathNodes.Count > 0) // When we get to the next position 
            {
                moveTo = pathNodes[0];
                pathNodes.RemoveAt(0);

                position += Globals.RadialMovement(position, moveTo, speed);
            }

        }

        public override void Draw(Vector2 offset)
        {

            base.Draw(offset);
        }
    }
}
