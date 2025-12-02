using System;
using Avalonia;
using Avalonia.Controls;

namespace Flowery.Controls
{
    public class DaisyHero : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyHero);

        // Background Image property?
        // Or just allow user to put Image in Background.
        // DaisyUI Hero usually has `hero-overlay` and `hero-content`.
    }
}
