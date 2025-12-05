using System;
using Avalonia;
using Avalonia.Automation.Peers;
using Avalonia.Controls.Primitives;

namespace Flowery.Controls
{
    public enum DaisyStatusIndicatorColor
    {
        Neutral,
        Primary,
        Secondary,
        Accent,
        Info,
        Success,
        Warning,
        Error
    }

    /// <summary>
    /// A status indicator control that displays a small colored dot to represent status.
    /// Includes accessibility support for screen readers via the AccessibleText attached property.
    /// </summary>
    public class DaisyStatusIndicator : TemplatedControl
    {
        private const string DefaultAccessibleText = "Status";

        protected override Type StyleKeyOverride => typeof(DaisyStatusIndicator);

        static DaisyStatusIndicator()
        {
            DaisyAccessibility.SetupAccessibility<DaisyStatusIndicator>(DefaultAccessibleText);

            ColorProperty.Changed.AddClassHandler<DaisyStatusIndicator>((control, _) =>
            {
                control.UpdateAccessibleNameFromColor();
            });
        }

        public static readonly StyledProperty<DaisyStatusIndicatorColor> ColorProperty =
            AvaloniaProperty.Register<DaisyStatusIndicator, DaisyStatusIndicatorColor>(nameof(Color), DaisyStatusIndicatorColor.Neutral);

        public DaisyStatusIndicatorColor Color
        {
            get => GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly StyledProperty<DaisySize> SizeProperty =
            AvaloniaProperty.Register<DaisyStatusIndicator, DaisySize>(nameof(Size), DaisySize.Medium);

        public DaisySize Size
        {
            get => GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly StyledProperty<bool> IsPingProperty =
            AvaloniaProperty.Register<DaisyStatusIndicator, bool>(nameof(IsPing), false);

        public bool IsPing
        {
            get => GetValue(IsPingProperty);
            set => SetValue(IsPingProperty, value);
        }

        public static readonly StyledProperty<bool> IsBounceProperty =
            AvaloniaProperty.Register<DaisyStatusIndicator, bool>(nameof(IsBounce), false);

        public bool IsBounce
        {
            get => GetValue(IsBounceProperty);
            set => SetValue(IsBounceProperty, value);
        }

        /// <summary>
        /// Gets or sets the accessible text announced by screen readers.
        /// When null (default), the text is automatically derived from the Color property.
        /// </summary>
        public string? AccessibleText
        {
            get => DaisyAccessibility.GetAccessibleText(this);
            set => DaisyAccessibility.SetAccessibleText(this, value);
        }

        private void UpdateAccessibleNameFromColor()
        {
            if (DaisyAccessibility.GetAccessibleText(this) == null)
            {
                Avalonia.Automation.AutomationProperties.SetName(this, GetDefaultAccessibleText());
            }
        }

        internal string GetDefaultAccessibleText()
        {
            return Color switch
            {
                DaisyStatusIndicatorColor.Success => "Online",
                DaisyStatusIndicatorColor.Error => "Error",
                DaisyStatusIndicatorColor.Warning => "Warning",
                DaisyStatusIndicatorColor.Info => "Information",
                DaisyStatusIndicatorColor.Primary => "Active",
                DaisyStatusIndicatorColor.Secondary => "Secondary",
                DaisyStatusIndicatorColor.Accent => "Highlighted",
                DaisyStatusIndicatorColor.Neutral => DefaultAccessibleText,
                _ => DefaultAccessibleText
            };
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new DaisyStatusIndicatorAutomationPeer(this);
        }
    }

    /// <summary>
    /// AutomationPeer for DaisyStatusIndicator that exposes it as a status element to assistive technologies.
    /// </summary>
    internal class DaisyStatusIndicatorAutomationPeer : ControlAutomationPeer
    {
        public DaisyStatusIndicatorAutomationPeer(DaisyStatusIndicator owner) : base(owner)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.StatusBar;
        }

        protected override string GetClassNameCore()
        {
            return "DaisyStatusIndicator";
        }

        protected override string? GetNameCore()
        {
            var indicator = (DaisyStatusIndicator)Owner;
            return DaisyAccessibility.GetAccessibleText(indicator) ?? indicator.GetDefaultAccessibleText();
        }

        protected override bool IsContentElementCore() => true;
        protected override bool IsControlElementCore() => true;
    }
}
