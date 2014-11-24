﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WorldCreator.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WorldCreator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel model;

        public MainPage()
        {
            this.InitializeComponent();
            model = new MainViewModel();
            this.DataContext = model;
            this.NavigationCacheMode = NavigationCacheMode.Required;
            var player = new WorldCreator.Audio.SoundPlayer();
            player.PlaySound("ms-appx:///Assets/Media/Ring01.wma");
        }

        #region Navigation
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            string name = this.PlayersComboBox.SelectedItem as string;
            this.PerformUserCommand(name, model.LogExisitnigPlayer);
        }

        private void Navigate()
        {
            var timer = new DispatcherTimer();
            timer.Tick += (ev, args) =>
            {
                model.IsLoading = true;
                if (model.IsPlayerLogged)
                {
                    timer.Stop();
                    model.IsLoading = false;
                    this.Frame.Navigate(typeof(MainMenuPage), model);
                }
            };
            timer.Interval = new TimeSpan(100);
            timer.Start();
        }

        private void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string name = this.InputPlayerName.Text;
            this.PerformUserCommand(name, model.CreateNewPlayer);
        }

        private void PerformUserCommand(string name, ICommand command)
        {
            command.Execute(name);
            Navigate();
        }
        #endregion
    }
}
