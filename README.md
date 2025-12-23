# Stability 2050

Semester project: Development of Software Programs (E25)

University of Southern Denmark  
Faculty of Engineering (TEK)  
Sønderborg Campus  

Course Code: SI1S-PRO  
Program: BSc. Software Engineering  
Semester: One / First Year  
Group number: 15  

---

## Authors

Alexander Paasch Christensen – alchr25@student.sdu.dk  
Maxim Dnestreanschii – madne25@student.sdu.dk  
Daniel Falat – dafal25@student.sdu.dk  
Kaloyan Lyudmilov Pepelyashki – kapep25@student.sdu.dk  

## Supervisor

Alexandra Moraru – almor24@student.sdu.dk  

---

## Project Overview

Stability 2050 is a text based adventure game created as a semester project at SDU.  
The game is developed upon the World of Zuul framework and adds own architecture, JSON data and corruption mechanics based on CPI (Corruption Perception Index).

The goal is to introduce the topic of corruption in a way that is easy to understand and teach people about it.  
The game is based on SDG 16 – Peace, Justice and Strong Institutions.

The player travels across several regions and answers tricky questions. Their answers influence the CPI level.

---

## Game Mechanics

This chapter describes main game mechanics of the game Stability 2050. Game mechanics describe how the player interacts with the game world and how their decisions affect the game and its outcome. The design of game mechanics is intentionally simple, clear and suitable for a text-based environment, with emphasis on the educational aspect of the game.

### Base Game Concept

Stability 2050 is a turn-based game. The player takes the role of a decision maker who travels around the world and faces corruption, weak institutions and ethical dilemmas. The main objective is to maintain global stability by making decisions that positively influence the Corruption Perception Index (CPI).

The game progresses as the player takes actions, with each action affecting time, game state and the final result.

### Turn Management and Time Flow

The game uses a turn management system to simulate time flow. Each player action represents one turn (one year). The game starts in the year 2025 and ends in 2050, giving the player a maximum of 25 turns.

This system creates pressure on the player to make effective decisions, as every action has a cost in time.

### Corruption Perception Index (CPI)

CPI is the key game mechanic. Each region has its own local CPI value, while the global CPI represents the overall state of the world.

Player decisions directly influence CPI values. Ethical and transparent choices increase CPI, while unethical decisions decrease it. This illustrates how small local decisions can have long-term global consequences.

### Crisis and Last Chance Mechanism

If the global CPI falls below a critical threshold, the game enters a crisis state. In this phase, the player is given a limited number of final turns to recover the CPI.

This “last chance” mechanic can be activated only once per game. If the player fails to recover the CPI, the game ends and the player loses.

### Quiz Mechanism

Quiz questions represent ethical and corruption-related dilemmas. Each region contains its own set of questions thematically connected to corruption, transparency and governance.

Each answer has a predefined effect on CPI. All quiz questions are based on our research and are designed to combine educational content with meaningful game consequences.

### Planned Item Mechanics

A concept of game items was designed to expand gameplay and provide additional strategic options. These mechanics were partially developed but not fully implemented due to time constraints.

Proposed items were divided into:
- Effect items – items with direct impact on CPI or number of turns
- Token items – one-time items intended to affect quiz answers (for example removing incorrect options)

---

## Architecture Overview

The project is built on an object-oriented architecture and applies concepts from Domain-Driven Design. The source code is divided into four layers.

### Presentation Layer
Handles user interaction through the console interface. It processes player input and displays game information without containing game logic.

### Application Layer
Coordinates the game flow and manages communication between presentation and domain layers. It controls the game loop, quiz sessions and game state updates.

### Domain Layer
Represents the core of the game. It contains the main game concepts, mechanics and rules, including entities such as Player, Region, World, Question and Answer.

### Infrastructure Layer
Handles technical concerns such as file operations and data parsing. Game data is stored in JSON files and transformed into DTOs used by the application.

---

## Data Management

Game data is stored in external JSON files and loaded at application startup.

The data flow is one-directional:

External data → Infrastructure → DTO → Application → Domain

This approach allows easy extension of the game by adding new regions or questions without modifying the game logic.

---

## Features

- Navigation between regions using commands
  
- Parsing JSON files (regions, CPI values, questions and answers)
  
- Calculation of regional and global CPI
  
- Turn-based progression
  
- Console Line Interface (CLI)
  
- Modular and maintainable layered architecture

---

## How to Run the Project

### Prerequisites
- .NET 9 SDK (version compatible with the project)

### Run Instructions
1. Clone the repository from GitHub
2. Open the project folder
3. Build the project using:
```bash
dotnet build
dotnet run
