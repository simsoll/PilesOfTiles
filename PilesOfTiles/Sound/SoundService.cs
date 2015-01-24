using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Caliburn.Micro;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using PilesOfTiles.DrawEffects.Messages;
using PilesOfTiles.Levels.Messages;
using PilesOfTiles.Particles.Messages;
using PilesOfTiles.Screens.Messages;

namespace PilesOfTiles.Sound
{
    public class SoundService : IHandle<GameStarted>, IHandle<ResumeGame>, IHandle<GamePaused>, IHandle<ScreenIsShaking>, IHandle<ParticleCreated>, IHandle<RowCleared>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Song _backgroundMusic;
        private readonly SoundEffect _shakeEffect;
        private readonly SoundEffect _particleEffect;
        private readonly SoundEffect _rowClearedEffect;

        private readonly TimeSpan _shakeEffectThreshold;
        private readonly TimeSpan _particleEffectThreshold;
        private TimeSpan _timeSinceLastShakeEffect;
        private TimeSpan _timeSinceLastParticleEffect;

        public SoundService(IEventAggregator eventAggregator, Song backgroundMusic, SoundEffect shakeEffect, SoundEffect particleEffect, SoundEffect rowClearedEffect)
        {
            _eventAggregator = eventAggregator;
            _backgroundMusic = backgroundMusic;
            _shakeEffect = shakeEffect;
            _particleEffect = particleEffect;
            _rowClearedEffect = rowClearedEffect;

            _shakeEffectThreshold = TimeSpan.FromMilliseconds(500);
            _particleEffectThreshold = TimeSpan.FromMilliseconds(500);
            _timeSinceLastShakeEffect = TimeSpan.Zero;
            _timeSinceLastParticleEffect = TimeSpan.Zero;

            MediaPlayer.IsRepeating = true;
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
            _timeSinceLastParticleEffect += gameTime.ElapsedGameTime;
            _timeSinceLastShakeEffect += gameTime.ElapsedGameTime;
        }

        public void Handle(GameStarted message)
        {
            MediaPlayer.Play(_backgroundMusic);
        }


        public void Handle(ResumeGame message)
        {
            MediaPlayer.Resume();
        }

        public void Handle(GamePaused message)
        {
            MediaPlayer.Pause();
        }

        public void Handle(ScreenIsShaking message)
        {
            if (_timeSinceLastShakeEffect < _shakeEffectThreshold) return;

            _shakeEffect.Play();
            _timeSinceLastShakeEffect = TimeSpan.Zero;
        }

        public void Handle(ParticleCreated message)
        {
            if (_timeSinceLastParticleEffect < _particleEffectThreshold) return;

            _particleEffect.Play();
            _timeSinceLastParticleEffect = TimeSpan.Zero;
        }

        public void Handle(RowCleared message)
        {
            _rowClearedEffect.Play();
        }
    }
}
