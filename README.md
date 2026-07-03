# Anti Spawn TK Plugin

A SCP: Secret Laboratory plugin that prevents MTF team kills during the initial spawn wave.

## Features

✅ **MTF Team Protection** - Blocks MTF-to-MTF damage for 30 seconds after spawn wave  
✅ **Cross-Faction Damage** - MTF can still take damage from SCP, Class-D, and Chaos Insurgency  
✅ **Self-Damage Allowed** - Players can still harm themselves (grenades, explosions)  
✅ **Configurable Protection Duration** - Adjust the protection window as needed  

## How It Works

- When an MTF spawn wave hits the surface, a 30-second protection window begins
- During this window, MTF members **cannot damage each other**
- Other factions (SCP, Class-D, Chaos) **can still damage MTF**
- Self-damage is **always allowed** (grenades will still blow you up)
- After 30 seconds, normal PvP resumes

## Installation

1. Place `AntiSpawnTK.cs` in your plugins folder
2. Restart the server or reload plugins
3. Configure in your plugin config file (optional)

## Configuration

```yaml
antispawntk:
  enabled: true
  debug: false
  protection_duration: 30  # seconds
```

### Config Options

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| `IsEnabled` | bool | `true` | Enable/disable the plugin |
| `Debug` | bool | `false` | Enable debug logging |
| `ProtectionDuration` | float | `30` | Seconds of MTF protection after spawn |

## Version

**v1.0.1** - MTF-only team damage blocking

## Author

Community
