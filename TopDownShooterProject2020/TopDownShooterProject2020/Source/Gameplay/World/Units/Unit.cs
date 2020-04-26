#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooterProject2020.Source.Gameplay.World.Inventory;
#endregion

namespace TopDownShooterProject2020
{
    public class Unit : AttackableObject
    {
        protected Skill currentSkill;
        protected Vector2 moveTo;
        protected List<Vector2> pathNodes = new List<Vector2>();
        private List<Skill> skills = new List<Skill>();

        public bool added;

        public List<Skill> Skills { get => skills; set => skills = value; }

        public List<InventoryItem> inventory = new List<InventoryItem>();

        public Unit(string path, Vector2 position, Vector2 dimensions, Vector2 frames, int ownerId) 
            : base (path, position, dimensions, frames, ownerId)
        {
            moveTo = new Vector2(position.X, position.Y);

            added = false;
        }
        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {                 
            base.Update(offset, enemy, grid);
        }

        public virtual void AddToInventory(object info)
        {
            InventoryItem tempItem = (InventoryItem)info;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (tempItem.name == inventory[i].name)
                {
                    inventory[i].amount++;
                    added = true;
                    break;
                }               
            }
            
            if(!added)
            {
                inventory.Add(tempItem);

            }

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
