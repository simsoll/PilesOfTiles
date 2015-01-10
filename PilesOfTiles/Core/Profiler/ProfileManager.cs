using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PilesOfTiles.Core.Profiler
{
    public class ProfileManager : IHandle<object>
    {
        private IEventAggregator _eventAggregator;
        private int _messages;
        private float _framesPerSecond;
        private int _messagesThisFrame;
        private StringBuilder _stringBuilder;

        public ProfileManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _messages = 0;
            _messagesThisFrame = 0;
            _framesPerSecond = 0;
        }

        public void Handle(object message)
        {
            _messages++;
            _messagesThisFrame++;
        }

        public void Update(GameTime gameTime)
        {
            _framesPerSecond = 1.0f/(float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Vector2 position)
        {
            _stringBuilder = new StringBuilder();
            _stringBuilder.AppendLine(string.Format("Frames per second: {0}", _framesPerSecond));
            _stringBuilder.AppendLine(string.Format("Messages per frame: {0}", _messages));
            _stringBuilder.AppendLine(string.Format("Total messages: {0}", _messagesThisFrame));

            spriteBatch.DrawString(font, _stringBuilder.ToString(), position, Color.Black, 0.0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0.0f);
            _messages = 0;
        }
    }
}
