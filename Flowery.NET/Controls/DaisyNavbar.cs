using System;
using Avalonia;
using Avalonia.Controls;

namespace Flowery.Controls
{
    /// <summary>
    /// A top navigation bar styled after DaisyUI's Navbar component.
    /// </summary>
    public class DaisyNavbar : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyNavbar);
    }
}
