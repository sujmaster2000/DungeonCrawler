using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCrawler
{
    class HealthPotion
    {
        char state;
        char type;
        string name;

        public Vector2 pos;
        Rectangle rect;

        public HealthPotion(Random r, string[,] Maze, char state, char type, string name)
        {
            bool validPos = false;

            while (!validPos)
            {
                pos.X = r.Next(1, Maze.GetLength(0) - 1);
                pos.Y = r.Next(1, Maze.GetLength(1) - 1);

                if (Maze[Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y)] == "f")
                {
                    validPos = true;
                }
            }

            rect = new Rectangle(Convert.ToInt32(pos.X * 32), Convert.ToInt32(pos.Y * 32), 32, 32);
        }

        public void Draw(SpriteBatch s, Texture2D t)
        {
            s.Draw(t, rect, Color.White);
        }
    }
}
