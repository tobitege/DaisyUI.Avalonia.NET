using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Flowery.Controls;

namespace Flowery.NET.Gallery.Examples;

public partial class DataInputExamples : UserControl, IScrollableExample
{
    public DataInputExamples()
    {
        InitializeComponent();
    }

    private void OnFeedbackButtonClicked(object? sender, RoutedEventArgs e)
    {
        var toast = this.FindControl<DaisyToast>("DemoToast");
        if (toast != null)
        {
            var textArea = sender as DaisyTextArea;
            var feedbackText = textArea?.Text ?? "No feedback";
            
            var alert = new DaisyAlert
            {
                Content = $"Feedback submitted: \"{feedbackText}\"",
                Variant = DaisyAlertVariant.Success
            };
            toast.Items.Add(alert);
            
            // Auto-remove after 3 seconds
            var timer = new System.Timers.Timer(3000);
            timer.Elapsed += (s, args) =>
            {
                timer.Stop();
                Avalonia.Threading.Dispatcher.UIThread.Post(() => toast.Items.Remove(alert));
            };
            timer.Start();
        }
    }

    public void ScrollToSection(string sectionName)
    {
        var scrollViewer = this.FindControl<ScrollViewer>("MainScrollViewer");
        if (scrollViewer == null) return;

        var sectionHeader = this.GetVisualDescendants()
            .OfType<SectionHeader>()
            .FirstOrDefault(h => h.SectionId == sectionName);

        if (sectionHeader?.Parent is Visual parent)
        {
            var transform = parent.TransformToVisual(scrollViewer);
            if (transform.HasValue)
            {
                var point = transform.Value.Transform(new Point(0, 0));
                // Add current scroll offset to get absolute position in content
                scrollViewer.Offset = new Vector(0, point.Y + scrollViewer.Offset.Y);
            }
        }
    }
}
