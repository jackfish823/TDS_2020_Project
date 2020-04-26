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
    public class TextZone
    {
        public int maxWidth, lineHeight;
        string str;
        public Vector2 position, dimensions;
        public Color color;

        public SpriteFont font;

        public List<string> lines = new List<string>();

        public TextZone(Vector2 position, string str, int maxWidth, int lineHeight, string font, Color fontColor)
        {
            this.position = position;
            this.str = str;


            this.maxWidth = maxWidth;
            this.lineHeight = lineHeight;
            color = fontColor;

            this.font = Globals.content.Load<SpriteFont>(font);

            if (this.str != "")
            {
                ParseLines();
            }
        }

        #region Properties

        public string Str
        {
            get
            {
                return str;
            }

            set
            {
                str = value;
                ParseLines();
            }
        }

        #endregion


        public virtual void ParseLines()
        {
            lines.Clear();

            List<string> wordList = new List<string>();
            string tempString = "";

            int largestWidth = 0, currentWdith = 0;

            if (str != "" && str != null)
            {
                wordList = str.Split(' ').ToList<string>();

                for (int i = 0; i < wordList.Count; i++)
                {
                    if (tempString != "")
                    {
                        tempString += " ";
                    }

                    currentWdith = (int)(font.MeasureString(tempString + wordList[i]).X);

                    if (currentWdith > largestWidth && currentWdith <= maxWidth)
                    {
                        largestWidth = currentWdith;
                    }

                    if (currentWdith <= maxWidth)
                    {
                        tempString += wordList[i];
                    }
                    else
                    {
                        lines.Add(tempString);

                        tempString = wordList[i];
                    }

                }

                if (tempString != "")
                {
                    lines.Add(tempString);
                }

                SetDims(largestWidth);
            }

        }


        public virtual void SetDims(int largerstWidth)
        {
            dimensions = new Vector2(largerstWidth, lineHeight * lines.Count);
        }

        public virtual void Draw(Vector2 offset)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Globals.spriteBatch.DrawString(font, lines[i], offset + new Vector2(position.X, position.Y + (lineHeight * i)), color);
            }
        }
    }
}