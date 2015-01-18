using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Core.Profiler
{
    public class ProfileService : IHandle<object>
    {
        private IEventAggregator _eventAggregator;
        private int _messages;
        private float _framesPerSecond;
        private int[] _messagesPerFrame;
        private int _messagesPerFrameIndex;
        private StringBuilder _stringBuilder;

        public ProfileService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _messages = 0;
            _messagesPerFrame = new int[10];
            _messagesPerFrameIndex = 0;
            _framesPerSecond = 0;
        }

        public void Handle(object message)
        {
            _messages++;
            _messagesPerFrame[_messagesPerFrameIndex]++;
        }

        public void Update(GameTime gameTime)
        {
            _framesPerSecond = 1.0f/(float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Vector2 position)
        {
            _stringBuilder = new StringBuilder();
            _stringBuilder.AppendLine(string.Format("Frames per second: {0}", _framesPerSecond));
            _stringBuilder.AppendLine(string.Format("Messages per frame: {0:0.00}", _messagesPerFrame.Average()));
            _stringBuilder.AppendLine(string.Format("Total messages: {0}", _messages));

            spriteBatch.DrawString(font, _stringBuilder.ToString(), position, Color.Black, 0.0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.0f);

            _messagesPerFrameIndex++;
            if (_messagesPerFrameIndex == _messagesPerFrame.Length)
            {
                _messagesPerFrameIndex = 0;
            }

            if (_messagesPerFrame[_messagesPerFrameIndex] > 0)
            { 
                _messagesPerFrame[_messagesPerFrameIndex] = 0;
            }
        }
    }
}
