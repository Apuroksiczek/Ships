# Battleship Game - Documentation

## Introduction
The Battleship Game is a multiplayer application where two players compete by placing their ships on boards and attempting to sink each other's ships. The application consists of two parts: a frontend built with React and a backend built with ASP.NET Core.

## Frontend (React)

The frontend of the application features a simple user interface that displays the game boards for both players and the current game state.

### Board Requirements
- The board defaults to dimensions 8x8.
- The default number of ships is 5.

### Ship Sizes
Ship sizes are configurable and can vary depending on the game settings. Each ship has a different size, starting from 1 and ending at the value specified in the Default Number of Ships setting.

### Board and Game Configuration
All values related to the board, game configuration, and ship sizes can be modified in the `ShipsConstants.js` file.

### Functionality
- Displaying game boards for both players.
- Real-time updating of boards during gameplay.
- Interaction with the player by placing their ships on the board.
- Displaying the current player's turn during gameplay.
- Making moves and communicating with the backend to receive game state updates.
- Displaying the winner when the game ends.

## Backend (ASP.NET Core)

The backend of the application consists of an ASP.NET Core API server that handles the game logic and communication with the frontend.

### Endpoints
1. **Move Endpoint**
   - Endpoint: `/api/ships/move`
   - Method: POST
   - Description: Executes a player's move and returns the result of the move along with the updated board state.
   - Input: Move information (e.g., target coordinates).
   - Response: Move status (hit, miss, sunk) and the updated board state.
   
2. **Reset Endpoint**
   - Endpoint: `/api/ships/reset`
   - Method: POST
   - Description: Resets the game state to the initial state (boards, ships, etc.).
   - Response: Confirmation of the reset.

## Requirements
- Visual Studio 2022: The application requires Visual Studio 2022 due to being developed on .NET 6.
- Visual Studio Code: To run the frontend, you need Visual Studio Code.

## Conclusion
The Battleship Game application comprises a React frontend and an ASP.NET Core backend. Players compete by placing ships on boards and making moves, and the application allows for real-time board updates and player communication through the backend API. The application also displays the current player's turn during gameplay and shows the winner when the game concludes.
