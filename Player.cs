using Microsoft.Xna.Framework;
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

        public const int PlayerSpriteSize = 50;

        public BoundingRectangle bounds;
        public float playerSpeed;
        private Texture2D texture;

        public Player(int viewportWidth, int viewportHeight)
        {
            bounds.X = 0;
            bounds.Y = viewportHeight - 50;
            bounds.Width = 50;
            bounds.Height = 50;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("santa");
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

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (playerSpeed == 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, bounds, new Rectangle(0, 0, PlayerSpriteSize, PlayerSpriteSize), Color.White);
                spriteBatch.End();
            }
            else
            {
                int spriteIndex = (int)(gameTime.TotalGameTime.TotalSeconds * 12) % 6;
                spriteBatch.Begin();
                spriteBatch.Draw(texture, bounds, new Rectangle(playerSpeed < 0 ? (spriteIndex + 1) * PlayerSpriteSize : spriteIndex * PlayerSpriteSize, 0, playerSpeed < 0 ? -PlayerSpriteSize : PlayerSpriteSize, PlayerSpriteSize), Color.White);
                spriteBatch.End();
            }
        }

    }
}
