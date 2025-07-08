# AngryBirds-NEA

**Angry Birds - NEA**, a recreation of the classic and beloved mobile game where players use birds to destroy structures and eliminate pigs for my Non Exam Assesment for Computer Science. This version brings the nostalgic fun of the original Angry Birds game to your PC, with the same core mechanics, physics, and cartoonish charm.

## Table of Contents

* [Introduction](#introduction)
* [Gameplay](#gameplay)
* [Features](#features)
* [Installation](#installation)
* [System Requirements](#system-requirements)
* [Test Data](#test-data)
* [Future Updates](#future-updates)
* [License](#license)

---

## Introduction

Angry Birds is a puzzle-solving game where the player uses a slingshot to launch birds at various structures that protect pigs. The goal is to knock down the structures, eliminate all the pigs, and cause as much destruction as possible to score points.

This version offers the original mechanics, bringing back the fun with birds, pigs, and physics-based puzzles. Players will control a slingshot, aim, and launch birds to hit the pigs. The game is divided into levels, with each level having unique layouts and obstacles. The game progresses as players eliminate pigs and complete challenges.

---

## Gameplay

### Objective

* **Launch Birds:** Aim and launch birds at structures to eliminate pigs.
* **Complete Levels:** Eliminate all pigs to move to the next level.
* **Scoring:** Score is based on the amount of destruction caused and pigs eliminated. The fewer birds used, the higher your score.

### Core Mechanics

* **Slingshot:** The primary tool for launching birds. Adjust the power and trajectory by pulling the slingshot back.
* **Birds:** Various birds with different abilities (starting with the basic Red bird).
* **Pigs:** The main target in each level, which pops when hit with a sufficient force.
* **Blocks:** Structures made of breakable and unbreakable blocks that protect the pigs.

---

## Features

### Bare Essentials

1. **Slingshot Mechanics:**

   * Launch birds with adjustable force.
   * Animation when the slingshot is pulled back.

2. **Birds:**

   * Birds interact with blocks and pigs based on physics.
   * The number of birds decreases as they are used.

3. **Blocks:**

   * Blocks fall based on physics, creating dynamic and engaging gameplay.

4. **Pigs:**

   * Pigs pop when hit with enough force.

5. **Background:**

   * Static background that supports the gameplay.

6. **Game Progression:**

   * Win or lose based on whether all pigs are eliminated.

---

### Extra Features (Future Updates)

1. **Score System:** Track the player’s performance and scores across different levels.
2. **High Score System:** Record and display the highest scores.
3. **Levels:** Multiple stages with increasing difficulty.
4. **Sound Effects:** Enjoy immersive sound effects, such as bird launches and blocks breaking.
5. **Splash Screen & Game Over/Win Screens:** Simple screens that display at the start and end of each game.

---

## Installation

### Prerequisites

* **Operating System:** Windows 7/10/11, macOS Mojave 10.14+, Linux Ubuntu 20.04
* **Unity** (for development): You can download Unity from [Unity's official website](https://unity.com/).

### Steps:

1. **Download the Game:**

   * \[Download Link](https://github.com/king525dev/AngryBirds-NEA/releases/tag/v0.0.1)

2. **Extract Files:**

   * Unzip the downloaded file to your preferred location.

3. **Run the Game:**

   * Double-click the game executable to start playing.

---

## System Requirements

### Hardware:

* **Processor:** 2GHz or higher
* **Memory:** 1GB RAM or more
* **Graphics:** 12MB+ (basic graphics required)
* **Hard Drive:** 250MB of free space
* **Mouse:** Required for controlling the slingshot
* **Monitor:** Required to view the game
* **Speakers:** To hear sound effects

### Software:

* **Operating System:** Windows 7/10/11, macOS Mojave 10.14+, Linux Ubuntu 20.04
* **Unity:** Game is developed using Unity, compatible with the listed operating systems.

---

## Test Data

### Slingshot

| Test                | Expected Output                                              | Explanation                           |
| ------------------- | ------------------------------------------------------------ | ------------------------------------- |
| Slingshot animation | The slingshot pulls back and follows the cursor when clicked | Ensures realistic slingshot behavior  |
| Slingshot launch    | The bird is flung out with proportional force                | Ensures consistent gameplay mechanics |

### Birds

| Test                    | Expected Output                      | Explanation                           |
| ----------------------- | ------------------------------------ | ------------------------------------- |
| Interaction with blocks | Bird alters block based on its force | Ensures proper physics interaction    |
| Interaction with pigs   | Pig pops when hit with enough force  | Confirms proper pig elimination logic |
| Bird physics            | Bird follows a parabolic trajectory  | Ensures realistic flight behavior     |

### Blocks & Pigs

| Test            | Expected Output                         | Explanation                               |
| --------------- | --------------------------------------- | ----------------------------------------- |
| Block physics   | Blocks fall when struck or unsupported  | Verifies dynamic gameplay environment     |
| Pig interaction | Pig pops when hit with sufficient force | Ensures correct pig elimination mechanics |

---

## Future Updates

While the current version focuses on delivering the basic gameplay mechanics, future updates will introduce additional features:

1. **Levels:** More complex stages with varied challenges and environments.
2. **Score System:** Track performance and encourage replayability.
3. **Multiplayer (if time allows):** Implement a feature for players to compete or cooperate.
4. **Leaderboard:** Although online scoreboards were considered, they have been delayed due to technical challenges.

---

## License

This project is an open-source recreation of the original Angry Birds game, developed for educational purposes. All original trademarks and copyrights belong to **Rovio Entertainment**. This version is a fan-made project and is not affiliated with Rovio Entertainment.