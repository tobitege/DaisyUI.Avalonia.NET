using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Flowery.Controls
{
    public enum DaisyButtonVariant
    {
        Default,
        Neutral,
        Primary,
        Secondary,
        Accent,
        Ghost,
        Link,
        Info,
        Success,
        Warning,
        Error
    }

    public enum DaisyButtonStyle
    {
        Default,
        Outline,
        Dash,
        Soft
    }

    public enum DaisyButtonShape
    {
        Default,
        Wide,
        Block,
        Square,
        Circle
    }

    public enum DaisySize
    {
        ExtraSmall,
        Small,
        Medium,
        Large,
        ExtraLarge
    }

    /// <summary>
    /// A Button control styled after DaisyUI's Button component.
    /// </summary>
    public class DaisyButton : Button
    {
        protected override Type StyleKeyOverride => typeof(DaisyButton);

        /// <summary>
        /// Defines the <see cref="Variant"/> property.
        /// </summary>
        public static readonly StyledProperty<DaisyButtonVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyButton, DaisyButtonVariant>(nameof(Variant), DaisyButtonVariant.Default);

        /// <summary>
        /// Gets or sets the visual variant (e.g., Primary, Secondary, Ghost).
        /// </summary>
        public DaisyButtonVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="Size"/> property.
        /// </summary>
        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyButton, DaisySize>(nameof(Size), DaisySize.Medium);

        /// <summary>
        /// Gets or sets the size of the button (ExtraSmall, Small, Medium, Large, ExtraLarge).
        /// </summary>
        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="ButtonStyle"/> property.
        /// </summary>
        public static readonly StyledProperty<DaisyButtonStyle> ButtonStyleProperty =
            AvaloniaProperty.Register<DaisyButton, DaisyButtonStyle>(nameof(ButtonStyle), DaisyButtonStyle.Default);

        /// <summary>
        /// Gets or sets the button style (Default, Outline, Dash, Soft).
        /// </summary>
        public DaisyButtonStyle ButtonStyle
        {
            get => GetValue(ButtonStyleProperty);
            set => SetValue(ButtonStyleProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="Shape"/> property.
        /// </summary>
        public static readonly StyledProperty<DaisyButtonShape> ShapeProperty =
            AvaloniaProperty.Register<DaisyButton, DaisyButtonShape>(nameof(Shape), DaisyButtonShape.Default);

        /// <summary>
        /// Gets or sets the button shape modifier (Default, Wide, Block, Square, Circle).
        /// </summary>
        public DaisyButtonShape Shape
        {
            get => GetValue(ShapeProperty);
            set => SetValue(ShapeProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="IsOutline"/> property.
        /// </summary>
        [Obsolete("Use ButtonStyle = DaisyButtonStyle.Outline instead")]
        public static readonly StyledProperty<bool> IsOutlineProperty =
            AvaloniaProperty.Register<DaisyButton, bool>(nameof(IsOutline));

        /// <summary>
        /// Gets or sets a value indicating whether the button should be an outline button.
        /// </summary>
        [Obsolete("Use ButtonStyle = DaisyButtonStyle.Outline instead")]
        public bool IsOutline
        {
            get => GetValue(IsOutlineProperty);
            set => SetValue(IsOutlineProperty, value);
        }

        /// <summary>
        /// Defines the <see cref="IsActive"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsActiveProperty =
            AvaloniaProperty.Register<DaisyButton, bool>(nameof(IsActive));

        /// <summary>
        /// Gets or sets a value indicating whether the button is in an active (pressed) state.
        /// </summary>
        public bool IsActive
        {
            get => GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }
    }
}
