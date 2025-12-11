using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace Flowery.NET.Gallery.Examples;

public class SongItem
{
    public string Artist { get; set; } = "";
    public string Song { get; set; } = "";
    public IBrush AvatarColor { get; set; } = Brushes.Gray;
}

public partial class DataDisplayExamples : UserControl, IScrollableExample
{
    public List<SongItem> Songs { get; } = new();

    public DataDisplayExamples()
    {
        InitializeComponent();
        InitializeSongData();
        InitializeMarkedDates();
        DataContext = this;
    }

    private void InitializeMarkedDates()
    {
        // Set up marked dates for the BookingTimeline example
        var bookingTimeline = this.FindControl<Flowery.Controls.DaisyDateTimeline>("BookingTimeline");
        var bookingToast = this.FindControl<Flowery.Controls.DaisyToast>("BookingToast");
        
        if (bookingTimeline != null)
        {
            bookingTimeline.MarkedDates = new List<Flowery.Controls.DateMarker>
            {
                new(System.DateTime.Today.AddDays(2), "Dr. Smith - 10:00 AM"),
                new(System.DateTime.Today.AddDays(5), "Team Meeting - 2:00 PM"),
                new(System.DateTime.Today.AddDays(8), "Dentist Appointment"),
                new(System.DateTime.Today.AddDays(12), "Project Review"),
            };

            // Wire up events to show toasts
            bookingTimeline.DateClicked += (s, date) =>
            {
                ShowBookingToast(bookingToast, $"Clicked: {date:ddd, MMM d}", Flowery.Controls.DaisyAlertVariant.Info);
            };

            bookingTimeline.DateConfirmed += (s, date) =>
            {
                ShowBookingToast(bookingToast, $"Confirmed: {date:ddd, MMM d}", Flowery.Controls.DaisyAlertVariant.Success);
            };

            bookingTimeline.EscapePressed += (s, e) =>
            {
                ShowBookingToast(bookingToast, "Jumped to today", Flowery.Controls.DaisyAlertVariant.Warning);
            };
        }
    }

    private void ShowBookingToast(Flowery.Controls.DaisyToast? toast, string message, Flowery.Controls.DaisyAlertVariant variant)
    {
        if (toast == null) return;
        
        var alert = new Flowery.Controls.DaisyAlert
        {
            Content = message,
            Variant = variant
        };
        
        toast.Items.Add(alert);
        
        // Auto-remove after 2 seconds
        var timer = new Avalonia.Threading.DispatcherTimer { Interval = System.TimeSpan.FromSeconds(2) };
        timer.Tick += (s, e) =>
        {
            timer.Stop();
            toast.Items.Remove(alert);
        };
        timer.Start();
    }

    private void InitializeSongData()
    {
        Songs.AddRange(new SongItem[]
        {
            new() { Artist = "Dio Lupa", Song = "REMAINING REASON", AvatarColor = new SolidColorBrush(Color.Parse("#7c3aed")) },
            new() { Artist = "Ellie Beilish", Song = "BEARS OF A FEVER", AvatarColor = new SolidColorBrush(Color.Parse("#db2777")) },
            new() { Artist = "Sabrino Gardener", Song = "CAPPUCCINO", AvatarColor = new SolidColorBrush(Color.Parse("#0d9488")) },
            new() { Artist = "Luna Starr", Song = "MIDNIGHT DREAMS", AvatarColor = new SolidColorBrush(Color.Parse("#2563eb")) },
            new() { Artist = "Max Thunder", Song = "ELECTRIC STORM", AvatarColor = new SolidColorBrush(Color.Parse("#16a34a")) },
            new() { Artist = "Aria Moon", Song = "SILVER LINING", AvatarColor = new SolidColorBrush(Color.Parse("#d97706")) },
            new() { Artist = "Kai Ember", Song = "PHOENIX RISING", AvatarColor = new SolidColorBrush(Color.Parse("#dc2626")) },
            new() { Artist = "Nova Chen", Song = "STARDUST MEMORIES", AvatarColor = new SolidColorBrush(Color.Parse("#8b5cf6")) },
            new() { Artist = "Zara Vox", Song = "ECHOES IN TIME", AvatarColor = new SolidColorBrush(Color.Parse("#ec4899")) },
            new() { Artist = "Leo Frost", Song = "WINTER SUN", AvatarColor = new SolidColorBrush(Color.Parse("#06b6d4")) },
        });
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
                scrollViewer.Offset = new Vector(0, point.Y + scrollViewer.Offset.Y);
            }
        }
    }
}
