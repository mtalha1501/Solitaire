# Solitaire Game (DSA Mid Project)

## Overview
Solitaire is a classic card game, developed in C# using WPF Forms. The game offers a rich user experience where the player can move cards between tableau piles, foundation piles, and the stock/waste piles. The project incorporates essential concepts from **Data Structures and Algorithms (DSA)** such as **Stacks**, **Queues**, and **Linked Lists** to manage and manipulate cards efficiently.

### Key Features:
- **Double-Click to Move Cards**: 
  - Players can double-click on a card to automatically move it to the first valid foundation pile.

  - If the card cannot be moved to a foundation, the game checks if it can be placed in a tableau pile, strictly to the game’s rules (descending order and alternating colors).

  - This adds a layer of convenience, making gameplay faster and more efficient without needing to manually drag cards.
 
  - Card Cannot move from Foundation To Tableau Piles.
  
- **Logic of Game**:

  - **Empty Tableau Piles**: When a tableau pile is empty, only a King can be moved to that pile.

  - **Card Flipping**: Automatically flips cards in tableau piles when the last face-up card is moved, keeping the game flow intact.
  
- **Data Structures**:

  - **Stacks**: Used for tableau, stock, and waste piles, managing cards with Last-In-First-Out (LIFO) logic.

  - **Queues**: Used for actions like drawing cards from the stock pile and managing possible moves.

  - **Linked Lists**: Employed for managing tableau piles, allowing efficient additions, removal, and transmission of cards among tableau piles.

## Screenshots
Include gameplay screenshots to showcase different aspects of your game:

1. **Initial Setup**: Screenshot showing the starting position of the game.
  
## Installation and Setup

1. **Prerequisites**:

   - Visual Studio with .NET support.

   - Windows OS.

2. **Clone the Repository**:
   ```bash
   git clone [https://gitlab.com/mtalha1501/csc200m24pid169/-/tree/master?ref_type=heads]
   cd Solitaire
   ```

3. **Run the Application**:
   - Open the project in Visual Studio.
   - Press `F5` to build and run the game.

## Usage
- **Game Controls**:

  - **Double-Click**: Double-click on a card to move it directly to the first valid foundation pile, or if it's not valid for the foundation, it will check the tableau for a possible move.

  - **Stock Pile**: Click to draw card button from the stock pile. If the stock is empty, the "Reset" button becomes active.

- **Win Condition**:
  - The game is won when all foundation piles cards count is 13 mean each Foundation Full.

## Future Enhancements

- Add undo/redo functionality.
- Add Drag and Drop Functionality for easy play
- Add Hint System 
- Implement a scoring system or timer to increase game challenge.

- Include sound effects for card movements and interactions.

```markdown
![Start Game]("C:\Users\PMLS\Desktop\Current Study\Mid Proj\Solitaire\Images\StartInitial.png")

```
