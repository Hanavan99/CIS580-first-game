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
        public BoundingCircle bounds;
        public float velX;
        public float velY;
        private Texture2D ball;
        private int spriteSize = 50;
        private Color color;


        public Ball(float x, float y, float velX, float velY, Color color)
        {
            bounds.X = x;
            bounds.Y = y;
            bounds.Radius = spriteSize / 2;
            this.velX = velX;
            this.velY = velY;
            this.color = color;
        }

        public void LoadContent(ContentManager content)
        {
            ball = content.Load<Texture2D>("ball");
        }

        public void Update(GameTime gameTime, int viewportWidth, int viewportHeight)
        {
            bounds.X += velX;
            bounds.Y += velY;

            if (bounds.X < bounds.Radius)
            {
                velX = -velX;
                bounds.X = bounds.Radius;
            } else if (bounds.X > viewportWidth - bounds.Radius)
            {
                velX = -velX;
                bounds.X = viewportWidth - bounds.Radius;
            }

            if (bounds.Y > viewportHeight - bounds.Radius) // if the ball is under the ground
            {
                bounds.Y = viewportHeight - (bounds.Y - (viewportHeight - bounds.Radius)) - bounds.Radius;
                velY = -velY;
            }

            velY += (float) (0.005 * gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ball, bounds, color);
            spriteBatch.End();
        }

    }
}
