using System;
using Avalonia;
using Avalonia.Controls;

namespace Flowery.Controls
{
    public class DaisyPagination : ListBox
    {
        protected override Type StyleKeyOverride => typeof(DaisyPagination);

        // Pagination acts like a radio group of buttons. ListBox is a good fit.
    }
}
