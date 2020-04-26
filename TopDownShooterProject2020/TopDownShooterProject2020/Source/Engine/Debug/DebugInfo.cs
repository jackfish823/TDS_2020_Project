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
    public enum DebugInfoType
    {
        Lines,
        Circles,
        Text
    }
    public class DebugInfo
    {
        private List<LinePacket> lines = new List<LinePacket>();
        private List<CirclePacket> circles = new List<CirclePacket>();
        private List<TextPacket> texts = new List<TextPacket>();
        private Basic2d solid;
        private SpriteFont font;

        public DebugInfo()
        {
            this.solid = new Basic2d("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(1, 1));
            this.font = Globals.content.Load<SpriteFont>(PathGlobals.ARIAL_FONT);
        }


        public void AddLine(Vector2 source, Vector2 target, Color color)
        {
            lines.Add(new LinePacket(source, target, color));
        }
        public void AddLine(LinePacket line)
        {
            lines.Add(line);
        }
        public void AddCircle(CirclePacket circle)
        {
            circles.Add(circle);
        }
        public void AddText(TextPacket text)
        {
            texts.Add(text);
        }
        public void Draw(DebugInfoType type, Vector2 offset)
        {
            if (Globals.toggleLinesDebug)
            {
                switch (type)
                {
                    case DebugInfoType.Circles:
                        DrawCircles(offset);
                        break;

                    case DebugInfoType.Lines:
                        DrawLines(offset);
                        break;

                    case DebugInfoType.Text:
                        DrawText(offset);
                        break;

                    default:
                        Console.WriteLine("NO MODE");
                        break;
                }
            }           
        }
        public void DrawLines(Vector2 offset)
        {            
            for (int i = 0; i < lines.Count; i++)
            {
                Globals.DrawLine(solid.texture, lines[i].Source, lines[i].Target, lines[i].Color, offset);
                lines.RemoveAt(i);
            }
        }
        public void DrawCircles(Vector2 offset)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                Globals.DrawCircle(solid.texture, circles[i].Source, circles[i].Radius, circles[i].Color, 1, 100, offset);
                circles.RemoveAt(i);

            }
        }
        public void DrawText(Vector2 offset)
        {
            Vector2 drawPlace = new Vector2(20, 20); // Top left on the screen at the start

            for (int i = 0; i < texts.Count; i++)
            {
                if(texts[i].Position != Vector2.Zero)
                {
                    Vector2 stringDimensions = font.MeasureString(texts[i].Text);
                    Globals.spriteBatch.DrawString(font, texts[i].Text, new Vector2(texts[i].Position.X - stringDimensions.X/2, texts[i].Position.Y) + offset, texts[i].Color);
                }
                else
                {
                    Globals.spriteBatch.DrawString(font, texts[i].Text, drawPlace, texts[i].Color);
                    drawPlace.Y += 25;
                }

                texts.RemoveAt(i);

            }
        }
        public void Clear()
        {
            lines.Clear();
            circles.Clear();
            texts.Clear();
        }        
    }
}
