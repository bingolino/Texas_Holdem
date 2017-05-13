﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace Client
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private UserData user;
        private MainWindow mainWindow;

        public ProfileWindow(UserData user, MainWindow mainWindow)
        {
            InitializeComponent();
            this.user = user;
            this.mainWindow = mainWindow;
            UsernameTxt.Text = user.username;
            PasswordTxt.Text = user.password;
            EmailTxt.Text = user.email;
        }

        private void AvatarButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            Stream avatarStream = null;
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                avatarStream = dlg.OpenFile();
                var content = new StreamContent(avatarStream);
                content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg"); // TODO: change jpeg
                RestClient.MakePostRequest(content.ToString());
            }
            else
            {
                // handle
            }
        }

        // POST: api/Login?username=elad
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            RestClient.SetController("Login?username=" + user.username);
            string ans = RestClient.MakePostRequest("{\"username\":\"" + UsernameTxt.Text + "\"," +
                                                    "\"password\":\"" + PasswordTxt.Text + "\"," +
                                                    "\"email\":\"" + EmailTxt.Text + "\"}" /*"\"," +
                                                    "\"avatar\":\"" + EmailTxt.Text + "\"}"*/);
            if (ans == "\"\"")
            {
                MessageBox.Show("User edited succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                mainWindow.UpdateUserLabels(UsernameTxt.Text, user.chips); //TODO: add avatar update
            }
            else
            {
                MessageBox.Show(ans, "Error in edit", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}