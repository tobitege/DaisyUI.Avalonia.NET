using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Flowery.Controls
{
    public class DaisyIndicator : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyIndicator);

        public static readonly StyledProperty<object?> BadgeProperty =
            AvaloniaProperty.Register<DaisyIndicator, object?>(nameof(Badge));

        public object? Badge
        {
            get => GetValue(BadgeProperty);
            set => SetValue(BadgeProperty, value);
        }

        public static readonly StyledProperty<HorizontalAlignment> BadgeHorizontalAlignmentProperty =
            AvaloniaProperty.Register<DaisyIndicator, HorizontalAlignment>(nameof(BadgeHorizontalAlignment), HorizontalAlignment.Right);

        public HorizontalAlignment BadgeHorizontalAlignment
        {
            get => GetValue(BadgeHorizontalAlignmentProperty);
            set => SetValue(BadgeHorizontalAlignmentProperty, value);
        }

        public static readonly StyledProperty<VerticalAlignment> BadgeVerticalAlignmentProperty =
            AvaloniaProperty.Register<DaisyIndicator, VerticalAlignment>(nameof(BadgeVerticalAlignment), VerticalAlignment.Top);

        public VerticalAlignment BadgeVerticalAlignment
        {
            get => GetValue(BadgeVerticalAlignmentProperty);
            set => SetValue(BadgeVerticalAlignmentProperty, value);
        }
    }
}
