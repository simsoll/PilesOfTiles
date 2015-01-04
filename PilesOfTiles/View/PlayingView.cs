using System.Collections.Generic;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Brick;
using PilesOfTiles.Collision;
using PilesOfTiles.HighScore;
using PilesOfTiles.Input;
using PilesOfTiles.Level;
using PilesOfTiles.Manager;
using PilesOfTiles.UserInterface;

namespace PilesOfTiles.View
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

        private IEnumerable<IManager> _managers; 

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


            var inputManager = new InputManager(_eventAggregator);
            var collisionManager = new CollisionManager(_eventAggregator);
            var levelManager = new LevelManager(_eventAggregator, centeredLevelPosition, _levelHeight, _levelWidth,
                _tileTexture, _tileSize);
            var brickManager = new BrickManager(_eventAggregator, centeredBrickSpawnPosition, _tileTexture, _tileSize);
            var highScoreManager = new HighScoreManager(_eventAggregator);
            var userInterfaceManager = new UserInterfaceManager(_eventAggregator, statisticsPosition, _tileSize, _textTexture, _textSize,
                Color.Black);

            _managers = new List<IManager>
            {
                inputManager,
                collisionManager,
                levelManager,
                brickManager,
                highScoreManager,
                userInterfaceManager
            };
       }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            foreach (var manager in _managers)
            {
                manager.Load();
            }
        }

        public void Unload()
        {
            foreach (var manager in _managers)
            {
                manager.Unload();
            }
            _eventAggregator.Unsubscribe(this);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var manager in _managers)
            {
                manager.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var manager in _managers)
            {
                manager.Draw(spriteBatch);
            }
        }
    }
}