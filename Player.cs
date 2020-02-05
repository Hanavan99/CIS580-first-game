﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game
{
    public class Player
    {

        public BoundingRectangle bounds;
        public float playerSpeed;
        private Texture2D standingTexture;
        private Texture2D movingTexture;

        public Player(int viewportWidth, int viewportHeight)
        {
            bounds.X = 0;
            bounds.Y = viewportHeight - 100;
            bounds.Width = 50;
            bounds.Height = 100;
        }

        public void LoadContent(ContentManager content)
        {
            standingTexture = content.Load<Texture2D>("player_standing");
            movingTexture = content.Load<Texture2D>("player_moving");
        }

        public void Update(GameTime gameTime, int width, int height)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.D)) // moving left
            {
                playerSpeed = 5;
            }
            else if (state.IsKeyDown(Keys.A)) // moving right
            {
                playerSpeed = -5;
            }
            else
            {
                playerSpeed = 0;
            }
            bounds.X += (int)(playerSpeed * gameTime.ElapsedGameTime.TotalMilliseconds * 0.1);
            if (bounds.X < 0)
            {
                bounds.X = 0;
            }
            else if (bounds.X > width - 50)
            {
                bounds.X = width - 50;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (playerSpeed == 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(standingTexture, bounds, Color.Black);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                spriteBatch.Draw(movingTexture, bounds, Color.Black);
                spriteBatch.End();
            }
        }

    }
}
