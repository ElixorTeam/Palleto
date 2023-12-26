﻿using System.Reflection;
using Microsoft.Maui.Controls;

namespace ScalesHybrid;

public partial class App : Application
{
    private static Mutex _mutex = new(true, Assembly.GetEntryAssembly()?.GetName().Name);
    
    public App()
    {
        if (!_mutex.WaitOne(TimeSpan.Zero, true))
        {
            Current?.Quit();
            Environment.Exit(0);
        }
        
        InitializeComponent();
        MainPage = new MainPage();
    }
}
