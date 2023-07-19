﻿namespace PM.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.MinimumWidth = 1440;
            window.MinimumHeight = 810;

            return window;
        }
    }
}