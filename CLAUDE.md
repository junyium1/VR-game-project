# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Meta Quest VR game built in Unity 6000.3.8f1, targeting Android/Quest hardware. Uses Universal Render Pipeline (URP 17.3.0), Meta XR SDK 85.0.0, and XR Interaction Toolkit 3.3.1.

## Build & Deploy

All builds are done through the Unity Editor (no CLI build scripts exist). To build for Quest:
- **Build target**: Android (Meta Quest)
- **Output**: `game.apk` in project root (produced by Unity's Build Settings)
- Deploy via ADB: `adb install game.apk`

There is no automated test runner or lint step. The Unity Test Framework package is present but no tests are written yet.

## Architecture

### Scenes
- `Assets/Scenes/Main Menu.unity` — title screen
- `Assets/Scenes/Arena Alpha.unity` — combat arena (active gameplay)

### Game Loop (proto)
`GameManager` (singleton, `DontDestroyOnLoad`) drives the state machine:

```
MainMenu → Fighting → RoundOver
                   ↑ (StartRound resets)
```

- `GameState.Fighting`: spawns enemies at random spawnpoints, tracks `enemiesDefeated` vs `enemiesToSpawn`
- `GameManager.enemyKilled()` increments the counter; transitions to `RoundOver` when all enemies are down
- `GameManager.EndRound(winnerName)` is called by `PlayerStats.Die()` on player death

### Player
- **Prefab**: `Assets/Prefabs/[VR_RIG_FULL].prefab` — full Meta Quest VR rig
- `PlayerStats`: lives (heart UI) + stamina (Slider UI). `TakeDamage()` removes one life; `Die()` calls `GameManager.EndRound`.
- `AnimateHandOnInput`: reads `Trigger` and `Grip` float values from `InputActionProperty` and drives hand `Animator` parameters.
- `TeleportationActivator`: toggles an `XRRayInteractor` on/off based on a held input action (teleport on press, disable on release).

### Enemy
- **Prefab**: `Assets/Prefabs/Enemy_Base.prefab`
- `Enemy`: requires `Rigidbody`. `GetHit(direction, force)` applies knockback impulse, notifies `GameManager`, and destroys itself after 3 s.

### Key Prefab
- `Assets/Prefabs/--- GAME MANAGER ---.prefab` — the persistent `GameManager` GameObject that must be present in every scene.

## Key Packages

| Package | Version | Purpose |
|---|---|---|
| Meta XR SDK (`com.meta.xr.sdk.all`) | 85.0.0 | Quest hardware, hand tracking, OVR |
| XR Interaction Toolkit | 3.3.1 | Teleport, ray interactors, XR input |
| Input System | 1.18.0 | `InputActionProperty` on hands/teleport |
| URP | 17.3.0 | Rendering pipeline |
| AI Navigation | 2.0.10 | NavMesh (not yet wired to enemies) |
