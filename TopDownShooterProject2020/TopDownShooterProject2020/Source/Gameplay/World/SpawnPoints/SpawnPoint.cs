#region Includes
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace TopDownShooterProject2020
{
    public class SpawnPoint : AttackableObject
    {
        public List<MobChoice> mobChoices = new List<MobChoice>();
        
        public BaseTimer spawnTimer = new BaseTimer(2200); // Timer for the spawns
        public SpawnPoint(string path, Vector2 position, Vector2 dimensions, Vector2 frames, int ownerId, XElement data) 
            : base (path, position, dimensions, frames, ownerId)
        {
            dead = false;
            this.health = 3;
            this.maxHealth = this.health;

            LoadData(data);

            hitDistance = 35.0f;
        }

        public override void Update(Vector2 offset, Player enemy, SquareGrid grid)
        {
            this.spawnTimer.UpdateTimer(); // Updating timer

            if(spawnTimer.Test() && GameGlobals.spawns) // Testing if timer is done
            {
                SpawnMob();
                this.spawnTimer.ResetToZero(); // Resetting the timer
            }
           
            base.Update(offset, enemy, grid);
        }

        public virtual void LoadData(XElement data)
        {
            if(data != null)
            {
                spawnTimer.AddToTimer(Convert.ToInt32(data.Element("timerAdd").Value, Globals.culture));

                List<XElement> mobList = (from t in data.Descendants("mob")
                                            select t).ToList<XElement>();

                for (int i = 0; i < mobList.Count; i++)
                {
                    mobChoices.Add(new MobChoice(mobList[i].Value, Convert.ToInt32(mobList[i].Attribute("rate").Value, Globals.culture)));
                }
            }
        }

        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Zombie(this.position, this.ownerId));
        }
        public override void Draw(Vector2 offeset)
        {
           /* Globals.antiAliasingEffect.Parameters["xSize"].SetValue((float)this.texture.Bounds.Height); // Illustrate this (float) -> .fx file (float)
            Globals.antiAliasingEffect.Parameters["ySize"].SetValue((float)this.texture.Bounds.Width);
            Globals.antiAliasingEffect.Parameters["xDraw"].SetValue((float)((int)this.dimensions.X)); // Concacinate it to int because pixels cant have parts in them (the above they alrady concacenated)
            Globals.antiAliasingEffect.Parameters["yDraw"].SetValue((float)((int)this.dimensions.Y));
            Globals.antiAliasingEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.antiAliasingEffect.CurrentTechnique.Passes[0].Apply();
            */
            base.Draw(offeset);
        }
    }
}
