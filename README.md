# Refactored Tic Tac Toe Game in Unity
Mathias Novakovic GP24

Original script: https://github.com/gitOpu/TicTacToe / https://dev.to/marufhow/create-a-tic-tac-toe-game-in-unity-2lnb

## Summary

This is a refactored version of an original creator's Tic Tac Toe game. To improve the structure and maintainability of the code, I made several design changes guided by Clean Code and SOLID principles:

I applied the Single Responsibility Principle (SRP) by ensuring each class handles just one clearly defined responsibility. For example, the TurnManager only manages player turns, while UI scripts handle only user interface.

I followed the Open/Closed Principle by organizing logic—such as win condition evaluation—in a way that makes it easy to extend (e.g., for a 4x4 board) without needing to modify existing core logic.

Then, I improved encapsulation by exposing game state only through controlled properties and public methods. This helps prevent inconsistent or unintended changes to state across the codebase.

To reduce redundancy, I followed the DRY (Don't Repeat Yourself) principle. I eliminated duplicate patterns and abstracted common logic into reusable structures.

Lastly, I made sure to inform myself of the Unity best practices by correctly using OnEnable and OnDisable for event registration and cleanup, using serialized fields for inspector assignments, and decoupling button click handling from core game logic for better modularity.

---

## Core script updates

### 'BoardManager.cs'
 The BoardManager previously handled both event wiring and game logic. Now it is strictly focused on win/tie checking and board reset logic. (Single responsibility Principle)
 I also made changes to the win condition logic which was a loop. Now win patterns are defined in 'winPatterns' for clarity and reuseability.
 Refactored the win check and tie logic into independent, readable blocks—making it easy to update for larger boards or new rules if the intention is to expand on the game.

### 'Cell.cs'
Restricted 'Value' property to read-only externally. It must now be changed via 'SetValue()' ensuring consistent state and event triggering.
I made clear distinction between gameplay state ( 'Value', 'IsInteractice') and lifecycle ( 'Reset' 'ApplyResult')
I set up to use two distinct events — 'OnValueChanged' for UI updates and 'OnResultApplied' for end-of-game visual feedback to decouple further between
the games logic and the UI.

### 'TurnManager.cs'
The turn manager was refactored to only manage player turns. Removed embedded logic inside UI or cell clicks.

### 'UIBehaviour.cs'
Separated UI handling so that this class now handles only game status display and restart logic.
To prevent memory leaks that are aligned with what usually is defined as a best practice for Unity, you now subscribes/unsubscribes from events in 'OnEnable' / 'OnDisable' to prevent memory leaks.
Refactored game result display logic to separate function 'DisplayResult()' for the purpose of reusability. 

### 'UICellBehaviour.cs'
The UICellBehaviour now only handles button clicks and visual sprite updates and not logic like win detection. 
I changed so that 'IsInteractive' is checked before allowing any input.
Finally the 'OnResultApplied' event now updates only visuals.
