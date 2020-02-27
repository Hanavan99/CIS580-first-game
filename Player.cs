using CIS580_first_game.Collisions;
using CIS580_first_game.GameState;
using CIS580_first_game.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game
{
    public class Player : UpdateableObject<MyGameState>
    {

        public const int PlayerSpriteSize = 50;

        public BoundingRectangle bounds;
        public float playerSpeed;
        public bool isFacingLeft; // player state representing the last direction of travel
        private Texture2D texture;

        public Player(int viewportWidth, int viewportHeight)
        {
            bounds.X = 0;
            bounds.Y = viewportHeight - PlayerSpriteSize - TerrainModel.TerrainHeight;
            bounds.Width = 50;
            bounds.Height = 50;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("santa");
        }

        public override void Update(GameTime gameTime, MyGameState gameState)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.D)) // moving right
            {
                playerSpeed = 5;
                isFacingLeft = true;
            }
            else if (state.IsKeyDown(Keys.A)) // moving left
            {
                playerSpeed = -5;
                isFacingLeft = false;
            }
            else
            {
                playerSpeed = 0;
            }
            Vector3 t = gameState.CameraMatrix.Translation; 
            
            bounds.X += (int)(playerSpeed * gameTime.ElapsedGameTime.TotalMilliseconds * 0.1);
            if (bounds.X < -gameState.WorldSize)
            {
                bounds.X = -gameState.WorldSize;
            }
            else if (bounds.X > gameState.WorldSize - PlayerSpriteSize)
            {
                bounds.X = gameState.WorldSize - PlayerSpriteSize;
            } else {
                gameState.CameraMatrix = Matrix.CreateTranslation(t.X - (float)(playerSpeed * gameTime.ElapsedGameTime.TotalMilliseconds * 0.1f), 0, 0);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, MyGameState gameState)
        {
            if (playerSpeed == 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, new BoundingRectangle(gameState.ViewportWidth / 2 - PlayerSpriteSize / 2, bounds.Y, bounds.Width, bounds.Height), GetPlayerSpriteRect(0), Color.White);
                spriteBatch.End();
            }
            else
            {
                int spriteIndex = (int)(gameTime.TotalGameTime.TotalSeconds * 12) % 6;
                spriteBatch.Begin();
                spriteBatch.Draw(texture, new BoundingRectangle(gameState.ViewportWidth / 2 - PlayerSpriteSize / 2, bounds.Y, bounds.Width, bounds.Height), GetPlayerSpriteRect(spriteIndex), Color.White);
                spriteBatch.End();
            }
        }

        private Rectangle GetPlayerSpriteRect(int spriteIndex)
        {
            return new Rectangle(isFacingLeft ? (spriteIndex + 1) * PlayerSpriteSize : spriteIndex * PlayerSpriteSize, 0, isFacingLeft ? -PlayerSpriteSize : PlayerSpriteSize, PlayerSpriteSize);
        }

    }
}
