# Pretty Blank Island - Unity Systems

Game systems built for Pretty Blank Island, a first-person exploration game developed by Xanthic Games and published by North Harbor Studios. Submitted to Steam, Xbox, and PlayStation.

## Systems Included

* **Achievement Trigger System**: Detects player collision and unlocks platform achievements
* **Main Menu**: Scene loading, game initialization, and settings management
* **Pause Menu**: In-game pause functionality with time control
* **Splash Screen Controller**: Custom animated splash screens for North Harbor Studios and Xanthic Games branding
* **First Person Controller**: Player movement and camera controls
* **Rune Collection System**: Collectible discovery and progression tracking

## Technologies

* Unity Engine (2021 LTS or later)
* C#

## About the Project

Pretty Blank Island is a first-person exploration walking simulator where players discover ancient runes across a mysterious island to uncover its secrets. This repository contains the core gameplay systems developed for cross-platform deployment.

**Developer**: Xanthic Games  
**Publisher**: North Harbor Studios  
**Platforms**: Steam, Xbox Series X|S, PlayStation 5

## Project Structure
```
Assets/
├── Scripts/
│   ├── MainMenu.cs - Main menu functionality
│   ├── PauseMenu.cs - Pause system and settings
│   ├── RuneCollector.cs - Collectible system
│   ├── SplashScreenController.cs - Branded splash screens
│   └── FirstPersonController.cs - Player controls
└── Scenes/
    ├── 0_Splash_Screen - Studio branding
    ├── 1_Main_Menu - Game entry point
    └── 2_Game - Main gameplay scene
```

## Setup Notes

- Cursor management: Automatically locks and hides cursor on game start
- Splash screens: Sequential display of publisher and developer branding
- Scene loading: All scenes must be added to Build Settings in correct order

## License

All rights reserved - North Harbor Games LLC © 2025
