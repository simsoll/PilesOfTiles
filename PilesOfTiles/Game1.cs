#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using PilesOfTiles.Brick;
using PilesOfTiles.Collision;
using PilesOfTiles.Core.Input;
using PilesOfTiles.Core.Input.Keyboard;
using PilesOfTiles.Core.Input.Keyboard.Messages;
using PilesOfTiles.Core.Profiler;
using PilesOfTiles.Input;
using PilesOfTiles.Level;

#endregion

namespace PilesOfTiles
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private SpriteFont _font;
        private ProfileManager _profileManager;
        private KeyboardManager _keyboardManager;
        private InputManager _inputManager;
        private LevelManager _levelManager;
        private BrickManager _brickManager;
        private CollisionManager _collisionManager;
        private IEventAggregator _eventAggregator;
        private int _tileSize;

        public Game1()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 520,
                PreferredBackBufferWidth = 520
            };
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _eventAggregator = new EventAggregator();

            _tileSize = 8;

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("particle");
            _font = Content.Load<SpriteFont>("Default");
            _profileManager = new ProfileManager(_eventAggregator);
            _keyboardManager = new KeyboardManager(_eventAggregator, TimeSpan.FromMilliseconds(500));
            _inputManager = new InputManager(_eventAggregator);
            _collisionManager = new CollisionManager(_eventAggregator);
            _levelManager = new LevelManager(_eventAggregator, TimeSpan.FromMilliseconds(500));
            _brickManager = new BrickManager(_eventAggregator);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardManager.Update(gameTime);
            _levelManager.Update(gameTime);

            _profileManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();

            _profileManager.Draw(_spriteBatch, _font, Vector2.Zero);
            _levelManager.Draw(_spriteBatch, _texture);
            _brickManager.Draw(_spriteBatch, _texture, _tileSize);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
