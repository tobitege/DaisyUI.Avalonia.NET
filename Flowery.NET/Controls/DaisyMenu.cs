using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Flowery.Controls
{
    public class DaisyMenu : ListBox
    {
        protected override Type StyleKeyOverride => typeof(DaisyMenu);

        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<DaisyMenu, Orientation>(nameof(Orientation), Orientation.Vertical);

        public Orientation Orientation
        {
            get => GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }
    }
}
