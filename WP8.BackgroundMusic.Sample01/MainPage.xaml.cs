using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP8.BackgroundMusic.Sample01.Resources;
using Microsoft.Phone.BackgroundAudio;

namespace WP8.BackgroundMusic.Sample01
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Register the PlayStateChanged Event handler.
            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);
        }

        /// <summary>
        /// Checks to see if the BackgroundAudioPlayer is already playing.
        /// Initializes the UI controls accordingly.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                playButton.Content = "Pause";     // Change to pause button
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                       " by " +
                                       BackgroundAudioPlayer.Instance.Track.Artist;

            }
            else
            {
                playButton.Content = "Play";     // Change to play button
                txtCurrentTrack.Text = "";
            }
        }


        /// <summary>
        /// Updates the UI with the current song data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            switch (BackgroundAudioPlayer.Instance.PlayerState)
            {
                case PlayState.Playing:
                    playButton.Content = "Pause";     // Change to pause button
                    prevButton.IsEnabled = true;
                    nextButton.IsEnabled = true;
                    break;

                case PlayState.Paused:
                case PlayState.Stopped:
                    playButton.Content = "Play";     // Change to play button
                    break;
            }

            if (null != BackgroundAudioPlayer.Instance.Track)
            {
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                       " by " +
                                       BackgroundAudioPlayer.Instance.Track.Artist;
            }
        }

        #region Button Click Event Handlers


        /// <summary> 
        /// Tells the background audio agent to skip to the previous track. 
        /// </summary> 
        /// <param name="sender">The button</param> 
        /// <param name="e">Click event args</param> 
        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundAudioPlayer.Instance.SkipPrevious();


            // Prevent the user from repeatedly pressing the button and causing  
            // a backlong of button presses to be handled. This button is re-eneabled  
            // in the TrackReady Playstate handler. 
            prevButton.IsEnabled = false;
        }




        /// <summary> 
        /// Tells the background audio agent to play the current  
        /// track or to pause if we're already playing. 
        /// </summary> 
        /// <param name="sender">The button</param> 
        /// <param name="e">Click event args</param> 
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                BackgroundAudioPlayer.Instance.Pause();
            }
            else
            {
                BackgroundAudioPlayer.Instance.Play();
            }


        }


        /// <summary> 
        /// Tells the background audio agent to skip to the next track. 
        /// </summary> 
        /// <param name="sender">The button</param> 
        /// <param name="e">Click event args</param> 
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundAudioPlayer.Instance.SkipNext();


            // Prevent the user from repeatedly pressing the button and causing  
            // a backlong of button presses to be handled. This button is re-eneabled  
            // in the TrackReady Playstate handler. 
            nextButton.IsEnabled = false;
        }


        #endregion Button Click Event Handlers 
    }
}