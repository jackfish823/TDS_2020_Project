#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TopDownShooterProject2020.Source.Engine.DataTypes.Forms;
#endregion


namespace TopDownShooterProject2020
{ 
    public class DismissibleMessage : Message
    {
        public Basic2d background;

        public BasicButton button;

        public PassObject ConfirmFunction;


        public DismissibleMessage(Vector2 position, Vector2 dimensions, string message, Color color, bool lockScreen, PassObject confirmFunction) 
            : base(position, dimensions, message, 1000, color, lockScreen)
        {
            background = new Basic2d("2d\\Misc\\background2", position, dimensions);
            button = new BasicButton("2d\\Misc\\button_grn1", new Vector2(position.X, position.Y + dimensions.Y/2 - 30), new Vector2(70, 35), PathGlobals.ARIAL_FONT, "Ok", new PassObject(CompleteClick), null);


            this.ConfirmFunction = confirmFunction;
        }

        public override void Update()
        {
            button.Update(Vector2.Zero);
        }

        public virtual void CompleteClick(object info)
        {
            ConfirmFunction(info);

            done = true;
        }

        public override void Draw()
        {
            background.Draw(Vector2.Zero);

            //textZone.color = color;
            textZone.Draw(new Vector2(position.X - textZone.dimensions.X/2, position.Y-dimensions.Y/2+20));

            if(button != null)
            {
                button.Draw(Vector2.Zero);
            }
        }
    }
}
