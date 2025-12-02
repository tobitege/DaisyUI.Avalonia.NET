using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Flowery.Controls
{
    public class ThemePreviewInfo
    {
        public string Name { get; set; } = "";
        public bool IsDark { get; set; }
        public IBrush Base100 { get; set; } = Brushes.Gray;
        public IBrush BaseContent { get; set; } = Brushes.Gray;
        public IBrush Primary { get; set; } = Brushes.Gray;
        public IBrush Secondary { get; set; } = Brushes.Gray;
        public IBrush Accent { get; set; } = Brushes.Gray;
    }

    public class DaisyThemeDropdown : ComboBox
    {
        protected override Type StyleKeyOverride => typeof(DaisyThemeDropdown);

        public static readonly StyledProperty<string> SelectedThemeProperty =
            AvaloniaProperty.Register<DaisyThemeDropdown, string>(nameof(SelectedTheme), "Light");

        public string SelectedTheme
        {
            get => GetValue(SelectedThemeProperty);
            set => SetValue(SelectedThemeProperty, value);
        }

        private static List<ThemePreviewInfo>? _cachedThemes;

        public DaisyThemeDropdown()
        {
            var themes = GetThemeInfos();
            ItemsSource = themes;
            SelectedIndex = themes.FindIndex(t => t.Name == "Dark");
        }

        private static List<ThemePreviewInfo> GetThemeInfos()
        {
            if (_cachedThemes != null) return _cachedThemes;

            _cachedThemes = new List<ThemePreviewInfo>();

            foreach (var themeInfo in DaisyThemeManager.AvailableThemes)
            {
                var preview = new ThemePreviewInfo { Name = themeInfo.Name, IsDark = themeInfo.IsDark };

                try
                {
                    var paletteUri = new Uri($"avares://Flowery.NET/Themes/Palettes/Daisy{themeInfo.Name}.axaml");
                    var palette = (ResourceDictionary)AvaloniaXamlLoader.Load(paletteUri);

                    if (palette.TryGetResource("DaisyBase100Brush", null, out var base100) && base100 is IBrush b100)
                        preview.Base100 = b100;
                    if (palette.TryGetResource("DaisyBaseContentBrush", null, out var baseContent) && baseContent is IBrush bcb)
                        preview.BaseContent = bcb;
                    if (palette.TryGetResource("DaisyPrimaryBrush", null, out var primary) && primary is IBrush pb)
                        preview.Primary = pb;
                    if (palette.TryGetResource("DaisySecondaryBrush", null, out var secondary) && secondary is IBrush sb)
                        preview.Secondary = sb;
                    if (palette.TryGetResource("DaisyAccentBrush", null, out var accent) && accent is IBrush ab)
                        preview.Accent = ab;
                }
                catch
                {
                    // Use defaults
                }

                _cachedThemes.Add(preview);
            }

            return _cachedThemes;
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == SelectedItemProperty && change.NewValue is ThemePreviewInfo themeInfo)
            {
                SelectedTheme = themeInfo.Name;
                ApplyTheme(themeInfo);
            }
        }

        private void ApplyTheme(ThemePreviewInfo themeInfo)
        {
            DaisyThemeManager.ApplyTheme(themeInfo.Name);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            DaisyThemeManager.ThemeChanged += OnThemeChanged;
            SyncWithCurrentTheme();
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
            DaisyThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object? sender, string themeName)
        {
            SyncWithCurrentTheme();
        }

        private void SyncWithCurrentTheme()
        {
            var currentTheme = DaisyThemeManager.CurrentThemeName;
            if (string.IsNullOrEmpty(currentTheme)) return;

            var themes = GetThemeInfos();
            var match = themes.FirstOrDefault(t => string.Equals(t.Name, currentTheme, StringComparison.OrdinalIgnoreCase));
            if (match != null && SelectedItem != match)
            {
                SelectedItem = match;
            }
        }
    }
}
