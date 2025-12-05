<!-- Supplementary documentation for DaisyStatusIndicator -->
<!-- This content is merged into auto-generated docs by generate_docs.py -->

# Overview

DaisyStatusIndicator shows a status dot with optional animations: ping (expanding ripple) or bounce. It supports **8 colors** and **5 sizes**, making it suitable for online/offline markers, alerts, or activity indicators.

## Options

| Property | Description |
|----------|-------------|
| `Color` | Neutral, Primary, Secondary, Accent, Info, Success, Warning, Error (sets dot fill). |
| `Size` | ExtraSmall, Small, Medium (default), Large, ExtraLarge (adjusts diameter). |
| `IsPing` | Shows an expanding fade-out ripple behind the dot. |
| `IsBounce` | Bounces the main dot vertically. |
| `AccessibleText` | Custom text for screen readers. When null, auto-derived from Color (see below). |

## Accessibility Support

DaisyStatusIndicator includes built-in accessibility for screen readers. Since status indicators convey meaning purely through color, the control automatically provides semantic text based on the `Color` property.

### Default Accessible Text by Color

| Color | Default Text | Typical Use Case |
|-------|--------------|------------------|
| Success | "Online" | User/service availability |
| Error | "Error" | Error states, offline |
| Warning | "Warning" | Caution states |
| Info | "Information" | Informational markers |
| Primary | "Active" | Active/selected state |
| Secondary | "Secondary" | Secondary state |
| Accent | "Highlighted" | Highlighted items |
| Neutral | "Status" | Generic status |

### Custom Accessible Text

Override the automatic text with the `AccessibleText` property:

```xml
<!-- Auto: announces "Online" -->
<controls:DaisyStatusIndicator Color="Success" />

<!-- Custom: announces "User is available" -->
<controls:DaisyStatusIndicator Color="Success" AccessibleText="User is available" />

<!-- Custom: announces "Server offline" -->
<controls:DaisyStatusIndicator Color="Error" AccessibleText="Server offline" />

<!-- Custom: announces "3 unread messages" -->
<controls:DaisyStatusIndicator Color="Primary" AccessibleText="3 unread messages" />
```

## Quick Examples

```xml
<!-- Basic status colors -->
<StackPanel Orientation="Horizontal" Spacing="8">
    <controls:DaisyStatusIndicator Color="Neutral" />
    <controls:DaisyStatusIndicator Color="Success" />
    <controls:DaisyStatusIndicator Color="Warning" />
    <controls:DaisyStatusIndicator Color="Error" />
</StackPanel>

<!-- Ping and bounce -->
<controls:DaisyStatusIndicator Color="Error" IsPing="True" />
<controls:DaisyStatusIndicator Color="Info" IsBounce="True" />

<!-- Compact sizing -->
<controls:DaisyStatusIndicator Size="ExtraSmall" Color="Success" />
<controls:DaisyStatusIndicator Size="Large" Color="Primary" />
```

## Tips & Best Practices

- Use **Ping** for attention-grabbing alerts; use **Bounce** for subtle activity cues.
- Keep indicators small near text/icons (ExtraSmall/Small) and larger for standalone badges.
- Place inside `DaisyIndicator` when overlaying on other controls (avatars/cards).
- Avoid combining Ping and Bounce simultaneously for clarity.
