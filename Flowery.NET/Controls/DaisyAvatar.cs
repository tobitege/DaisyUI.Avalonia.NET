using System;
using Avalonia;
using Avalonia.Controls;

namespace Flowery.Controls
{
    public class DaisyAvatar : ContentControl
    {
        protected override Type StyleKeyOverride => typeof(DaisyAvatar);

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyAvatar, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly StyledProperty<bool> IsRoundedProperty =
            AvaloniaProperty.Register<DaisyAvatar, bool>(nameof(IsRounded), true); // DaisyUI defaults to circle often or squircle

        public bool IsRounded
        {
            get => GetValue(IsRoundedProperty);
            set => SetValue(IsRoundedProperty, value);
        }

        // DaisyUI Avatars also support "Online/Offline" status rings.
        // We can add a Status property or let user put DaisyIndicator inside?
        // DaisyUI structure: <div class="avatar online"><div class="w-24 rounded-full"><img ... /></div></div>
        // So the avatar container handles the status.

        public static readonly StyledProperty<DaisyStatus> StatusProperty =
            AvaloniaProperty.Register<DaisyAvatar, DaisyStatus>(nameof(Status), DaisyStatus.None);

        public DaisyStatus Status
        {
            get => GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }
    }

    public enum DaisyStatus
    {
        None,
        Online,
        Offline
    }
}
