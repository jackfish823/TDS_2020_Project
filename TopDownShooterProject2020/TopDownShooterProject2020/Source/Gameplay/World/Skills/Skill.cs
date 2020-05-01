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
    public class Skill // something that affects something else and usually has an effect associated
    {
        protected bool active;
        private bool done;
        public int selectionType;

        protected AttackableObject owner;


        public BasicEffect targetEffect;

        public Skill(AttackableObject owner)          
        {
            active = false;
            Done = false;

            selectionType = 1;

            this.owner = owner;

            targetEffect = new TargetingCircle(new Vector2(0, 0), new Vector2(194, 194));
        }

        #region Properties
    public bool Active 
        { 
            get => active;
            set
            {
                if(value && !active && targetEffect != null) // We pass it true (to be active) and it is not active alreadu
                {
                    targetEffect.Done = false;
                    GameGlobals.PassEffect(targetEffect);
                }
                    active = value;
            }
        }

        public bool Done { get => done; set => done = value; }
        #endregion

        public virtual void Update(Vector2 offset, Player enemy)
        {
            if(active && !Done)
            {             
                Targeting(offset, enemy);
            }
        }

        public virtual void Reset()
        {
            Active = false;
            Done = false;
        }

        public virtual void Targeting(Vector2 offset, Player enemy)
        {
            if(Globals.mouse.LeftClickRelese())
            {
                Active = false;
                Done = false;
            }
        }
    }
}
