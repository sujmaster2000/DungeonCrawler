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
    class GUIHandler
    {
        public List<Button> Buttons = new List<Button>();

        public GUIHandler()
        {

        }

        public void AddButton(Button b)
        {
            Buttons.Add(b);
        }

        public void Draw(SpriteBatch s, SpriteFont arial)
        {
            foreach (Button b in Buttons)
            {
                b.Draw(s, arial);
            }
        }
    }
}
