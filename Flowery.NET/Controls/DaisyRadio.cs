using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Flowery.Controls
{
    public enum DaisyRadioVariant
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
    /// A RadioButton control styled after DaisyUI's Radio component.
    /// </summary>
    public class DaisyRadio : RadioButton
    {
        protected override Type StyleKeyOverride => typeof(DaisyRadio);

        public static readonly StyledProperty<DaisyRadioVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyRadio, DaisyRadioVariant>(nameof(Variant), DaisyRadioVariant.Default);

        public DaisyRadioVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyRadio, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
