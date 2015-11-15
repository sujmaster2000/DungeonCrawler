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
    class Button
    {

        Texture2D ButtonTexture;
        Rectangle Rect;
        Rectangle CamRect;

        int SuccessX;
        int SuccessY;

        public void LoadContent(ContentManager Content, string Path, int x, int y)
        {
            ButtonTexture = Content.Load<Texture2D>(Path);
            Rect = new Rectangle(x, y, ButtonTexture.Width, ButtonTexture.Height);
            CamRect = new Rectangle(Rect.X*2, Rect.Y*2, Rect.Width*2, Rect.Height*2);
        }

        public void Update(int x, int y)
        {
            Rect.X = x;
            Rect.Y = y;
            CamRect = new Rectangle(Rect.X * 2, Rect.Y * 2, Rect.Width * 2, Rect.Height * 2);
        }

        public bool hasBeenPressed(Rectangle MouseRect, MouseState Mouse)
        {
            if (MouseRect.X >= CamRect.X && MouseRect.X <= CamRect.X + CamRect.Width && MouseRect.Y >= CamRect.Y && MouseRect.Y <= CamRect.Y + CamRect.Height && Mouse.LeftButton == ButtonState.Pressed)
            {
                SuccessX = MouseRect.X;
                SuccessY = MouseRect.Y;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw(SpriteBatch s, SpriteFont arial)
        {
            s.Draw(ButtonTexture, Rect, Color.White);
            s.DrawString(arial, SuccessX + ", " + SuccessY, new Vector2(Rect.X, Rect.Y), Color.Red);
        }
    }
}
