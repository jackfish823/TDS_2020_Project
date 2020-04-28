#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class GridLocation
    {
        public bool filled, impassable, unPathable, hasBeenUsed, isViewable; // impassable - cannot walk through it and it counts as being filled, unPathable - cant walk through it but not nessecarily counts as being filled
        public float fscore, cost, currentDistance;
        public Vector2 parent, position;

        #region Constructors
        public GridLocation(float cost, bool filled)
        {
            this.cost = cost;
            this.filled = filled;

            unPathable = false;
            impassable = false;
            hasBeenUsed = false;
            isViewable = false;
        }
        public GridLocation(Vector2 position, float cost, bool filled, float fscore)
        {
            this.cost = cost;
            this.filled = filled;
            this.position = position;
            this.fscore = fscore;

            unPathable = false;
            impassable = filled;
            hasBeenUsed = false;
            isViewable = false;
        }
        #endregion



        // Sets the parent, fscore and currentDistance
        public void SetNode(Vector2 parent, float fscore, float currentDistance)
        {
            this.parent = parent;
            this.fscore = fscore;
            this.currentDistance = currentDistance;
        }
        public virtual void SetToFilled(bool impassable)
        {
            filled = true;
            this.impassable = impassable;
        }
        public virtual void SetToNotFilled(bool impassable)
        {
            filled = false;
            this.impassable = impassable;
        }

    }
}
