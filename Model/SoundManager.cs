using System;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace NumbrixGame.Model
{
    /// <summary>
    ///     Defines the Sound Manager
    /// </summary>
    internal class SoundManager
    {
        #region Data members

        /// <summary>
        ///     The media player
        /// </summary>
        private readonly MediaPlayer mediaPlayer;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether [music on].
        /// </summary>
        /// <value><c>true</c> if [music on]; otherwise, <c>false</c>.</value>
        public bool MusicOn { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SoundManager" /> class.
        /// </summary>
        public SoundManager()
        {
            this.mediaPlayer = new MediaPlayer {
                Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Tetris.mp3")),
                IsLoopingEnabled = true,
                Volume = 0.1
            };
            this.mediaPlayer.Play();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Plays the music
        /// </summary>
        public void Play()
        {
            this.mediaPlayer.Play();
        }

        /// <summary>
        ///     Pauses the music
        /// </summary>
        public void Pause()
        {
            this.mediaPlayer.Pause();
        }

        #endregion
    }
}