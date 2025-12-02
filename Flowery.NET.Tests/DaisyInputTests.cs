using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Flowery.Controls;
using Xunit;

namespace Flowery.NET.Tests
{
    public class DaisySelectTests
    {
        [AvaloniaFact]
        public void Should_Have_Default_Variant_As_Bordered()
        {
            var select = new DaisySelect();
            Assert.Equal(DaisySelectVariant.Bordered, select.Variant);
        }

        [AvaloniaFact]
        public void Should_Initialize()
        {
            var select = new DaisySelect();
            var window = new Window { Content = select };
            window.Show();

            Assert.NotNull(select);
        }
    }

    public class DaisyTextAreaTests
    {
        [AvaloniaFact]
        public void Should_Have_AcceptsReturn_True()
        {
            var textArea = new DaisyTextArea();
            Assert.True(textArea.AcceptsReturn);
        }
    }
}
