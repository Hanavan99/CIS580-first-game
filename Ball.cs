using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game
{
    public class Ball
    {
        private Texture2D ball;
        private int spriteSize = 50;

        public float X { get; set; } 
        public float Y { get; set; }
        public float VelX { get; set; }
        public float VelY { get; set; }
        public Color Color { get; set; }

        public Ball(float x, float y, float velX, float velY, Color color)
        {
            X = x;
            Y = y;
            VelX = velX;
            VelY = velY;
            Color = color;
        }

        public void Initialize(ContentManager content)
        {
            ball = content.Load<Texture2D>("ball");
        }

        public void Update(GameTime gameTime, int viewportWidth, int viewportHeight)
        {
            X += VelX;
            Y += VelY;

            if (Y > viewportHeight - spriteSize) // if the ball is under the ground
            {
                Y = viewportHeight - (Y - (viewportHeight - spriteSize)) - spriteSize;
                VelY = -VelY;
            }

            VelY += (float) (0.01 * gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch, int xoff)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ball, new Rectangle((int)X + xoff, (int)Y, spriteSize, spriteSize), Color);
            spriteBatch.End();
        }

    }
}
