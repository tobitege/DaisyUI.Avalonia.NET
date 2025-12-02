#!/usr/bin/env python3
"""
Fix DaisyUI palette border colors (Base300) to ensure visible borders.

For dark themes: Base300 should be LIGHTER than Base100 to create visible borders
For light themes: Base300 should have sufficient contrast from Base100
"""

import os
import re
from pathlib import Path


def hex_to_rgb(hex_color: str) -> tuple[int, int, int]:
    """Convert hex color to RGB tuple."""
    hex_color = hex_color.lstrip('#')
    return tuple(int(hex_color[i:i+2], 16) for i in (0, 2, 4))


def rgb_to_hex(r: int, g: int, b: int) -> str:
    """Convert RGB to hex color."""
    return f"#{r:02X}{g:02X}{b:02X}"


def get_luminance(hex_color: str) -> float:
    """Calculate relative luminance of a color (0=dark, 1=light)."""
    r, g, b = hex_to_rgb(hex_color)
    # sRGB luminance formula
    return (0.299 * r + 0.587 * g + 0.114 * b) / 255


def is_dark_theme(base100_color: str) -> bool:
    """Determine if a theme is dark based on Base100 luminance."""
    return get_luminance(base100_color) < 0.5


def calculate_good_border_color(base100: str, base200: str, is_dark: bool) -> str:
    """Calculate a good border color with proper contrast."""
    r1, g1, b1 = hex_to_rgb(base100)

    if is_dark:
        # For dark themes: make border LIGHTER than background
        # Add 25-35% to each channel, capped at 255
        factor = 0.30
        r = min(255, int(r1 + (255 - r1) * factor) + 20)
        g = min(255, int(g1 + (255 - g1) * factor) + 20)
        b = min(255, int(b1 + (255 - b1) * factor) + 20)
    else:
        # For light themes: make border DARKER with good contrast
        # Subtract ~15-20% from each channel
        factor = 0.18
        r = max(0, int(r1 * (1 - factor)) - 10)
        g = max(0, int(g1 * (1 - factor)) - 10)
        b = max(0, int(b1 * (1 - factor)) - 10)

    return rgb_to_hex(r, g, b)


def check_contrast(base100: str, base300: str) -> float:
    """Calculate contrast ratio between two colors."""
    lum1 = get_luminance(base100)
    lum2 = get_luminance(base300)
    lighter = max(lum1, lum2)
    darker = min(lum1, lum2)
    return (lighter + 0.05) / (darker + 0.05)


def process_palette_file(filepath: Path) -> dict | None:
    """Process a single palette file and return info about changes needed."""
    content = filepath.read_text(encoding='utf-8')

    # Extract Base100 and Base300 colors
    base100_match = re.search(r'<Color x:Key="DaisyBase100Color">(#[A-Fa-f0-9]{6})</Color>', content)
    base200_match = re.search(r'<Color x:Key="DaisyBase200Color">(#[A-Fa-f0-9]{6})</Color>', content)
    base300_match = re.search(r'<Color x:Key="DaisyBase300Color">(#[A-Fa-f0-9]{6})</Color>', content)

    if not all([base100_match, base200_match, base300_match]):
        return None

    base100 = base100_match.group(1)
    base200 = base200_match.group(1)
    base300 = base300_match.group(1)

    is_dark = is_dark_theme(base100)
    contrast = check_contrast(base100, base300)

    # Determine if fix is needed
    needs_fix = False
    reason = ""

    if is_dark:
        # For dark themes, Base300 should be LIGHTER than Base100
        if get_luminance(base300) <= get_luminance(base100):
            needs_fix = True
            reason = "Dark theme: Base300 is darker than Base100 (borders invisible)"
        elif contrast < 1.3:
            needs_fix = True
            reason = f"Dark theme: Low contrast ({contrast:.2f})"
    else:
        # For light themes, need sufficient contrast
        if contrast < 1.15:
            needs_fix = True
            reason = f"Light theme: Low contrast ({contrast:.2f})"

    new_base300 = calculate_good_border_color(base100, base200, is_dark)

    return {
        'name': filepath.stem,
        'is_dark': is_dark,
        'base100': base100,
        'base200': base200,
        'old_base300': base300,
        'new_base300': new_base300,
        'old_contrast': contrast,
        'new_contrast': check_contrast(base100, new_base300),
        'needs_fix': needs_fix,
        'reason': reason,
        'filepath': filepath,
        'content': content
    }


def apply_fix(info: dict) -> None:
    """Apply the Base300 fix to a palette file."""
    content = info['content']
    old_line = f'<Color x:Key="DaisyBase300Color">{info["old_base300"]}</Color>'
    new_line = f'<Color x:Key="DaisyBase300Color">{info["new_base300"]}</Color>'

    new_content = content.replace(old_line, new_line)
    info['filepath'].write_text(new_content, encoding='utf-8')


def main():
    palettes_dir = Path(__file__).parent.parent / "Flowery.NET" / "Themes" / "Palettes"

    if not palettes_dir.exists():
        print(f"Error: Palettes directory not found: {palettes_dir}")
        return

    results = []
    for axaml_file in sorted(palettes_dir.glob("*.axaml")):
        info = process_palette_file(axaml_file)
        if info:
            results.append(info)

    print("\n" + "="*80)
    print("PALETTE BORDER COLOR ANALYSIS")
    print("="*80)

    # Group by needs fix
    needs_fix = [r for r in results if r['needs_fix']]
    ok = [r for r in results if not r['needs_fix']]

    print(f"\n✓ {len(ok)} palettes OK")
    print(f"✗ {len(needs_fix)} palettes need fixing\n")

    if needs_fix:
        print("-"*80)
        print("PALETTES THAT NEED FIXING:")
        print("-"*80)
        for info in needs_fix:
            theme_type = "DARK" if info['is_dark'] else "LIGHT"
            print(f"\n{info['name']} [{theme_type}]")
            print(f"  Reason: {info['reason']}")
            print(f"  Base100: {info['base100']}")
            print(f"  Old Base300: {info['old_base300']} (contrast: {info['old_contrast']:.2f})")
            print(f"  New Base300: {info['new_base300']} (contrast: {info['new_contrast']:.2f})")

        print("\n" + "-"*80)
        print("Applying fixes...")
        for info in needs_fix:
            apply_fix(info)
            print(f"✓ Fixed {info['name']}")
        print(f"\n✓ Fixed {len(needs_fix)} palette files!")
    else:
        print("All palettes have sufficient border contrast!")


if __name__ == "__main__":
    main()
