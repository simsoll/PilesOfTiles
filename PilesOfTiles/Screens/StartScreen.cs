using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PilesOfTiles.Core.Input.Keyboard.Messages;
using PilesOfTiles.HighScore;
using PilesOfTiles.Screens.Messages;
using PilesOfTiles.UserInterfaces;

namespace PilesOfTiles.Screens
{
    public class StartScreen : IScreen, IHandle<KeyPressed>
    {
        private IEventAggregator _eventAggregator;
        private Texture2D _textTexture;
        private readonly Vector2 _centeredTextPosition;
        private int _textSize;
        private Color _textColorSelected;
        private Color _textColorUnselected;

        private Button[] _buttons;
        private int _selectedButtonIndex;

        private const string StartGameButtonTitle = "Start Game";
        private const string HighScoreBoardButtonTitle = "High Score Board";
        private const string QuitButtonTitle = "Quit";

        private PixelAlfabet _pixelAlfabet;

        public StartScreen(IEventAggregator eventAggregator, Texture2D textTexture, Vector2 centeredTextPosition, int textSize, Color textColorSelected,
            Color textColorUnselected)
        {
            _eventAggregator = eventAggregator;
            _textTexture = textTexture;
            _centeredTextPosition = centeredTextPosition;
            _textSize = textSize;
            _textColorSelected = textColorSelected;
            _textColorUnselected = textColorUnselected;

            _buttons = new[]
            {
                new Button
                {
                    Title = StartGameButtonTitle,
                    IsSelected = true
                },
                new Button
                {
                    Title = HighScoreBoardButtonTitle,
                    IsSelected = false
                },
                new Button
                {
                    Title = QuitButtonTitle,
                    IsSelected = false
                }
            };

            _selectedButtonIndex = 0;

            _pixelAlfabet = new PixelAlfabet();
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var offset = new Vector2(0, 25);
            var buttonIndex = 1;

            foreach (var button in _buttons)
            {
                var buttonPositionOffset = offset * buttonIndex;

                var color = button.IsSelected ? _textColorSelected : _textColorUnselected;
                _pixelAlfabet.DrawTextCentered(spriteBatch, button.Title, _textTexture, _centeredTextPosition + buttonPositionOffset, _textSize,
                    color);

                buttonIndex++;
            }
        }


        public void Handle(KeyPressed message)
        {
            switch (message.Key)
            {
                case Keys.Enter:
                    PressSelectedButton();
                    break;
                case Keys.Down:
                    SelectNextButton();
                    break;
                case Keys.Up:
                    SelectPreviousButton();
                    break;
            }
        }

        private void SelectPreviousButton()
        {
            _buttons[_selectedButtonIndex].IsSelected = false;

            if (_selectedButtonIndex == 0)
            {
                _selectedButtonIndex = _buttons.Length - 1;
            }
            else
            {
                _selectedButtonIndex--;
            }
            _buttons[_selectedButtonIndex].IsSelected = true;
        }

        private void SelectNextButton()
        {
            _buttons[_selectedButtonIndex].IsSelected = false;

            if (_selectedButtonIndex == _buttons.Length - 1)
            {
                _selectedButtonIndex = 0;
            }
            else
            {
                _selectedButtonIndex++;
            }
            _buttons[_selectedButtonIndex].IsSelected = true;
        }

        private void PressSelectedButton()
        {
            switch (_buttons[_selectedButtonIndex].Title)
            {
                case StartGameButtonTitle:
                    _eventAggregator.PublishOnUIThread(new StartGame());
                    break;
                case HighScoreBoardButtonTitle:
                    _eventAggregator.PublishOnUIThread(new ShowHighScoreBoard());
                    break;
                case QuitButtonTitle:
                    _eventAggregator.PublishOnUIThread(new QuitGame());
                    break;
            }
        }

        internal class Button
        {
            public string Title { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}