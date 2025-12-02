using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Flowery.Controls
{
    public enum DaisyCardVariant
    {
        Normal,
        Compact,
        Side
    }

    /// <summary>
    /// A Card control styled after DaisyUI's Card component.
    /// </summary>
    public class DaisyCard : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyCard);

        public static readonly StyledProperty<DaisyCardVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyCard, DaisyCardVariant>(nameof(Variant), DaisyCardVariant.Normal);

        public DaisyCardVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        // Additional card properties could be added like Title, Actions, Image, but simple ContentControl
        // with a structured template or just styling the container is standard DaisyUI usage
        // (often it's just a div with .card).
        // However, Avalonia controls usually want some slots if we want to enforce structure.
        // For strict port, we'll keep it as a wrapper that applies the style.
    }
}
