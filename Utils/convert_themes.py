#!/usr/bin/env python3
"""
Convert DaisyUI CSS theme files to Avalonia AXAML ResourceDictionary format.
Uses the existing Daisy* key naming convention from Flowery.NET.
"""

import os
import re
import math
from pathlib import Path


def oklch_to_rgb(l: float, c: float, h: float) -> tuple[int, int, int]:
    """Convert OKLCH to RGB (0-255)."""
    # OKLCH -> OKLab
    a = c * math.cos(math.radians(h))
    b = c * math.sin(math.radians(h))

    # OKLab -> linear sRGB
    l_ = l + 0.3963377774 * a + 0.2158037573 * b
    m_ = l - 0.1055613458 * a - 0.0638541728 * b
    s_ = l - 0.0894841775 * a - 1.2914855480 * b

    l_cubed = l_ ** 3
    m_cubed = m_ ** 3
    s_cubed = s_ ** 3

    r_linear = +4.0767416621 * l_cubed - 3.3077115913 * m_cubed + 0.2309699292 * s_cubed
    g_linear = -1.2684380046 * l_cubed + 2.6097574011 * m_cubed - 0.3413193965 * s_cubed
    b_linear = -0.0041960863 * l_cubed - 0.7034186147 * m_cubed + 1.7076147010 * s_cubed

    # Linear sRGB -> sRGB (gamma correction)
    def gamma(x):
        if x <= 0.0031308:
            return 12.92 * x
        return 1.055 * (x ** (1 / 2.4)) - 0.055

    r = gamma(r_linear)
    g = gamma(g_linear)
    b = gamma(b_linear)

    # Clamp and convert to 0-255
    r = max(0, min(255, round(r * 255)))
    g = max(0, min(255, round(g * 255)))
    b = max(0, min(255, round(b * 255)))

    return (r, g, b)


def oklch_to_hex(oklch_str: str) -> str:
    """Parse OKLCH string and convert to hex color."""
    # Parse: "28.822% 0.022 277.508" or "28.822 0.022 277.508"
    parts = oklch_str.strip().split()
    if len(parts) != 3:
        raise ValueError(f"Invalid OKLCH format: {oklch_str}")

    l_str, c_str, h_str = parts

    # L can be percentage or 0-1
    if l_str.endswith('%'):
        l = float(l_str[:-1]) / 100.0
    else:
        l = float(l_str)

    c = float(c_str)
    h = float(h_str)

    r, g, b = oklch_to_rgb(l, c, h)
    return f"#{r:02X}{g:02X}{b:02X}"


def darken_hex(hex_color: str, factor: float = 0.8) -> str:
    """Darken a hex color by a factor (0-1)."""
    hex_color = hex_color.lstrip('#')
    r = int(hex_color[0:2], 16)
    g = int(hex_color[2:4], 16)
    b = int(hex_color[4:6], 16)

    r = int(r * factor)
    g = int(g * factor)
    b = int(b * factor)

    return f"#{r:02X}{g:02X}{b:02X}"


def parse_css_theme(css_content: str) -> dict:
    """Parse DaisyUI CSS theme file and extract colors."""
    theme = {
        'is_dark': False,
        'colors': {}
    }

    # Check color scheme
    scheme_match = re.search(r'color-scheme:\s*(light|dark)', css_content, re.IGNORECASE)
    if scheme_match:
        theme['is_dark'] = scheme_match.group(1).lower() == 'dark'

    # Parse OKLCH colors
    color_pattern = re.compile(r'--color-([a-z0-9-]+):\s*oklch\(([^)]+)\)', re.IGNORECASE)
    for match in color_pattern.finditer(css_content):
        key = match.group(1)
        oklch_value = match.group(2).strip()
        try:
            hex_color = oklch_to_hex(oklch_value)
            theme['colors'][key] = hex_color
        except Exception as e:
            print(f"  Warning: Failed to parse {key}: {oklch_value} - {e}")

    return theme


# Mapping from DaisyUI CSS keys to Avalonia resource keys (Daisy* format)
KEY_MAPPING = {
    'base-100': ('DaisyBase100Color', 'DaisyBase100Brush'),
    'base-200': ('DaisyBase200Color', 'DaisyBase200Brush'),
    'base-300': ('DaisyBase300Color', 'DaisyBase300Brush'),
    'base-content': ('DaisyBaseContentColor', 'DaisyBaseContentBrush'),
    'primary': ('DaisyPrimaryColor', 'DaisyPrimaryBrush'),
    'primary-content': ('DaisyPrimaryContentColor', 'DaisyPrimaryContentBrush'),
    'secondary': ('DaisySecondaryColor', 'DaisySecondaryBrush'),
    'secondary-content': ('DaisySecondaryContentColor', 'DaisySecondaryContentBrush'),
    'accent': ('DaisyAccentColor', 'DaisyAccentBrush'),
    'accent-content': ('DaisyAccentContentColor', 'DaisyAccentContentBrush'),
    'neutral': ('DaisyNeutralColor', 'DaisyNeutralBrush'),
    'neutral-content': ('DaisyNeutralContentColor', 'DaisyNeutralContentBrush'),
    'info': ('DaisyInfoColor', 'DaisyInfoBrush'),
    'info-content': ('DaisyInfoContentColor', 'DaisyInfoContentBrush'),
    'success': ('DaisySuccessColor', 'DaisySuccessBrush'),
    'success-content': ('DaisySuccessContentColor', 'DaisySuccessContentBrush'),
    'warning': ('DaisyWarningColor', 'DaisyWarningBrush'),
    'warning-content': ('DaisyWarningContentColor', 'DaisyWarningContentBrush'),
    'error': ('DaisyErrorColor', 'DaisyErrorBrush'),
    'error-content': ('DaisyErrorContentColor', 'DaisyErrorContentBrush'),
}


def generate_axaml(theme_name: str, theme: dict) -> str:
    """Generate Avalonia AXAML ResourceDictionary content."""
    lines = [
        '<ResourceDictionary xmlns="https://github.com/avaloniaui"',
        '                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">',
        f'    <!-- DaisyUI {theme_name.title()} Theme Palette -->',
    ]

    colors = theme['colors']

    # Primary
    if 'primary' in colors:
        lines.append('    <!-- Primary -->')
        hex_val = colors['primary']
        lines.append(f'    <Color x:Key="DaisyPrimaryColor">{hex_val}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyPrimaryBrush" Color="{{StaticResource DaisyPrimaryColor}}" />')
        focus_hex = darken_hex(hex_val, 0.8)
        lines.append(f'    <Color x:Key="DaisyPrimaryFocusColor">{focus_hex}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyPrimaryFocusBrush" Color="{{StaticResource DaisyPrimaryFocusColor}}" />')
        content_hex = colors.get('primary-content', '#ffffff')
        lines.append(f'    <Color x:Key="DaisyPrimaryContentColor">{content_hex}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyPrimaryContentBrush" Color="{{StaticResource DaisyPrimaryContentColor}}" />')
        lines.append('')

    # Secondary
    if 'secondary' in colors:
        lines.append('    <!-- Secondary -->')
        hex_val = colors['secondary']
        lines.append(f'    <Color x:Key="DaisySecondaryColor">{hex_val}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisySecondaryBrush" Color="{{StaticResource DaisySecondaryColor}}" />')
        focus_hex = darken_hex(hex_val, 0.8)
        lines.append(f'    <Color x:Key="DaisySecondaryFocusColor">{focus_hex}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisySecondaryFocusBrush" Color="{{StaticResource DaisySecondaryFocusColor}}" />')
        content_hex = colors.get('secondary-content', '#ffffff')
        lines.append(f'    <Color x:Key="DaisySecondaryContentColor">{content_hex}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisySecondaryContentBrush" Color="{{StaticResource DaisySecondaryContentColor}}" />')
        lines.append('')

    # Accent
    if 'accent' in colors:
        lines.append('    <!-- Accent -->')
        hex_val = colors['accent']
        lines.append(f'    <Color x:Key="DaisyAccentColor">{hex_val}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyAccentBrush" Color="{{StaticResource DaisyAccentColor}}" />')
        focus_hex = darken_hex(hex_val, 0.8)
        lines.append(f'    <Color x:Key="DaisyAccentFocusColor">{focus_hex}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyAccentFocusBrush" Color="{{StaticResource DaisyAccentFocusColor}}" />')
        content_hex = colors.get('accent-content', '#163835')
        lines.append(f'    <Color x:Key="DaisyAccentContentColor">{content_hex}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyAccentContentBrush" Color="{{StaticResource DaisyAccentContentColor}}" />')
        lines.append('')

    # Neutral
    if 'neutral' in colors:
        lines.append('    <!-- Neutral -->')
        hex_val = colors['neutral']
        lines.append(f'    <Color x:Key="DaisyNeutralColor">{hex_val}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyNeutralBrush" Color="{{StaticResource DaisyNeutralColor}}" />')
        focus_hex = darken_hex(hex_val, 0.8)
        lines.append(f'    <Color x:Key="DaisyNeutralFocusColor">{focus_hex}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyNeutralFocusBrush" Color="{{StaticResource DaisyNeutralFocusColor}}" />')
        content_hex = colors.get('neutral-content', '#ffffff')
        lines.append(f'    <Color x:Key="DaisyNeutralContentColor">{content_hex}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyNeutralContentBrush" Color="{{StaticResource DaisyNeutralContentColor}}" />')
        lines.append('')

    # Base
    if 'base-100' in colors:
        lines.append('    <!-- Base -->')
        lines.append(f'    <Color x:Key="DaisyBase100Color">{colors["base-100"]}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyBase100Brush" Color="{{StaticResource DaisyBase100Color}}" />')
        lines.append(f'    <Color x:Key="DaisyBase200Color">{colors.get("base-200", colors["base-100"])}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyBase200Brush" Color="{{StaticResource DaisyBase200Color}}" />')
        lines.append(f'    <Color x:Key="DaisyBase300Color">{colors.get("base-300", colors["base-100"])}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyBase300Brush" Color="{{StaticResource DaisyBase300Color}}" />')
        content_hex = colors.get('base-content', '#1f2937')
        lines.append(f'    <Color x:Key="DaisyBaseContentColor">{content_hex}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyBaseContentBrush" Color="{{StaticResource DaisyBaseContentColor}}" />')
        lines.append('    ')

    # Info, Success, Warning, Error
    lines.append('    <!-- Info, Success, Warning, Error -->')
    if 'info' in colors:
        lines.append(f'    <Color x:Key="DaisyInfoColor">{colors["info"]}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyInfoBrush" Color="{{StaticResource DaisyInfoColor}}" />')
        lines.append('    ')

    if 'success' in colors:
        lines.append(f'    <Color x:Key="DaisySuccessColor">{colors["success"]}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisySuccessBrush" Color="{{StaticResource DaisySuccessColor}}" />')
        lines.append('    ')

    if 'warning' in colors:
        lines.append(f'    <Color x:Key="DaisyWarningColor">{colors["warning"]}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyWarningBrush" Color="{{StaticResource DaisyWarningColor}}" />')
        lines.append('    ')

    if 'error' in colors:
        lines.append(f'    <Color x:Key="DaisyErrorColor">{colors["error"]}</Color>')
        lines.append(f'    <SolidColorBrush x:Key="DaisyErrorBrush" Color="{{StaticResource DaisyErrorColor}}" />')
        lines.append('    ')

    lines.append('</ResourceDictionary>')

    return '\n'.join(lines)


def convert_theme_file(css_path: Path, output_dir: Path):
    """Convert a single CSS theme file to AXAML."""
    theme_name = css_path.stem
    print(f"Converting {theme_name}...")

    css_content = css_path.read_text(encoding='utf-8')
    theme = parse_css_theme(css_content)

    # Generate Pascal case name for file
    pascal_name = ''.join(word.title() for word in theme_name.split('-'))
    output_path = output_dir / f"Daisy{pascal_name}.axaml"

    axaml_content = generate_axaml(theme_name, theme)
    output_path.write_text(axaml_content, encoding='utf-8')

    print(f"  -> {output_path.name} ({'dark' if theme['is_dark'] else 'light'})")
    return pascal_name, theme['is_dark']


def main():
    script_dir = Path(__file__).parent
    themes_dir = script_dir / 'EmbeddedThemes'
    output_dir = script_dir.parent / 'Flowery.NET' / 'Themes' / 'Palettes'

    css_files = sorted(themes_dir.glob('*.css')) if themes_dir.exists() else []

    if not css_files:
        print(f"No CSS theme files found in '{themes_dir}'")
        print()
        print("Please download DaisyUI CSS theme files and place them in that folder.")
        print("Source: https://github.com/saadeghi/daisyui/tree/master/src/theming/themes")
        return

    output_dir.mkdir(parents=True, exist_ok=True)

    print(f"Found {len(css_files)} CSS theme files")
    print(f"Output directory: {output_dir}")
    print()

    converted = []
    for css_file in css_files:
        try:
            name, is_dark = convert_theme_file(css_file, output_dir)
            converted.append((name, is_dark))
        except Exception as e:
            print(f"  ERROR: {e}")

    print()
    print(f"Converted {len(converted)} themes")
    print()
    print("Light themes:", [n for n, d in converted if not d])
    print("Dark themes:", [n for n, d in converted if d])


if __name__ == '__main__':
    main()
