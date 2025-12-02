using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace Flowery.Controls
{
    public class DaisyRating : RangeBase
    {
        protected override Type StyleKeyOverride => typeof(DaisyRating);

        private Control? _foregroundPart;
        private Control? _backgroundPart;

        public DaisyRating()
        {
            Minimum = 0;
            Maximum = 5;
            Value = 0;
            Cursor = Cursor.Parse("Hand");
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyRating, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly StyledProperty<bool> IsReadOnlyProperty =
            AvaloniaProperty.Register<DaisyRating, bool>(nameof(IsReadOnly));

        public bool IsReadOnly
        {
            get => GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            _foregroundPart = e.NameScope.Find<Control>("PART_ForegroundStars");
            _backgroundPart = e.NameScope.Find<Control>("PART_BackgroundStars");

            UpdateVisuals();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == ValueProperty ||
                change.Property == MinimumProperty ||
                change.Property == MaximumProperty)
            {
                UpdateVisuals();
            }
        }

        private void UpdateVisuals()
        {
            if (_foregroundPart == null || _backgroundPart == null) return;

            // Ideally we wait for layout to know the width, but for now we can try to rely on the container size logic.
            // If we use a Grid for layout, we can set the Width of the foreground container wrapper.
            // Actually, we need to know the 'full' width to calculate the percentage.
            // But if we put them in a grid, they have the same size.
            // We just need to clip the foreground.

            // NOTE: In Avalonia, if we change Width of a container, we trigger layout.
            // We want to Clip.

            // Let's rely on the Bounds change to update the Clip Rect.
            InvalidateArrange();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var result = base.ArrangeOverride(finalSize);
            UpdateClip(finalSize);
            return result;
        }

        private void UpdateClip(Size bounds)
        {
            if (_foregroundPart == null) return;

            var range = Maximum - Minimum;
            if (range <= 0) return;

            var percent = (Value - Minimum) / range;
            if (percent < 0) percent = 0;
            if (percent > 1) percent = 1;

            // We clip the foreground part
            var clipWidth = bounds.Width * percent;

            // We can use a RectangleGeometry for clipping
            // or if the template uses a container with Width, we set that Width.
            // Setting Width is easier if the alignment is Left.

            _foregroundPart.Width = clipWidth;
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            if (IsReadOnly) return;

            UpdateValueFromPoint(e.GetPosition(this));
            e.Pointer.Capture(this);
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            base.OnPointerMoved(e);
            if (IsReadOnly) return;

            if (this.Equals(e.Pointer.Captured))
            {
                UpdateValueFromPoint(e.GetPosition(this));
            }
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);
            if (IsReadOnly) return;

            if (this.Equals(e.Pointer.Captured))
            {
                e.Pointer.Capture(null);
            }
        }

        private void UpdateValueFromPoint(Point p)
        {
            var width = Bounds.Width;
            if (width <= 0) return;

            var percent = p.X / width;
            if (percent < 0) percent = 0;
            if (percent > 1) percent = 1;

            var range = Maximum - Minimum;
            var rawValue = (percent * range) + Minimum;

            // Snap logic (optional, default to integer for stars usually)
            // DaisyUI "rating" is usually steps.
            // Let's snap to 1.0 for "click", but "partial fills" (display) are supported.
            // Users usually expect clicking star 3 to give 3.

            var newValue = Math.Ceiling(rawValue);
            // If we want half stars: Math.Ceiling(rawValue * 2) / 2.0;

            // For now, integer snapping feels most "DaisyUI".
            // But if the user clicks exactly on 3.5 area?
            // Let's stick to Integer snapping for interaction, but the Value property can be set to 3.5 programmatically.

            SetCurrentValue(ValueProperty, newValue);
        }
    }
}
