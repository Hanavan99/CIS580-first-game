using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game.Model
{
    public abstract class UpdateableObject<T> : UpdateableObject
    {

        public abstract void Update(GameTime gameTime, T gameState);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, T gameState);

        public override void Update(GameTime gameTime, object gameState)
        {
            Update(gameTime, (T)gameState);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, object gameState)
        {
            Draw(gameTime, spriteBatch, (T)gameState);
        }

    }
    
    public abstract class UpdateableObject
    {

        public abstract void Update(GameTime gameTime, object gameState);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, object gameState);

    }
}
