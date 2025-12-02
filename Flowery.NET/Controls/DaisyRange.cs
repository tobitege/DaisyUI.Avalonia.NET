using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Flowery.Controls
{
    public enum DaisyRangeVariant
    {
        Default,
        Primary,
        Secondary,
        Accent,
        Success,
        Warning,
        Info,
        Error
    }

    /// <summary>
    /// A Slider control styled after DaisyUI's Range component.
    /// </summary>
    public class DaisyRange : Slider
    {
        protected override Type StyleKeyOverride => typeof(DaisyRange);

        public static readonly StyledProperty<DaisyRangeVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyRange, DaisyRangeVariant>(nameof(Variant), DaisyRangeVariant.Default);

        public DaisyRangeVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyRange, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
