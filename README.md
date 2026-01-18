# Achievement Trigger System

A Unity-based achievement system that detects player interaction with game objects and unlocks platform-specific achievements.

## Features
- Trigger-based collision detection
- Cross-platform achievement integration (Steam, Xbox, PlayStation)
- Reusable component for multiple achievement types

## Technical Implementation
- Uses Unity's `OnTriggerEnter` to detect player collision with achievement zones
- Integrates with platform SDKs (Steamworks, Xbox Live, PlayStation Network)
- Modular design allows easy addition of new achievements

## Usage
1. Attach script to GameObject with trigger collider
2. Assign achievement ID in Inspector
3. System automatically unlocks achievement when player enters trigger zone

## Code Architecture
- **Component-based design** for maximum reusability
- **Platform abstraction layer** handles different SDK implementations
- **Event-driven** achievement unlocking
```
