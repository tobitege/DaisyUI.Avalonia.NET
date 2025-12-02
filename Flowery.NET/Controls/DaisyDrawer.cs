using System;
using Avalonia;
using Avalonia.Controls;

namespace Flowery.Controls
{
    public class DaisyDrawer : SplitView
    {
        protected override Type StyleKeyOverride => typeof(DaisyDrawer);

        // Defaults for DaisyUI Drawer
        public DaisyDrawer()
        {
            DisplayMode = SplitViewDisplayMode.Inline;
            PanePlacement = SplitViewPanePlacement.Left;
            OpenPaneLength = 300; // Typical drawer width
            CompactPaneLength = 0;
            // Don't set IsPaneOpen here - let XAML control it
        }
    }
}
