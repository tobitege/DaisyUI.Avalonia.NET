using System;
using Avalonia;
using Avalonia.Controls;

namespace Flowery.Controls
{
    public class DaisyModal : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyModal);

        public static readonly StyledProperty<bool> IsOpenProperty =
            AvaloniaProperty.Register<DaisyModal, bool>(nameof(IsOpen));

        public bool IsOpen
        {
            get => GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        // Command to close?
        // DaisyUI modals usually close on backdrop click or close button.
    }
}
