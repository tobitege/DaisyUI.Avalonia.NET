using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using System.Windows.Input;

namespace Flowery.Controls
{
    public class DaisyFileInput : Button
    {
        protected override Type StyleKeyOverride => typeof(DaisyFileInput);

        public static readonly StyledProperty<string> FileNameProperty =
            AvaloniaProperty.Register<DaisyFileInput, string>(nameof(FileName), "No file chosen");

        public string FileName
        {
            get => GetValue(FileNameProperty);
            set => SetValue(FileNameProperty, value);
        }

        public static readonly StyledProperty<DaisyButtonVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyFileInput, DaisyButtonVariant>(nameof(Variant), DaisyButtonVariant.Default);

        public DaisyButtonVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }

         public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyFileInput, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
