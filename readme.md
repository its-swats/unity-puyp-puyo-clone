Puyo Puyo Clone created in Unity

Compiled Web Version available at http://www.shawnwatson.info/puyopuyo/

This should be all of the files needed to create and compile a Puyo Puyo clone in Unity. Most of the meat of the project is in the Scripts folder. 

GameBoard.cs - all logic for the game board that stores unit locations

PlayerController.cs - interface for moving muyo left/right/down and rotating

Puyo.cs - code for the connected blocks of 2 puyo units

PuyoUnit.cs - logic relating to the individual halves of the main Puyo block

PuyoSpawner - main logic script for spawning new Puyos, deleting groups of 4, and causing leftover puyos to fall

GameOver.cs - logic for the game over screen and restarting

GameStart.cs - logic for the start screen
