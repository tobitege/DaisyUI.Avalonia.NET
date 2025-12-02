using System;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace Flowery.Controls
{
    public enum DaisyStatVariant
    {
        Default,
        Primary,
        Secondary,
        Accent,
        Info,
        Success,
        Warning,
        Error
    }

    public class DaisyStat : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyStat);

        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<DaisyStat, string>(nameof(Title));

        public string Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly StyledProperty<string> ValueProperty =
            AvaloniaProperty.Register<DaisyStat, string>(nameof(Value));

        public string Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly StyledProperty<string> DescriptionProperty =
            AvaloniaProperty.Register<DaisyStat, string>(nameof(Description));

        public string Description
        {
            get => GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly StyledProperty<object?> FigureProperty =
            AvaloniaProperty.Register<DaisyStat, object?>(nameof(Figure));

        public object? Figure
        {
            get => GetValue(FigureProperty);
            set => SetValue(FigureProperty, value);
        }

        public static readonly StyledProperty<object?> ActionsProperty =
            AvaloniaProperty.Register<DaisyStat, object?>(nameof(Actions));

        public object? Actions
        {
            get => GetValue(ActionsProperty);
            set => SetValue(ActionsProperty, value);
        }

        public static readonly StyledProperty<bool> IsCenteredProperty =
            AvaloniaProperty.Register<DaisyStat, bool>(nameof(IsCentered), false);

        public bool IsCentered
        {
            get => GetValue(IsCenteredProperty);
            set => SetValue(IsCenteredProperty, value);
        }

        public static readonly StyledProperty<DaisyStatVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyStat, DaisyStatVariant>(nameof(Variant), DaisyStatVariant.Default);

        public DaisyStatVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }
    }

    public class DaisyStats : ItemsControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyStats);

        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<DaisyStats, Orientation>(nameof(Orientation), Orientation.Horizontal);

        public Orientation Orientation
        {
            get => GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        static DaisyStats()
        {
            OrientationProperty.Changed.AddClassHandler<DaisyStats>((x, _) => x.UpdateChildBorders());
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            UpdateChildBorders();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == ItemCountProperty)
            {
                UpdateChildBorders();
            }
        }

        private void UpdateChildBorders()
        {
            var items = Items;
            if (items == null) return;

            var dividerBrush = this.FindResource("DaisyBase300Brush") as IBrush ?? Brushes.Gray;
            var isHorizontal = Orientation == Orientation.Horizontal;
            var index = 0;

            foreach (var item in items)
            {
                if (item is DaisyStat stat)
                {
                    if (index == 0)
                    {
                        stat.BorderThickness = new Thickness(0);
                    }
                    else
                    {
                        stat.BorderThickness = isHorizontal ? new Thickness(1, 0, 0, 0) : new Thickness(0, 1, 0, 0);
                        stat.BorderBrush = dividerBrush;
                    }
                }
                index++;
            }
        }
    }
}
