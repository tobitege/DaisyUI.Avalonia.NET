using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Flowery.Controls
{
    public enum DaisyInputVariant
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
    /// A TextBox control styled after DaisyUI's Input component.
    /// </summary>
    public class DaisyInput : TextBox
    {
        protected override Type StyleKeyOverride => typeof(DaisyInput);

        /// <summary>
        /// Defines the <see cref="Variant"/> property.
        /// </summary>
        public static readonly StyledProperty<DaisyInputVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyInput, DaisyInputVariant>(nameof(Variant), DaisyInputVariant.Bordered);

        /// <summary>
        /// Gets or sets the visual variant (e.g., Bordered, Ghost, Primary).
        /// </summary>
        public DaisyInputVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="Size"/> property.
        /// </summary>
        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyInput, DaisySize>(nameof(Size), DaisySize.Medium);

        /// <summary>
        /// Gets or sets the size of the input.
        /// </summary>
        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
