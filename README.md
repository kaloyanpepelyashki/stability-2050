# Stability 2050
Semester project: Development of Software Programs (E25)

University of Southern Denmark  
Faculty of Engineering (TEK)  
Sønderborg Campus

**Course Code:** SI1S-PRO  
**Program:** BSc. Software Engineering  
**Semester:** One / First Year
**Group number:** 15

### Author(s)
Alexander Paasch Christensen – alchr25@student.sdu.dk  
Maxim Dnestreanschii – madne25@student.sdu.dk  
Daniel Falat – dafal25@student.sdu.dk  
Kaloyan Lyudmilov Pepelyashki – kapep25@student.sdu.dk

### Supervisor
Alexandra Moraru – almor24@student.sdu.dk

## Project Overview
Stability 2050 is a text based adventure game created as a semester project at SDU.
The game is developed upon the World of Zuul framework and adds own architecture, 
JSON data and corruption mechanics based on CPI (Corruption Perception Index).

The goal is to introduce the topic of corruption in a way that is easy to understand and teach people about it.
The game is based on SDG 16 – Peace, Justice and Strong Institutions
The player travels across several regions and answers tricky questions. His answers influence the CPI level.


## Features
- navigation between regions using commands
- parsing JSON files (names, descriptions, CPI, questions, answers)
- calculation of regional and global CPI
- simple console line interface
- easy to expand and maintain because of the modular approach

## Architecture Overview
Project is divided into 4 layers:

- **Presentation** – CLI, information printing, input validation
- **Application** – coordination of the game logic, commands
- **Domain** – main game mechanics (Player, Region, World, CpiTracker, TurnCounter)
- **Infrastructure** – loading JSON files and transformation to DTO

You can find more information in the project report.


