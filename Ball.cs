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

        private float X, Y;
        private float VelX, VelY;
        private Color Color;
        private Texture2D ball;
        private int spriteSize = 50;

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

            if (Y > viewportHeight + spriteSize) // if the ball is under the ground
            {
                Y = viewportHeight - (Y - viewportHeight - spriteSize);
                VelY = -VelY;
            }

            VelY += (float) (0.01 * gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ball, new Rectangle((int)X, (int)Y, spriteSize, spriteSize), Color);
            spriteBatch.End();
        }

    }
}
