using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Flowery.Controls
{
    public enum DaisyTabVariant
    {
        None,
        Bordered,
        Lifted,
        Boxed
    }

    public class DaisyTabs : TabControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyTabs);

        public static readonly StyledProperty<DaisyTabVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyTabs, DaisyTabVariant>(nameof(Variant), DaisyTabVariant.Bordered);

        public DaisyTabVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

         public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyTabs, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
