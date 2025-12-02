using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Flowery.NET.Gallery.Examples;

public partial class HomePage : UserControl
{
    public event EventHandler? BrowseComponentsRequested;

    public HomePage()
    {
        InitializeComponent();
    }

    public void BrowseBtn_Click(object? sender, RoutedEventArgs e)
    {
        BrowseComponentsRequested?.Invoke(this, EventArgs.Empty);
    }

    private void OpenUrl(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Process.Start("xdg-open", url);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            Process.Start("open", url);
    }

    public void GitHubBtn_Click(object? sender, RoutedEventArgs e)
    {
        OpenUrl("https://github.com/tobitege/Flowery.NET");
    }

    public void DaisyUIBtn_Click(object? sender, RoutedEventArgs e)
    {
        OpenUrl("https://daisyui.com");
    }

    public void AvaloniaBtn_Click(object? sender, RoutedEventArgs e)
    {
        OpenUrl("https://avaloniaui.net");
    }
}
