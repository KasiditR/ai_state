# AI State Management in Unity

This Unity project demonstrates a flexible AI state management system that utilizes Unity's NavMesh for navigation. The system is designed to handle various AI behaviors through state transitions, making it adaptable to different gameplay scenarios.

## Overview

The AI system is built around a state machine pattern, where each AI agent has a `StateController` that manages its current state and transitions between states. The states define the behavior of the AI agent, such as moving, attacking, or idling.

### Key Components

1. **StateController:**
   - An abstract base class that serves as the core of the AI system.
   - Manages the NavMeshAgent component, which handles pathfinding and movement.
   - Contains a dictionary of states and manages the transition between these states.
   - Tracks the AI's target, controls movement, and facilitates state transitions based on the game's logic.

2. **BaseState:**
   - An abstract class that defines the structure for AI states.
   - Each state inherits from this base class and implements specific behaviors.
   - Contains methods like `OnEnter`, `OnUpdate`, and `OnExit` to manage state-specific logic during the state lifecycle.

3. **Specific States:**
   - Custom states derived from `BaseState`, such as `AttackState`, `DieState`, `IdleState`, and `MovingState`.
   - Each state encapsulates specific behaviors, such as how the AI should act when attacking, when it has died, or when it is idle.
   - States also determine what the next state should be, based on game conditions or AI decisions.

### Example: Monster State Management

The following example demonstrates a practical implementation of the AI state management system for a monster:

#### MonsterStateExample

- **Description:** Manages the monster's states and behaviors, such as moving, attacking, and dying.
- **Key Methods:**
  - `Initialize`: Sets up the states and initializes the AI.
  - `Attack`: Defines the attack behavior.
  - `Die`: Defines the death behavior and deactivates the monster.

#### MonsterMovingStateExample

- **Description:** Handles the behavior when the monster is moving.
- **Key Methods:**
  - `OnEnter`: Initializes the state.
  - `OnUpdate`: Moves the monster towards the target if within attack range.
  - `GetNextState`: Transitions to the attack state if the monster is within range.

#### MonsterIdleStateExample

- **Description:** Handles the behavior when the monster is idle.
- **Key Methods:**
  - `OnEnter`: Initializes the state.
  - `GetNextState`: Transitions to the attack state if the target is within range; otherwise, transitions to the moving state.

#### MonsterDieStateExample

- **Description:** Handles the behavior when the monster dies.
- **Key Methods:**
  - `OnEnter`: Initializes the state and deactivates the monster.
  - `GetNextState`: Currently returns the base state.

#### MonsterAttackStateExample

- **Description:** Handles the behavior when the monster is attacking.
- **Key Methods:**
  - `OnEnter`: Initializes the state.
  - `OnUpdate`: Executes the attack action.
  - `GetNextState`: Transitions to the moving state if the target moves out of range.

### How It Works

- **Initialization:** The `StateController` initializes the states and sets the initial state when the game starts or when the AI agent is spawned.
- **State Transition:** The `StateController` checks for conditions to change states, such as proximity to a target or the health status of the AI. If a state transition is needed, the controller handles the transition process, ensuring smooth changes in behavior.
- **Behavior Execution:** Each state defines how the AI should behave while in that state, using the NavMeshAgent for movement and other Unity components for actions.

### Customization

Developers can extend the system by creating new states that inherit from `BaseState`. This allows for adding new behaviors and expanding the AI capabilities. The system is modular, making it easy to adjust or replace states without impacting the entire system.

## Usage

To implement this AI state management system in a Unity project:

1. **Add the Scripts:** Include the necessary scripts for `StateController`, `BaseState`, and any specific states in the project.
2. **Attach to GameObjects:** Attach the `StateController` to AI GameObjects in the scene. Ensure the NavMeshAgent component is also attached.
3. **Configure States:** Define and initialize states in the `StateController`'s implementation, linking them to the appropriate behaviors.
4. **Run the Scene:** Test the AI behaviors by running the scene, observing how the AI agents interact with their environment and react to player actions.

This AI system is designed to be both robust and flexible, making it suitable for a wide range of game genres and scenarios.
