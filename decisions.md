# Battleship Recruitment - Documentation

## Frontend (React)

The frontend of the application features a simple user interface that displays the game boards for both players and the current game state.

## Backend (ASP.NET Core)

The backend of the application consists of an ASP.NET Core API server that handles the game logic and communication with the frontend.

### Endpoints
1. **Move Endpoint**
   - Endpoint: `/api/ships/move`
   - Method: POST
   - Description: Executes a player's move and returns the result of the move along with the updated board state.
   - Response: Information about the current board layout for both players, identifies the player responsible for the current move, and reports whether a victory for either player has already occurred.
   
2. **Reset Endpoint**
   - Endpoint: `/api/ships/reset`
   - Method: POST
   - Description: Resets the game state to the initial state (boards, ships, etc.).
   - Response: Confirmation of the reset.


### Board Requirements
- The board defaults to dimensions 8x8.
- The default number of ships is 5.

### Ship Sizes
Ship sizes are configurable and can vary depending on the game settings. Each ship has a different size, starting from 1 and ending at the value specified in the Default Number of Ships setting.

### Board and Game Configuration
All values related to game configuration like board and ship sizes can be modified in the `appsettings.json` file.

### Assumptions:

- Displaying game boards for both players.
- Real-time updating of boards.
- Displaying the current player's turn.
- Communicating with the backend to receive game state updates.
- Displaying the winner when the game ends.
- Possibility to reset when the game ends.
- The game state is retained even when the browser window is closed and the game is coontinued.
- Application configuration via appsettings.json
- Simple clear user interaface.
- Application frontend written in BEM methodology.
- Using the SCSS preprocessor for CSS.
- Using the principles like KISS or DRY.


## Requirements
- Visual Studio 2022: The application requires Visual Studio 2022 due to being developed on .NET 6.
- Visual Studio Code: To run the frontend, you need Visual Studio Code.

## Screenshots
Screenshots of appliacation are in folder screenshots in repo.
