using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PilesOfTiles.Screens.Messages;
using GameEnded = PilesOfTiles.HighScore.Messages.GameEnded;

namespace PilesOfTiles.Screens
{
    public class ScreenManager : IScreen, IHandle<StartGame>, IHandle<ResumeGame>, IHandle<ShowHighScoreBoard>, IHandle<GameEnded>, IHandle<ReturnToStartMenu>, IHandle<GamePaused>
    {
        private IEventAggregator _eventAggregator;
        private IScreen _startScreen;
        private IScreen _playingScreen;
        private IScreen _gamePausedScreen;
        private IScreen _gameEndedScreen;
        private IScreen _highScoreScreen;
        private IScreen _currentScreen;

        public ScreenManager(IEventAggregator eventAggregator, IScreen startScreen, IScreen playingScreen,
            IScreen gamePausedScreen, IScreen gameEndedScreen, IScreen highScoreScreen)
        {
            _eventAggregator = eventAggregator;

            _startScreen = startScreen;
            _playingScreen = playingScreen;
            _gamePausedScreen = gamePausedScreen;
            _gameEndedScreen = gameEndedScreen;
            _highScoreScreen = highScoreScreen;

            Load(_startScreen);
        }

        public void Handle(ReturnToStartMenu message)
        {
            _currentScreen.Unload();
            Load(_startScreen);
        }

        public void Handle(StartGame message)
        {
            _currentScreen.Unload();
            Load(_playingScreen);
            _eventAggregator.PublishOnUIThread(new GameStarted());
        }

        public void Handle(ResumeGame message)
        {
            _currentScreen.Unload();
            Load(_playingScreen);
        }

        public void Handle(GamePaused message)
        {
            _currentScreen.Unload();
            Load(_gamePausedScreen);
        }

        public void Handle(ShowHighScoreBoard message)
        {
            _currentScreen.Unload();
            Load(_highScoreScreen);
        }

        public void Handle(GameEnded message)
        {
            _currentScreen.Unload();
            Load(_gameEndedScreen);
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
            _currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScreen.Draw(spriteBatch);
        }

        private void Load(IScreen screen)
        {
            _currentScreen = screen;
            _currentScreen.Load();
        }
    }
}
