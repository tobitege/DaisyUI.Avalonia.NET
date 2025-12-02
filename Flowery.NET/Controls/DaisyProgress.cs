using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Flowery.Controls
{
    public enum DaisyProgressVariant
    {
        Default,
        Primary,
        Secondary,
        Accent,
        Info,
        Success,
        Warning,
        Error
    }

    public class DaisyProgress : ProgressBar
    {
        protected override Type StyleKeyOverride => typeof(DaisyProgress);

        public static readonly StyledProperty<DaisyProgressVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyProgress, DaisyProgressVariant>(nameof(Variant), DaisyProgressVariant.Default);

        public DaisyProgressVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyProgress, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
