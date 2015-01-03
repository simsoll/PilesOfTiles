using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Collision.Messages;
using PilesOfTiles.Level.Messages;

namespace PilesOfTiles.Screen
{
    public class ViewManager : IView, IHandle<GameOver>, IHandle<GameCompleted>
    {
        private IEventAggregator _eventAggregator;
        private IView _startView;
        private IView _playingView;
        private IView _gamePausedView;
        private IView _gameOverView;
        private IView _gameCompletedView;
        private IView _highScoreView;
        private IView _currentView;

        public ViewManager(IEventAggregator eventAggregator, IView startView, IView playingView,
            IView gamePausedView, IView gameOverView, IView gameCompletedView, IView highScoreView)
        {
            _eventAggregator = eventAggregator;

            _startView = startView;
            _playingView = playingView;
            _gamePausedView = gamePausedView;
            _gameOverView = gameOverView;
            _gameCompletedView = gameCompletedView;
            _highScoreView = highScoreView;

            Load(startView);
        }

        public void Handle(GameOver message)
        {
            _currentView.Unload();
            Load(_gameOverView);
        }

        public void Handle(GameCompleted message)
        {
            _currentView.Unload();
            Load(_gameCompletedView);
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
            _currentView.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentView.Draw(spriteBatch);
        }

        private void Load(IView view)
        {
            _currentView = view;
            _currentView.Load();
        }
    }
}
