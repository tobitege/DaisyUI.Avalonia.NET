using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace Flowery.NET.Gallery.Examples;

public partial class ActionsExamples : UserControl, IScrollableExample
{
    public event EventHandler? OpenModalRequested;

    public ActionsExamples()
    {
        InitializeComponent();
    }

    public void OpenModalBtn_Click(object? sender, RoutedEventArgs e)
    {
        OpenModalRequested?.Invoke(this, EventArgs.Empty);
    }

    public void ScrollToSection(string sectionName)
    {
        var scrollViewer = this.FindControl<ScrollViewer>("MainScrollViewer");
        if (scrollViewer == null) return;

        var sectionHeader = this.GetVisualDescendants()
            .OfType<SectionHeader>()
            .FirstOrDefault(h => h.Title.StartsWith(sectionName, StringComparison.OrdinalIgnoreCase));

        if (sectionHeader?.Parent is Visual parent)
        {
            var transform = parent.TransformToVisual(scrollViewer);
            if (transform.HasValue)
            {
                var point = transform.Value.Transform(new Point(0, 0));
                scrollViewer.Offset = new Vector(0, point.Y);
            }
        }
    }
}
