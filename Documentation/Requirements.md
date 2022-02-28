## Technology Stack:

- C#  
  Perfect for a game with many various entities and subsystems, supports dynamic module loading and reflection, which can be useful for user-level modding later.
- .NET Core Hosting Extensions  
  A set of extremely useful runtime extensions providing dependency injection, logging, command line & file configuration.
- MonoGame  
  Formerly Microsoft XNA, object-oriented wrapper for DirectX 12 on Windows, SDL 2 on Windows, Linux & Mac OS.

## Functional Requirements (For a Level of MVP)

The game must contain the following entities:

- Maps (containing sttaic landscape of different terrain types and dynamically-destroyed decorations)
- Units (that can move, gather resources, build and fight depending on their type)
- Buildings (providing passive bonuses like extending max army size or allowing the player to produce units)
- Players (for resource accounting and simple statistics like resources gathered/spent, units produced/killed)

The game must implement the following game mechanics and systems:

- Movement and path generation system
- Resource gathering system
- Building and repairing system
- Unit production system
- Primitive combat system (melee combat only with single attack and armor type)
- Simple cheating AI (having information about the entire game state and constantly attacking you if possible)
- Victory and defeat conditions
- Game state saving & loading

## Non-Functional Requirements & Engine Features

- Event-based subsystem for handling user input
- Simple graphics user interface subsystem (windows, labels, picture boxes & labels)
- Game state management (transitions from main menu to gameplay, to 'About the Program' screen, etc.)
- Viewport-based rendering & 2D camera
- Controllers for passing user input to gameplay-level services
- Order management system
- In-memory repositories for storing the game state
