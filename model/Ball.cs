using CIS580_first_game.Collisions;
using CIS580_first_game.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game.Model
{
    public class Ball : UpdateableObject<MyGameState>
    {
        public BallModel model;
        public BoundingCircle bounds;
        public float velX;
        public float velY;
        private Color color;


        public Ball(float x, float y, float velX, float velY, Color color, BallModel model)
        {
            bounds.X = x;
            bounds.Y = y;
            bounds.Radius = BallModel.SpriteSize / 2;
            this.velX = velX;
            this.velY = velY;
            this.color = color;
            this.model = model;
        }

       

        public override void Update(GameTime gameTime, MyGameState gameState)
        {
            bounds.X += velX;
            bounds.Y += velY;

            if (bounds.X < bounds.Radius)
            {
                velX = -velX;
                bounds.X = bounds.Radius;
                model.BounceSound.Play();
            } else if (bounds.X > gameState.ViewportWidth - bounds.Radius)
            {
                velX = -velX;
                bounds.X = gameState.ViewportWidth - bounds.Radius;
                model.BounceSound.Play();
            }

            if (bounds.Y > gameState.ViewportHeight - bounds.Radius) // if the ball is under the ground
            {
                bounds.Y = gameState.ViewportHeight - (bounds.Y - (gameState.ViewportHeight - bounds.Radius)) - bounds.Radius;
                velY = -velY;
                model.BounceSound.Play();
            }

            velY += (float) (0.005 * gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, MyGameState gameState)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(model.Texture, bounds, color);
            spriteBatch.End();
        }

    }
}
