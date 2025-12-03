using System;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace Flowery.Controls
{
    public enum DaisyStatusIndicatorColor
    {
        Neutral,
        Primary,
        Secondary,
        Accent,
        Info,
        Success,
        Warning,
        Error
    }

    public class DaisyStatusIndicator : TemplatedControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyStatusIndicator);

        public static readonly StyledProperty<DaisyStatusIndicatorColor> ColorProperty =
            AvaloniaProperty.Register<DaisyStatusIndicator, DaisyStatusIndicatorColor>(nameof(Color), DaisyStatusIndicatorColor.Neutral);

        public DaisyStatusIndicatorColor Color
        {
            get => GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyStatusIndicator, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly StyledProperty<bool> IsPingProperty =
            AvaloniaProperty.Register<DaisyStatusIndicator, bool>(nameof(IsPing), false);

        public bool IsPing
        {
            get => GetValue(IsPingProperty);
            set => SetValue(IsPingProperty, value);
        }

        public static readonly StyledProperty<bool> IsBounceProperty =
            AvaloniaProperty.Register<DaisyStatusIndicator, bool>(nameof(IsBounce), false);

        public bool IsBounce
        {
            get => GetValue(IsBounceProperty);
            set => SetValue(IsBounceProperty, value);
        }
    }
}
