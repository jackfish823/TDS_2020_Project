#region Includes
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion


namespace TopDownShooterProject2020.Source.Engine.DataTypes.Forms
{
    public class ArrowSelector
    {
        public int selected;

        public string title;

        public Vector2 position, dimensions;

        public List<BasicButton> buttons = new List<BasicButton>(); 

        public List<FormOption> options = new List<FormOption>();

        public ArrowSelector(Vector2 position, Vector2 dimensions, string title)
        {
            this.title = title;
            this.dimensions = dimensions;
            this.position = position;
            selected = 0;

            buttons.Add(new BasicButton("2d\\Misc\\left_arrow", new Vector2(-dimensions.X / 2 + dimensions.Y / 2, 0), new Vector2(dimensions.Y, dimensions.Y), PathGlobals.ARIAL_FONT, "", LeftArrowClick, null));
            buttons.Add(new BasicButton("2d\\Misc\\right_arrow", new Vector2(dimensions.X / 2 - dimensions.Y / 2, 0), new Vector2(dimensions.Y, dimensions.Y), PathGlobals.ARIAL_FONT, "", RightArrowClick, null));

        }
        public virtual void Update(Vector2 offset)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(offset + position);
            }
        }

        public virtual void AddOption(FormOption formOption)
        {
            options.Add(formOption);
        }

        public virtual void LeftArrowClick(object info)
        {
            selected--;
            if(selected < 0)
            {
                selected = options.Count - 1;
            }
        }

        public virtual void RightArrowClick(object info)
        {
            selected++;
            if (selected >= options.Count)
            {
                selected = 0;
            }
        }

        public virtual FormOption GetCurrentOption()
        {
            return options[selected];
        }

        public virtual XElement ReturnXML()
        {
            XElement xml = new XElement("Option",
                                    new XElement("name", title), 
                                    new XElement("selected", selected),
                                    new XElement("selectedName", options[selected].name));

            return xml;
        }

        public virtual void Draw(Vector2 offset, SpriteFont font)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(offset + position);
            }

            Globals.CleanShader();

            if(options.Count > selected && selected >= 0)
            {
                Vector2 strDimensions = font.MeasureString(options[selected].name);
                Globals.spriteBatch.DrawString(font, options[selected].name, offset + position + new Vector2( - strDimensions.X/2, -strDimensions.Y/2), Color.GreenYellow);

                strDimensions = font.MeasureString(title + ": ");
                Globals.spriteBatch.DrawString(font, title + ": ", offset + position + new Vector2(-dimensions.X/2 - strDimensions.X - 10, -strDimensions.Y / 2), Color.GreenYellow);
            }
        }

    }
}   
