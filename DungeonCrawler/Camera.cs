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
        public Vector2 centre;

        public Camera()
        {

        }

        public void Update(GameTime g, Vector2 pos)
        {
            centre = new Vector2(pos.X * 64 + 32 - 640, pos.Y * 64 + 32 - 360);
            transform = Matrix.CreateScale(new Vector3(2, 2, 0)) * Matrix.CreateTranslation(-centre.X, -centre.Y, 0);
        }
    }
}
