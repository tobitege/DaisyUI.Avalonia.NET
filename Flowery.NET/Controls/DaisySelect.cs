using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Flowery.Controls
{
    public enum DaisySelectVariant
    {
        Bordered,
        Ghost,
        Primary,
        Secondary,
        Accent,
        Info,
        Success,
        Warning,
        Error
    }

    /// <summary>
    /// A ComboBox control styled after DaisyUI's Select component.
    /// </summary>
    public class DaisySelect : ComboBox
    {
        protected override Type StyleKeyOverride => typeof(DaisySelect);

        public static readonly StyledProperty<DaisySelectVariant> VariantProperty =
            AvaloniaProperty.Register<DaisySelect, DaisySelectVariant>(nameof(Variant), DaisySelectVariant.Bordered);

        public DaisySelectVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisySelect, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
