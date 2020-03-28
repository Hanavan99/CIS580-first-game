using CIS580_first_game.Collisions;
using CIS580_first_game.GameState;
using CIS580_first_game.particle;
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
        public BallModel model; // allows for the Flyweight pattern
        public BoundingCircle bounds;
        public float velX;
        public float velY;
        private Color color;
        private ParticleSystem impact;
        private ParticleSystem fire;

        public Ball(float x, float y, float velX, float velY, Color color, BallModel model, GraphicsDevice device)
        {
            bounds.X = x;
            bounds.Y = y;
            bounds.Radius = BallModel.SpriteSize / 2;
            this.velX = velX;
            this.velY = velY;
            this.color = color;
            this.model = model;
            this.impact = new ParticleSystem(device, 100, model.ImpactTexture);
            impact.BlendState = BlendState.Opaque;
            impact.SpawnParticle = (ref Particle p, Random r) =>
            {
                p.position = impact.Emitter + new Vector2(0, BallModel.SpriteSize / 2);
                p.velocity = 50.0f * impact.RandomVector();
                p.acceleration = new Vector2(0, 500f);
                p.color = color;
                p.scale = 0.5f + (float)r.NextDouble();
                p.life = 3.0f;
            };
            impact.UpdateParticle = ParticleSystem.DefaultParticleUpdater;
            fire = new ParticleSystem(device, 200, model.FireTexture);
            fire.SpawnParticle = (ref Particle p, Random r) =>
            {
                p.position = impact.Emitter;
                p.velocity = 50.0f * impact.RandomVector();
                p.acceleration = new Vector2(0, -100f);
                p.color = new Color(20, 20, 20);
                p.scale = 0.5f + (float)r.NextDouble();
                p.life = 10.0f;
            };
            fire.UpdateParticle = ParticleSystem.DefaultParticleUpdater;
            fire.SpawnPerFrame = 3;
        }

       

        public override void Update(GameTime gameTime, MyGameState gameState)
        {
            bounds.X += velX;
            bounds.Y += velY;

            if (bounds.X < -gameState.WorldSize + bounds.Radius)
            {
                velX = -velX;
                bounds.X = -gameState.WorldSize + bounds.Radius;
                model.BounceSound.Play();
            } else if (bounds.X > gameState.WorldSize - bounds.Radius)
            {
                velX = -velX;
                bounds.X = gameState.WorldSize - bounds.Radius;
                model.BounceSound.Play();
            }

            if (bounds.Y > gameState.ViewportHeight - bounds.Radius - TerrainModel.TerrainHeight) // if the ball is under the ground
            {
                bounds.Y = gameState.ViewportHeight - (bounds.Y - (gameState.ViewportHeight - bounds.Radius - TerrainModel.TerrainHeight)) - bounds.Radius - TerrainModel.TerrainHeight;
                velY = -velY;
                model.BounceSound.Play();
                impact.SpawnPerFrame = 10; // allow particle system to spawn 10 particles

            }
            impact.Emitter = new Vector2(bounds.X, bounds.Y);
            impact.Update(gameTime);
            impact.SpawnPerFrame = 0;

            fire.Update(gameTime);

            velY += (float) (0.005 * gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, MyGameState gameState)
        {
            impact.Draw(gameState.CameraMatrix);
            fire.Draw(gameState.CameraMatrix);
            spriteBatch.Begin(transformMatrix: gameState.CameraMatrix);
            spriteBatch.Draw(model.Texture, bounds, color);
            spriteBatch.End();
        }

    }
}
