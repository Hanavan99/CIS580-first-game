using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CIS580_first_game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Ball> balls = new List<Ball>();
        private Player player;
        private Random r = new Random();
        private double lastMillis = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }
        
        private void AddBall()
        {
            Ball b = new Ball(r.Next() % (_graphics.PreferredBackBufferWidth - 50) + 25, 200, (float)r.NextDouble() * 5f - 2.5f, (float)r.NextDouble() * 5f - 2.5f, new Color(r.Next() % 128 + 128, r.Next() % 128 + 128, r.Next() % 128 + 128));
            b.LoadContent(Content);
            balls.Add(b);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            AddBall();
            player.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
                foreach (Ball b in balls.ToArray())
                {
                    b.Update(gameTime, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
                    if (player.bounds.CollidesWith(b.bounds))
                    {
                        // add code here for end of game
                        balls.Clear();
                        AddBall();
                    }
                }

            if (gameTime.TotalGameTime.TotalMilliseconds - lastMillis > 10000)
            {
                AddBall();
                lastMillis = gameTime.TotalGameTime.TotalMilliseconds;
            }

            player.Update(gameTime, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            foreach (Ball b in balls.ToArray())
            {
                b.Draw(_spriteBatch);
            }

            player.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

    }
}
