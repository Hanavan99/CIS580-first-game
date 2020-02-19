using CIS580_first_game.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580_first_game.Model
{
    public class TerrainTile : UpdateableObject<MyGameState>
    {

        private TerrainModel model;
        private Rectangle bounds;
        private Rectangle srcBounds;

        public TerrainTile(TerrainModel model, int x, int type, int viewportHeight)
        {
            this.model = model;
            bounds = new Rectangle(x * TerrainModel.TerrainWidth, viewportHeight - TerrainModel.TerrainWidth, TerrainModel.TerrainWidth, TerrainModel.TerrainWidth);
            srcBounds = new Rectangle(type * TerrainModel.TerrainWidth, 0, TerrainModel.TerrainWidth, TerrainModel.TerrainWidth);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, MyGameState gameState)
        {
            spriteBatch.Begin(transformMatrix: gameState.CameraMatrix);
            spriteBatch.Draw(model.Texture, bounds, srcBounds, Color.White);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime, MyGameState gameState)
        {
            // do nothing
        }
    }
}
