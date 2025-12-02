using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;

namespace Flowery.Controls
{
    public enum DaisyAlertVariant
    {
        Info,
        Success,
        Warning,
        Error
    }

    /// <summary>
    /// An Alert control styled after DaisyUI's Alert component.
    /// </summary>
    public class DaisyAlert : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyAlert);

        public static readonly StyledProperty<DaisyAlertVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyAlert, DaisyAlertVariant>(nameof(Variant), DaisyAlertVariant.Info);

        public DaisyAlertVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public static readonly StyledProperty<object?> IconProperty =
            AvaloniaProperty.Register<DaisyAlert, object?>(nameof(Icon));

        public object? Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
    }
}
