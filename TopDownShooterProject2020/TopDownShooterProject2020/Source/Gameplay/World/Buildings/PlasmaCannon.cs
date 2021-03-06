﻿#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace TopDownShooterProject2020
{
    public class PlasmaCannon : Building
    {
        int range;
        Unit closest = null;
        BaseTimer shootTimer = new BaseTimer(400);
        public PlasmaCannon(Vector2 position, Vector2 frames, int ownerId)
            : base ("2d\\Buildings\\futuristic_cannon", position, new Vector2(840/10f, 1561/10f), frames, ownerId)
        {
            this.range = 350;
            // Make set health func
            this.health = 10;
            this.maxHealth = this.health;
            this.hitDistance = 35.0f;
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            GameGlobals.PassDebugInfo(new CirclePacket(position, range, Color.White));

            if(closest != null)
            {
                GameGlobals.PassDebugInfo(new LinePacket(position, closest.position, Color.GreenYellow));
            }

            shootTimer.UpdateTimer();
            if(shootTimer.Test())
            {
                FireOrb(enemy);
                shootTimer.ResetToZero();
            }

            if (dead)
            {
                Vector2 tempLocation = grid.GetSlotFromPixel(position, Vector2.Zero); // Got the location from pixel
                List<GridLocation> locations = grid.GetSlotsFromLocationAndSize(tempLocation, new Vector2(3,3));
                grid.UnFillBlock(locations);
            }


            base.Update(offset, enemy, grid);
        }

        // Shoots at the closest enemy (AI)
        public virtual void FireOrb(Player enemy)
        {
            closest = null;

            float closestDistance = range, currentDistance = 0;

            for(int i = 0; i < enemy.units.Count; i++)
            {
                currentDistance = Globals.GetDistance(this.position, enemy.units[i].position);

                if(closestDistance > currentDistance)
                {
                    closestDistance = currentDistance;
                    closest = enemy.units[i];
                }
            }

            // If found something in range
            if(closest != null)
            {
                rotation = Globals.RotateToward(position, closest.position);
                GameGlobals.PassDamaginObject(new DarkOrb(this.position, this, closest.position));
                Globals.soundControl.PlaySound("PlasmaShot", true);

            }
        }


        public override void Draw(Vector2 offeset)
        {        
            base.Draw(offeset, new Vector2(420, 1300));
        }
    }
}

