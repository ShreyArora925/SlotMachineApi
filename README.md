# Slot Machine API

A .NET 10 Web API with MongoDB that simulates a slot machine backend.

## Prerequisites

* .NET 10 SDK
* MongoDB Atlas account (free tier is fine)
* .NET 10 SDK (download from https://dotnet.microsoft.com/download)

## Setup

1. Clone the repo
2. Go to MongoDB Atlas (https://mongodb.com/cloud/atlas), create a free cluster
3. Create a database user and allow all IPs (0.0.0.0/0) under Network Access
4. Get your connection string from Connect → Drivers
5. Update SlotMachineApi.API/appsettings.json with your connection string
6. Run the API: dotnet run --project SlotMachineApi.API
7. Check the terminal for your port number, then seed the database by calling: POST http://localhost:{port}/api/game/seed
8. Open http://localhost:{port}/scalar/v1 in your browser to test the API interactively

This creates player1 with $1000 balance and a 5x3 matrix config.

## Endpoints

POST /api/game/spin
Request:  { "playerId": "player1", "betAmount": 10 }
Response: { "resultMatrix": \[\[3,3,3,4,5],\[2,3,2,3,3],\[1,2,3,3,3]], "winAmount": 270, "currentBalance": 360 }

POST /api/game/balance
Request:  { "playerId": "player1", "amount": 100 }
Response: { "currentBalance": 460 }

POST /api/game/seed
Initializes the database with a default player and game config.

## How Winning Works

* Matrix is randomly generated each spin (0-9 per cell)
* Two win line types: straight rows and zigzag diagonals
* Win = consecutive identical digits from position 0, minimum 3 in a row, multiplied by bet
* Example: 3,3,3,4,5 = win of 9 x bet

## Reconfiguration

Matrix size and win lines are stored in MongoDB. Change them directly in the gameconfig collection, no restart needed.

## Concurrency

Spin and UpdateBalance can be called simultaneously. Balance updates use atomic MongoDB operations to prevent race conditions.

## Running Tests

dotnet test

## Assumptions

* Multiple players supported via playerId in each request
* New players are created automatically on first balance update
* All straight rows and zigzag diagonals are active by default

