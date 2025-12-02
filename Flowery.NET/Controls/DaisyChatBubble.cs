using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;

namespace Flowery.Controls
{
    public enum DaisyChatBubbleVariant
    {
        Default,
        Neutral,
        Primary,
        Secondary,
        Accent,
        Info,
        Success,
        Warning,
        Error
    }

    public class DaisyChatBubble : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyChatBubble);

        public static readonly StyledProperty<bool> IsEndProperty =
            AvaloniaProperty.Register<DaisyChatBubble, bool>(nameof(IsEnd), false);

        public bool IsEnd
        {
            get => GetValue(IsEndProperty);
            set => SetValue(IsEndProperty, value);
        }

        public static readonly StyledProperty<IImage> ImageProperty =
            AvaloniaProperty.Register<DaisyChatBubble, IImage>(nameof(Image));

        public IImage Image
        {
            get => GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public static readonly StyledProperty<string> HeaderProperty =
            AvaloniaProperty.Register<DaisyChatBubble, string>(nameof(Header));

        public string Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly StyledProperty<string> FooterProperty =
            AvaloniaProperty.Register<DaisyChatBubble, string>(nameof(Footer));

        public string Footer
        {
            get => GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }

        public static readonly StyledProperty<DaisyChatBubbleVariant> VariantProperty =
            AvaloniaProperty.Register<DaisyChatBubble, DaisyChatBubbleVariant>(nameof(Variant), DaisyChatBubbleVariant.Default);

        public DaisyChatBubbleVariant Variant
        {
            get => GetValue(VariantProperty);
            set => SetValue(VariantProperty, value);
        }
    }
}
