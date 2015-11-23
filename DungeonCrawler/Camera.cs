using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler
{
    public class Camera
    {
        public Matrix transform;
        Viewport view;
        public Vector2 centre;

        public Camera (Viewport newView)
        {
            view = newView;
        }

        public Camera()
        {

        }

        public void Update(GameTime g, Vector2 playerPos)
        {
            centre = new Vector2(playerPos.X * 64 + 32 - 640, playerPos.Y * 64 + 32 - 360);
            transform = Matrix.CreateScale(new Vector3(2, 2, 0)) * Matrix.CreateTranslation(-centre.X, -centre.Y, 0);
        }
    }
}
