using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawler
{
    public class MessageHandler
    {
        public string[] Message = new string[5];
        public Vector2 MessageBoxLocation;

        public MessageHandler (Vector2 Pos)
        {
            MessageBoxLocation = Pos;
            for (int i = 0; i < 5; i++)
            {
                Message[i] = "";
            }
        }
        
        public MessageHandler()
        {

        }

        public void AddMessage(string s)
        {
            for (int i = 4; i > 0; i-- )
            {
                Message[i] = Message[i-1];
            }

            Message[0] = s;
        }

        public void Update(Vector2 pos)
        {
            MessageBoxLocation = pos;
        }

        public void Draw(SpriteBatch s, SpriteFont f)
        {
            for (int i = 0; i < 5; i ++)
            {
                s.DrawString(f, Message[i], new Vector2(MessageBoxLocation.X, MessageBoxLocation.Y + 13 * i), Color.White);
            }
        }
    }
}
