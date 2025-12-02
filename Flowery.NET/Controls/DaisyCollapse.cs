using System;
using Avalonia;
using Avalonia.Controls;

namespace Flowery.Controls
{
    public class DaisyCollapse : Expander
    {
        protected override Type StyleKeyOverride => typeof(DaisyCollapse);

        // Inherits Expander logic.
        // DaisyUI Collapse variants:
        // - collapse-arrow: Arrow on right.
        // - collapse-plus: Plus/Minus sign.
        // - Default: No icon usually (just click).

        // We can add a Variant enum if we want to switch icons easily.

        public static readonly StyledProperty<DaisyCollapseVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyCollapse, DaisyCollapseVariant>(nameof(Variant), DaisyCollapseVariant.Arrow);

        public DaisyCollapseVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }
    }

    public enum DaisyCollapseVariant
    {
        Arrow,
        Plus
    }
}
