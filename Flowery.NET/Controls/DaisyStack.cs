using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Flowery.Controls
{
    public class DaisyStack : Grid
    {
        // DaisyStack acts like a grid but visual offsets for children.
        // We can use styles to offset children.
        // It inherits Grid to allow stacking (Grid cells overlap by default).

        // We do not override StyleKeyOverride because we want default Grid behavior + styles.
        // But to attach styles automatically, we need a StyleKey?
        // Actually, Grid doesn't have a ControlTheme.
        // We should make it a custom control that HAS a Styles collection or use a class.

        // If we make it `DaisyStack : Grid`, we can use `Style Selector="controls|DaisyStack > :is(Control)"`.
    }
}
