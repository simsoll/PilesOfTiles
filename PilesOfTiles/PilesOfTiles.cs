#region Using Statements
using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Bricks;
using PilesOfTiles.Collision;
using PilesOfTiles.Core;
using PilesOfTiles.Core.Input.Keyboard;
using PilesOfTiles.Core.Profiler;
using PilesOfTiles.DrawEffects;
using PilesOfTiles.HighScore;
using PilesOfTiles.Input;
using PilesOfTiles.Levels;
using PilesOfTiles.Particles;
using PilesOfTiles.Randomizers;
using PilesOfTiles.Screens;
using PilesOfTiles.Screens.Messages;
using PilesOfTiles.UserInterfaces;
using IDrawable = PilesOfTiles.Core.IDrawable;
using IScreen = PilesOfTiles.Screens.IScreen;

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

        private IRandomizer _randomizer;
        
        private Texture2D _tileTexture;
        private Texture2D _textTexture;

        private int _sizeMultiplier = 2;
        private IEnumerable<IController> _controllers;
        private IEnumerable<IUpdatable> _updatables;
        private IEnumerable<IDrawable> _drawables;

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

            _randomizer = new Randomizer();

            var tileSize = _sizeMultiplier*8;
            var textSize = _sizeMultiplier*1;
            var levelHeight = 30;
            var levelWidth = 20;
            var textColor = Color.Black;
            var unSelectedTextColor = Color.Gray;
            var highScoreFilePath = "Highscore.txt";

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Default");
            _profileService = new ProfileService(_eventAggregator);
            _keyboardService = new KeyboardService(_eventAggregator, TimeSpan.FromMilliseconds(500));

            _tileTexture = GetPlain2DTexture(tileSize);
            _textTexture = GetPlain2DTexture(textSize);

            var centeredLevelPosition = CenteredPosition(levelWidth, levelHeight, tileSize);
            var centeredBrickSpawnPosition = centeredLevelPosition + new Vector2(levelWidth/2.0f - 1, 0);
            var centeredTextPosition = CenteredPosition(levelWidth, levelHeight, textSize) + new Vector2(10, 0);
            var statisticsPosition = centeredTextPosition + new Vector2(0, levelHeight + 125);

            _gameScreen = InitializeGameScreen(_randomizer, levelWidth, levelHeight, tileSize, textSize,
                centeredLevelPosition, centeredBrickSpawnPosition, statisticsPosition);

            var highScoreRepository = new HighScoreRepository(highScoreFilePath);

            _startScreen = new StartScreen(_eventAggregator, _textTexture, centeredTextPosition, textSize, textColor, unSelectedTextColor);
            _gamePausedScreen = new GamePausedScreen(_eventAggregator, _textTexture, centeredTextPosition, textSize, textColor);
            _gameEndedScreen = new GameEndedScreen(_eventAggregator, highScoreRepository, _textTexture, centeredTextPosition, textSize, textColor);
            _highScoreScreen = new HighScoreScreen(_eventAggregator, highScoreRepository, _textTexture, centeredTextPosition, textSize, textColor);

            _screenManager = new ScreenManager(_eventAggregator, _startScreen, _gameScreen, _gamePausedScreen,
                _gameEndedScreen, _highScoreScreen);
            _screenManager.Load();
        }

        private Vector2 CenteredPosition(int levelWidth, int levelHeight, int tileSize)
        {
            var screenHeight = GraphicsDevice.Viewport.Height/(float)tileSize;
            var screenWidth = GraphicsDevice.Viewport.Width / (float)tileSize;

            return new Vector2(screenWidth, screenHeight)/2.0f -
                   new Vector2(levelWidth, levelHeight)/2.0f;
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
//#if DEBUG
//            _profileService.Draw(_spriteBatch, _font, Vector2.Zero);
//#endif
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private GameScreen InitializeGameScreen(IRandomizer randomizer, int levelWidth, int levelHeight, int tileSize, int textSize, Vector2 centeredLevelPosition, Vector2 centeredBrickSpawnPosition, Vector2 statisticsPosition)
        {
            var inputService = new InputService(_eventAggregator);
            var collisionDetectionService = new CollisionDetectionService(_eventAggregator);
            var particleEngine = new ParticleEngine(_eventAggregator, randomizer, new[] { _tileTexture });
            var levelManager = new LevelManager(_eventAggregator, centeredLevelPosition, levelHeight, levelWidth);
            var brickManager = new BrickManager(_eventAggregator, centeredBrickSpawnPosition);
            var highScoreService = new HighScoreService(_eventAggregator);
            var userInterfaceService = new UserInterfaceService(_eventAggregator, statisticsPosition, _textTexture,
                textSize,
                Color.Black);
            var drawEffectService = new DrawEffectService(_eventAggregator, randomizer, _tileTexture, tileSize, _textTexture, textSize);

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
