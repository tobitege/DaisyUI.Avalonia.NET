using System;
using Avalonia;
using Avalonia.Controls;

namespace Flowery.Controls
{
    public class DaisyBreadcrumbs : ItemsControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyBreadcrumbs);

        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            return new DaisyBreadcrumbItem();
        }

        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            recycleKey = null;
            return item is not DaisyBreadcrumbItem;
        }
    }

    public class DaisyBreadcrumbItem : ContentControl
    {
         // Just a container so we can template it
    }
}
