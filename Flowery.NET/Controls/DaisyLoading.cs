using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Flowery.Controls
{
    public enum DaisyLoadingVariant
    {
        Spinner,
        Dots,
        Ring,
        Ball,
        Bars,
        Infinity
    }

    public class DaisyLoading : TemplatedControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyLoading);

        public static readonly StyledProperty<DaisyLoadingVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyLoading, DaisyLoadingVariant>(nameof(Variant), DaisyLoadingVariant.Spinner);

        public DaisyLoadingVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyLoading, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
