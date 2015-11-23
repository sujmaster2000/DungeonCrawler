using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace DungeonCrawler
{
    public class MouseHandler
    {
        public Rectangle rect;
        public MouseState m;

        public MouseHandler()
        {
            rect = new Rectangle(0,0,1,1);
        }

        public void Update()
        {
            m = Mouse.GetState();
            rect.X = m.X;
            rect.Y = m.Y;
        }
    }
}
