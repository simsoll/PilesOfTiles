#region Using Statements
using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PilesOfTiles.Brick;
using PilesOfTiles.Collision;
using PilesOfTiles.Core;
using PilesOfTiles.Core.Input.Keyboard;
using PilesOfTiles.Core.Profiler;
using PilesOfTiles.DrawEffect;
using PilesOfTiles.HighScore;
using PilesOfTiles.Input;
using PilesOfTiles.Level;
using PilesOfTiles.Particle;
using PilesOfTiles.Screen;
using PilesOfTiles.Screen.Messages;
using PilesOfTiles.UserInterface;
using IDrawable = PilesOfTiles.Core.IDrawable;
using IScreen = PilesOfTiles.Screen.IScreen;

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
        private ProfileService _profileService;
        private KeyboardService _keyboardService;

        private IEventAggregator _eventAggregator;
        private IScreen _screenManager;
        private IScreen _startScreen;
        private IScreen _gamePausedScreen;
        private IScreen _gameScreen;
        private IScreen _gameEndedScreen;
        private IScreen _highScoreScreen;

        private Texture2D _tileTexture;
        private int _tileSize;
        private int _levelWidth;
        private int _levelHeight;

        private Texture2D _textTexture;
        private int _textSize;

        private int _sizeMultiplier = 2;
        private IEnumerable<IController> _controllers;
        private IEnumerable<IUpdatable> _updatables;
        private IEnumerable<IDrawable> _drawables;
        private int _screenHeight;
        private int _screenWidth;

        public PilesOfTiles()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = _sizeMultiplier*568,
                PreferredBackBufferWidth = _sizeMultiplier*320
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

            _tileSize = _sizeMultiplier*8;
            _textSize = _sizeMultiplier*1;
            _levelHeight = 30;
            _levelWidth = 20;

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Default");
            _profileService = new ProfileService(_eventAggregator);
            _keyboardService = new KeyboardService(_eventAggregator, TimeSpan.FromMilliseconds(500));

            _tileTexture = GetPlain2DTexture(_tileSize);
            _textTexture = GetPlain2DTexture(_textSize);

            _screenHeight = GraphicsDevice.Viewport.Width / _tileSize;
            _screenWidth = GraphicsDevice.Viewport.Height / _tileSize;

            var centeredLevelPosition = new Vector2(_screenHeight, _screenWidth) / 2 -
                            new Vector2(_levelWidth, _levelHeight) / 2;

            var centeredBrickSpawnPosition = centeredLevelPosition + new Vector2(_levelWidth / 2 - 1, 0);
            var statisticsPosition = centeredLevelPosition + new Vector2(0, _levelHeight + 2);

            _gameScreen = InitializeGameScreen(centeredLevelPosition, centeredBrickSpawnPosition, statisticsPosition);

            _startScreen = new StartScreen(_eventAggregator, _textTexture, _textSize, Color.Blue, Color.Red);
            _gamePausedScreen = new GamePausedScreen(_eventAggregator, _textTexture, _textSize, Color.Blue);
            _gameEndedScreen = new GameEndedScreen(_eventAggregator, _textTexture, _textSize, Color.Blue);
            _highScoreScreen = new HighScoreScreen(_eventAggregator, _textTexture, _textSize, Color.Blue);

            _screenManager = new ScreenManager(_eventAggregator, _startScreen, _gameScreen, _gamePausedScreen, _gameEndedScreen, _highScoreScreen);
            _screenManager.Load();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            _screenManager.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _keyboardService.Update(gameTime);
            _screenManager.Update(gameTime);

            _profileService.Update(gameTime);

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

            _screenManager.Draw(_spriteBatch);
#if DEBUG
            _profileService.Draw(_spriteBatch, _font, Vector2.Zero);
#endif
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private GameScreen InitializeGameScreen(Vector2 centeredLevelPosition, Vector2 centeredBrickSpawnPosition,
    Vector2 statisticsPosition)
        {
            var inputService = new InputService(_eventAggregator);
            var collisionDetectionService = new CollisionDetectionService(_eventAggregator);
            var particleEngine = new ParticleEngine(_eventAggregator, new[] { _tileTexture });
            var levelManager = new LevelManager(_eventAggregator, centeredLevelPosition, _levelHeight, _levelWidth,
                _tileTexture, _tileSize);
            var brickManager = new BrickManager(_eventAggregator, centeredBrickSpawnPosition);
            var highScoreService = new HighScoreService(_eventAggregator);
            var userInterfaceService = new UserInterfaceService(_eventAggregator, statisticsPosition, _tileSize, _textTexture,
                _textSize,
                Color.Black);
            var drawEffectService = new DrawEffectService(_eventAggregator, _tileTexture, _tileSize, _textTexture, _textSize);

            _controllers = new List<IController>
            {
                inputService,
                collisionDetectionService,
                particleEngine,
                levelManager,
                brickManager,
                highScoreService,
                userInterfaceService,
                drawEffectService
            };

            _updatables = new List<IUpdatable>
            {
                particleEngine,
                levelManager,
                drawEffectService,
                highScoreService
            };

            _drawables = new List<IDrawable>
            {
                userInterfaceService,
                drawEffectService
            };


            return new GameScreen(_eventAggregator, _controllers, _updatables, _drawables);
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
