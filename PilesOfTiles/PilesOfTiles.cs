#region Using Statements
using System;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PilesOfTiles.Brick;
using PilesOfTiles.Collision;
using PilesOfTiles.Core.Input.Keyboard;
using PilesOfTiles.Core.Profiler;
using PilesOfTiles.HighScore;
using PilesOfTiles.Input;
using PilesOfTiles.Level;
using PilesOfTiles.Particle;
using PilesOfTiles.UserInterface;
using PilesOfTiles.View;
using PilesOfTiles.View.Messages;

#endregion

namespace PilesOfTiles
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PilesOfTiles : Game, IHandle<QuitGame>
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private ProfileManager _profileManager;
        private KeyboardManager _keyboardManager;
        private ParticleManager _particleManager;

        private IEventAggregator _eventAggregator;
        private IView _viewManager;
        private IView _startView;
        private IView _gamePausedView;
        private IView _playingView;
        private IView _gameEndedView;
        private IView _highScoreView;

        private Texture2D _tileTexture;
        private int _tileSize;
        private int _levelWidth;
        private int _levelHeight;

        private Texture2D _textTexture;
        private int _textSize;

        public PilesOfTiles()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 568,
                PreferredBackBufferWidth = 320
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
            _eventAggregator.Subscribe(this);

            _tileSize = 8;
            _textSize = 1;
            _levelHeight = 30;
            _levelWidth = 20;

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Default");
            _profileManager = new ProfileManager(_eventAggregator);
            _keyboardManager = new KeyboardManager(_eventAggregator, TimeSpan.FromMilliseconds(500));

            _tileTexture = GetPlain2DTexture(_tileSize);
            _textTexture = GetPlain2DTexture(_textSize);

            _startView = new StartView(_eventAggregator, _textTexture, _textSize, Color.Blue, Color.Red);
            _playingView = new PlayingView(_eventAggregator, _tileTexture, _tileSize, _textTexture, _textSize,
                _levelWidth, _levelHeight, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _gamePausedView = new GamePausedView(_eventAggregator, _textTexture, _textSize, Color.Blue);
            _gameEndedView = new GameEndedView(_eventAggregator, _textTexture, _textSize, Color.Blue);
            _highScoreView = new HighScoreView(_eventAggregator, _textTexture, _textSize, Color.Blue);

            _viewManager = new ViewManager(_eventAggregator, _startView, _playingView, _gamePausedView, _gameEndedView, _highScoreView);
            _viewManager.Load();

            _particleManager = new ParticleManager(new []{_tileTexture}, Vector2.Zero);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            _viewManager.Unload();
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
            _viewManager.Update(gameTime);
            _particleManager.EmitterLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            _particleManager.Update(gameTime);

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

            _viewManager.Draw(_spriteBatch);
            _particleManager.Draw(_spriteBatch);
#if DEBUG
            _profileManager.Draw(_spriteBatch, _font, Vector2.Zero);
#endif
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Texture2D GetPlain2DTexture(int textureSize)
        {
            var texture = new Texture2D(GraphicsDevice, textureSize, textureSize);
            var color = new Color[textureSize * textureSize];
            for (var i = 0; i < color.Length; i++)
            {
                color[i] = Color.White;
            }
            texture.SetData(color);

            return texture;
        }

        public void Handle(QuitGame message)
        {
            Exit();
        }
    }
}
