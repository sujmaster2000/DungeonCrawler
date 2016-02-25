using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCrawler
{
    public class HealthPotion
    {
        public bool hasBeenConsumed = false;
        public Vector2 pos;
        Rectangle rect;

        public HealthPotion()
        {

        }

        public HealthPotion(Random r, Floor f)
        {
            bool validPos = false;

            while (!validPos)
            {
                pos.X = r.Next(1, f.Wall_Grid.GetLength(0) - 1);
                pos.Y = r.Next(1, f.Wall_Grid.GetLength(1) - 1);

                if (f.Wall_Grid[Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y)] == "f")
                {
                    validPos = true;
                }
            }

            rect = new Rectangle(Convert.ToInt32(pos.X * 32), Convert.ToInt32(pos.Y * 32), 32, 32);
        }

        public void Draw(SpriteBatch s, Texture2D t)
        {
            if (!hasBeenConsumed)
            {
                s.Draw(t, rect, Color.White);
            }
        }
    }
}
