using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Flowery.Controls
{
    public class DaisyToast : ItemsControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyToast);

        // We use standard HorizontalAlignment and VerticalAlignment from Control.
        // Defaults will be set in the ControlTheme (Bottom/Right).
    }
}
