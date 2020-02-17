using CIS580_first_game.Collisions;
using CIS580_first_game.GameState;
using CIS580_first_game.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CIS580_first_game
{
    public class Game1 : Game
    {
        public static readonly Color[] BallColorList = new Color[] { Color.White, Color.DeepSkyBlue, Color.Red, Color.ForestGreen, Color.LightGoldenrodYellow };

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<UpdateableObject> objects = new List<UpdateableObject>();
        private Player player;
        private Random r = new Random();
        private double lastMillis = 0;
        private SpriteFont font;
        private SoundEffect levelUp;
        
        private MyGameState gameState;
        private BallModel ballModel;

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
            objects.Add(player);

            ballModel = new BallModel();

            gameState = new MyGameState();

            base.Initialize();
        }
        
        private void AddBall()
        {
            Ball b = new Ball(r.Next() % (_graphics.PreferredBackBufferWidth - 50) + 25, 200, (float)r.NextDouble() * 5f - 2.5f, (float)r.NextDouble() * 5f - 2.5f, BallColorList[r.Next() % BallColorList.Length], ballModel);
            objects.Add(b);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            AddBall();
            player.LoadContent(Content);
            ballModel.LoadContent(Content);
            font = Content.Load<SpriteFont>("default_font");
            levelUp = Content.Load<SoundEffect>("level_up");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            gameState.ViewportHeight = _graphics.PreferredBackBufferHeight;
            gameState.ViewportWidth = _graphics.PreferredBackBufferWidth;

            // handle updating of all updateable objects
            foreach (UpdateableObject o in objects.ToArray())
            {
                o.Update(gameTime, gameState);
                if (o.GetType().Equals(typeof(Ball)) && player.bounds.CollidesWith(((Ball)o).bounds))
                {
                    objects.Clear();
                    objects.Add(player);
                    AddBall();
                    lastMillis = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            // end of game checks
            if (gameTime.TotalGameTime.TotalMilliseconds - lastMillis > 10000)
            {
                AddBall();
                lastMillis = gameTime.TotalGameTime.TotalMilliseconds;
                levelUp.Play();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);

            // draw all updateable objects
            foreach (UpdateableObject o in objects.ToArray())
            {
                o.Draw(gameTime, _spriteBatch, gameState);
            }

            // draw string
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, "Current Level: " + (objects.Count - 1), new Vector2(20, 20), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
