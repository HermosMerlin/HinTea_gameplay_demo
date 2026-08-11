# HinTea Gameplay Demo Agent Guide

## Project

- This is a Unity 2D simulation-management gameplay validation demo.
- Unity editor version: `6000.3.10f1`.
- The project uses Universal Render Pipeline 2D.
- The project uses the Unity Input System package, version `1.18.0`.
- The project contains the existing input asset `Assets/InputSystem_Actions.inputactions`.
- The input asset contains a `Player/Move` action and a `Player/Interact` action. Interact is bound to the keyboard `E` key and uses a Press interaction for one trigger per key press.
- The user created `Assets/Scenes/TeaShop.unity` as the working scene during the tutorial.
- The `TeaShop` scene uses the Lit 2D (URP) template, which provides an orthographic camera and a Global Light 2D.
- At the time this guide was created, the worktree already contained uncommitted Unity changes, including changes to ProjectSettings, a new `TeaShop` scene, and deletion of `SampleScene`. Treat those changes as user work. Do not revert, overwrite, or clean them up without explicit permission.

## User And Teaching Context

- This repository is strictly a learning project. Do not treat it as a production task or optimize for completing gameplay autonomously.
- The user has C++ programming experience and has made simple games such as Pac-Man.
- The user has not used Unity or another game engine before.
- Explain Unity concepts by relating them to familiar programming concepts when useful, but do not assume knowledge of Unity's editor or lifecycle.
- The default communication language is Chinese.
- The user wants oral, step-by-step guidance rather than unsolicited file edits.
- Do not modify project files, scenes, scripts, settings, or assets unless the user explicitly instructs the AI to make that specific change.
- Never autonomously write, finish, repair, refactor, or extend task code. If the user reports that a tutorial step is complete, explain or verify it first; do not fill in missing implementation unless explicitly instructed.
- When the user asks to be taught, provide the next small concept and a verification criterion instead of implementing it on the user's behalf.
- The current request explicitly authorized changes to this `agents.md` file, the root `.gitignore`, and the two ignored planning documents described below.

## Explanation Requirements

- Whenever introducing a new menu item, component, Inspector field, asset, setting, or numeric value, explain why it is needed and why that value or option is appropriate.
- Do not give unexplained click sequences.
- Introduce one small concept at a time and provide a clear verification or acceptance criterion before moving on.
- Prefer the smallest working implementation over framework-heavy or production-scale architecture.
- Call out important Unity-specific differences from a self-written C++ game loop, including GameObject and Component composition, MonoBehaviour lifecycle methods, Update versus FixedUpdate, Inspector references, and Play Mode persistence.

## Tutorial Scope

- The initial practice project is a one-screen tea stall.
- The target gameplay loop is: move, approach a station, interact, consume resources, prepare tea, serve customers, receive money, and start the next day.
- Use placeholder colored 2D shapes before introducing art assets.
- Teach these fundamentals through the practice: Scene, GameObject, Component, Transform, SpriteRenderer, Rigidbody2D, Collider2D, Trigger, MonoBehaviour, Input System, Canvas, TextMeshPro, Button, Prefab, Instantiate, and simple game state.
- Do not introduce Tilemap, Animator, pathfinding, ScriptableObject data architecture, inventory frameworks, Addressables, networking, or complex save systems until the core loop works or the user explicitly requests them.
- Keep game rules centralized in a small manager initially. Avoid introducing singletons, event buses, or elaborate abstractions before their need is demonstrated.

## Workflow And Safety

- Inspect the relevant files and current Git status before making changes.
- Preserve user changes, uncommitted work, and generated Unity project state.
- Never use destructive Git commands such as `git reset --hard` or `git checkout --` unless explicitly requested.
- Before reporting a file change as complete, inspect the diff and verify that only intended files changed.
- Do not commit, amend, push, or create a pull request unless explicitly requested.

## Current Task Tracking

- Maintain both the root `plan.md` outline and the root `CURRENT_TASK.md` document throughout the tutorial.
- `plan.md` records the stable learning outline and major project stages; update it when the overall learning route changes.
- Maintain the root `CURRENT_TASK.md` document whenever moving to the next tutorial step.
- `CURRENT_TASK.md` must contain only the current goal and the required steps, with no other content.
- Before answering any question related to the current task, read `CURRENT_TASK.md` and use its goal and steps as the source of truth. Read `plan.md` when discussing the overall learning route.
- Update `CURRENT_TASK.md` when the current goal is completed and the next goal is selected.
- Keep both `plan.md` and `CURRENT_TASK.md` ignored by Git; do not commit either file.
