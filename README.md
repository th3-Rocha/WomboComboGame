# WomboComboGame

A small **Unity (C#)** project made mainly for **studying and practicing C# gameplay programming** (MonoBehaviours, physics, UI, coroutines, layers/masks, etc.).

This repo was originally created for **GameLab 24.1 (Seconds) Jam** with the theme **"Reaching Higher"**.

---

## Goal / What this project is for

- Learn and practice **C# scripting in Unity**
- Read and modify simple gameplay systems:
  - player movement logic
  - cooldowns
  - UI counters
  - physics collisions
  - spawning / instantiation
  - camera control
  - simple enemy behaviour + ragdoll

---

## Main mechanics (from the scripts)

### 1) Start / End flow + countdown timer
- The game starts in a **Start Menu** and only begins when the player **right-clicks**.
- When starting, it enables:
  - in-game HUD
  - gameplay prefabs
  - `PlayerController` + `PlayerComboController`
- A **24.1 seconds** countdown runs using a coroutine and updates a `TextMeshProUGUI` timer.
- When the timer reaches 0, the game switches to the **End Menu** state.

(See: `GameStart.cs`, `GameEnd.cs`)

---

### 2) Teleport movement (right click)
- Player movement is based on **teleporting to the mouse position** (raycast from camera to the world).
- Teleport has a **cooldown**.
- Teleport triggers:
  - animation (`PlayerTeleport`)
  - audio
  - camera shake
  - a teleport VFX (`tpEffect`)
- There is also an optional **teleport preview overlay** (`tpOverlay3d`) when enabled.
- If the player is near an enemy, teleport can also rotate the player towards that enemy.

(See: `PlayerController.cs`)

---

### 3) Attack / combo aiming (left click “attack mode”)
- Holding **left click**:
  - stops player rigidbody velocity
  - enables an attack panel (`AtackPanel`)
  - makes arms material transparent (alpha changes)
  - sets the player into “attacking” animation/state
- While attacking, the character rotates:
  - towards the **nearest enemy** inside a detection radius, or
  - towards the **mouse cursor hit point** if no enemy is close.

(See: `PlayerComboController.cs`)

---

### 4) Arm spawning + hit detection + cooldown on hit
- An `ATKControl` spawns an **arm prefab** at random points on an interval.
- The spawned arm gets a `CollisionDetector` that reports hits when it collides with an enemy layer.
- When hitting an enemy:
  - spawns a hit effect prefab
  - increments the hit counter HUD
  - triggers camera shake
  - applies a “hit cooldown” (so the hit feedback is gated)

(See: `ATKControl.cs`, `CollisionDetector.cs`, `ArmFoward.cs`, `HitHudCounter.cs`)

---

### 5) Score system (based on hit count)
- Score UI simply mirrors the **hit counter** value.

(See: `GameScore.cs`)

---

### 6) Enemies + ragdoll death
There are enemy behaviours like:
- **Walking enemy** that moves forward with physics forces, limits max speed, picks random walk animation + random material, and can die on collisions with certain layers.
- Ragdoll toggling by disabling animator and enabling child rigidbodies/collisions.

(See: `EnemyBehaviourWalking.cs`, `EnemyBehaviour.cs`, `RagDollBehaviour.cs`)

---

### 7) Camera stage / layer system (scroll wheel progression)
- Mouse wheel changes a `Stage` value.
- Stage selects a camera position from a list, and if it exceeds the list it starts moving the camera pivot up by “levels”.

(See: `LevelAndLayer.cs`, `CameraChangePosition.cs`, `CamLayerControl.cs`)

---

## Controls (based on scripts)

- **Right Mouse Button**
  - Start the game (when in the start menu)
  - Teleport (during gameplay)

- **Left Mouse Button (hold)**
  - Attack mode / aim attacks (rotate towards enemy or cursor)

- **Mouse Wheel**
  - Change “Stage” / camera level

---

## Tech / Notes

- Built in **Unity** using **C# scripts** (`Assets/Scripts`)
- Uses **TextMeshPro** for UI text
- Uses physics (Rigidbody, Colliders) heavily for movement + collision gameplay

---

## Repo structure (important folders)

- `Assets/Scripts/` – gameplay code (teleport, attack, enemies, camera, UI)

---

## Disclaimer

This project is meant for **learning and experimentation**. Code is intentionally simple and jam-style.
