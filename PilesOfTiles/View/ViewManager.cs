using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.HighScore.Messages;
using PilesOfTiles.Level.Messages;
using PilesOfTiles.View.Messages;
using GameEnded = PilesOfTiles.HighScore.Messages.GameEnded;

namespace PilesOfTiles.View
{
    public class ViewManager : IView, IHandle<StartGame>, IHandle<ResumeGame>, IHandle<ShowHighScoreBoard>, IHandle<GameEnded>, IHandle<ReturnToStartMenu>, IHandle<GamePaused>
    {
        private IEventAggregator _eventAggregator;
        private IView _startView;
        private IView _playingView;
        private IView _gamePausedView;
        private IView _gameEndedView;
        private IView _highScoreView;
        private IView _currentView;

        public ViewManager(IEventAggregator eventAggregator, IView startView, IView playingView,
            IView gamePausedView, IView gameEndedView, IView highScoreView)
        {
            _eventAggregator = eventAggregator;

            _startView = startView;
            _playingView = playingView;
            _gamePausedView = gamePausedView;
            _gameEndedView = gameEndedView;
            _highScoreView = highScoreView;

            Load(_startView);
        }

        public void Handle(ReturnToStartMenu message)
        {
            _currentView.Unload();
            Load(_startView);
        }

        public void Handle(StartGame message)
        {
            _currentView.Unload();
            Load(_playingView);
            _eventAggregator.PublishOnUIThread(new GameStarted());
        }

        public void Handle(ResumeGame message)
        {
            _currentView.Unload();
            Load(_playingView);
        }

        public void Handle(GamePaused message)
        {
            _currentView.Unload();
            Load(_gamePausedView);
        }

        public void Handle(ShowHighScoreBoard message)
        {
            _currentView.Unload();
            Load(_highScoreView);
        }

        public void Handle(GameEnded message)
        {
            _currentView.Unload();
            Load(_gameEndedView);
            _eventAggregator.PublishOnUIThread(new Messages.GameEnded
            {
                CauseBy = message.CausedBy,
                Score = message.Score,
                DifficultyLevel = message.DifficultyLevel
            });
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
