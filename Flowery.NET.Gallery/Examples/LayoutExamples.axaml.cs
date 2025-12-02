using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Flowery.Controls;

namespace Flowery.NET.Gallery.Examples;

public partial class LayoutExamples : UserControl, IScrollableExample
{
    public LayoutExamples()
    {
        InitializeComponent();
    }

    public void DrawerToggleBtn_Click(object? sender, RoutedEventArgs e)
    {
        var drawer = this.FindControl<DaisyDrawer>("DemoDrawer");
        if (drawer != null) drawer.IsPaneOpen = !drawer.IsPaneOpen;
    }

    public void ScrollToSection(string sectionName)
    {
        var scrollViewer = this.FindControl<ScrollViewer>("MainScrollViewer");
        if (scrollViewer == null) return;

        var sectionHeader = this.GetVisualDescendants()
            .OfType<SectionHeader>()
            .FirstOrDefault(h => h.Title.StartsWith(sectionName, System.StringComparison.OrdinalIgnoreCase));

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
