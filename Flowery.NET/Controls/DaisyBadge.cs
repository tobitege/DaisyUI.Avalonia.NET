using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Flowery.Controls
{
    public enum DaisyBadgeVariant
    {
        Default,
        Neutral,
        Primary,
        Secondary,
        Accent,
        Ghost,
        Info,
        Success,
        Warning,
        Error
    }

    /// <summary>
    /// A Badge control styled after DaisyUI's Badge component.
    /// </summary>
    public class DaisyBadge : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyBadge);

        public static readonly StyledProperty<DaisyBadgeVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyBadge, DaisyBadgeVariant>(nameof(Variant), DaisyBadgeVariant.Default);

        public DaisyBadgeVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyBadge, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly StyledProperty<bool> IsOutlineProperty =
            AvaloniaProperty.Register<DaisyBadge, bool>(nameof(IsOutline));

        public bool IsOutline
        {
            get => GetValue(IsOutlineProperty);
            set => SetValue(IsOutlineProperty, value);
        }
    }
}
