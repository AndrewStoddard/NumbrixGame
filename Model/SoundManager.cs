using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace NumbrixGame.Model
{

    /// <summary>
    ///   Defines the Sound Manager
    /// </summary>
    class SoundManager
    {
        private MediaPlayer mediaPlayer;

        /// <summary>Gets or sets a value indicating whether [music on].</summary>
        /// <value>
        /// <c>true</c> if [music on]; otherwise, <c>false</c>.</value>
        public bool MusicOn { get; set; }


        /// <summary>Initializes a new instance of the <see cref="SoundManager" /> class.</summary>
        public SoundManager()
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Tetris.mp3"));
            mediaPlayer.IsLoopingEnabled = true;
            mediaPlayer.Volume = 0.1;
            mediaPlayer.Play();
        }

        /// <summary>Plays the music</summary>
        public void Play()
        {
            this.mediaPlayer.Play();
        }

        /// <summary>Pauses the music</summary>
        public void Pause()
        {
            this.mediaPlayer.Pause();
        }
    }
}
