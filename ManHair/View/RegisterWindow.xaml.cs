﻿using ManHair.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManHair.View
{
    /// <summary>
    /// Interaction logic for RegisterButton.xaml

    public partial class Register : Window
    {
        RegisterViewModel rvm { get; set; }
        MainWindow mv;
        public Register()
        {
            InitializeComponent();
            rvm = new RegisterViewModel();
            DataContext = rvm;
        }
        public Register(MainWindow main)
        {
            InitializeComponent();
            mv = main;
            mv.Hide();
            rvm = new RegisterViewModel();
            DataContext = rvm;
        }
        private void RegisterButton(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text != "" && txtTlf.Text != "" && txtEmail.Text != "" && txtPassword.Password != "" && txtConfirmPassword.Password != "")
            {
                if (txtPassword.Password == txtConfirmPassword.Password)
                {
                    if (rvm.CreateNewCustomer(txtName.Text, Int32.Parse(txtTlf.Text), txtEmail.Text, txtPassword.Password))
                    {
                        MessageBox.Show(rvm.LoginMessage);
                        mv.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(rvm.LoginMessage);
                    }
                }
                else
                {
                    MessageBox.Show("Adgangskoden og BekræftAdgangskode matcher ikke :(");
                }
            }
            else
            {
                MessageBox.Show("Udfyld alle felterne :(");
            }
        }

        private void CloseButto_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void RedirectButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
