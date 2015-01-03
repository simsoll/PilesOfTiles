using System;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Brick;
using PilesOfTiles.Collision;
using PilesOfTiles.HighScore;
using PilesOfTiles.Input;
using PilesOfTiles.Level;
using PilesOfTiles.UserInterface;

namespace PilesOfTiles.Screen
{
    public class PlayingView : IView
    {
        private IEventAggregator _eventAggregator;

        private Texture2D _tileTexture;
        private int _tileSize;
        private Texture2D _textTexture;
        private int _textSize;
        private int _levelWidth;
        private int _levelHeight;

        private int _screenWidth;
        private int _screenHeight;

        private InputManager _inputManager;
        private LevelManager _levelManager;
        private BrickManager _brickManager;
        private CollisionManager _collisionManager;
        private HighScoreManager _highScoreManager;
        private UserInterfaceManager _userInterfaceManager;

        public PlayingView(
            IEventAggregator eventAggregator, 
            Texture2D tileTexture, 
            int tileSize, 
            Texture2D textTexture,
            int textSize,
            int levelWidth,
            int levelHeight,
            int viewPortWidth,
            int viewPortHeight)
       {
            _eventAggregator = eventAggregator;
            _tileTexture = tileTexture;
            _tileSize = tileSize;
            _textTexture = textTexture;
            _textSize = textSize;
            _levelWidth = levelWidth;
            _levelHeight = levelHeight;

            _screenHeight = viewPortHeight / _tileSize;
            _screenWidth = viewPortWidth / _tileSize;


            var centeredLevelPosition = new Vector2(_screenWidth, _screenHeight)/2 -
                                        new Vector2(_levelWidth, _levelHeight)/2;


            var centeredBrickSpawnPosition = centeredLevelPosition + new Vector2(_levelWidth/2 - 1, 0);
            var statisticsPosition = centeredLevelPosition + new Vector2(0, _levelHeight + 2);


            _inputManager = new InputManager(_eventAggregator);
            _collisionManager = new CollisionManager(_eventAggregator);
            _levelManager = new LevelManager(_eventAggregator, centeredLevelPosition, _levelHeight, _levelWidth,
                _tileTexture, _tileSize);
            _brickManager = new BrickManager(_eventAggregator, centeredBrickSpawnPosition, _tileTexture, _tileSize);
            _highScoreManager = new HighScoreManager(_eventAggregator);
            _userInterfaceManager = new UserInterfaceManager(_eventAggregator, statisticsPosition, _tileSize, _textTexture, _textSize,
                Color.Black);

        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Update(GameTime gameTime)
        {
            _levelManager.Update(gameTime);
            _highScoreManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _levelManager.Draw(spriteBatch);
            _brickManager.Draw(spriteBatch);
            _userInterfaceManager.Draw(spriteBatch);
        }
    }
}