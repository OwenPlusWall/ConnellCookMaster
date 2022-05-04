# ConnellCookMaster
 
Within the given time frame, I have put forth my best effort to create the project, following the design instructions as closely as possible.

There are a few features that I did not have time to add, those being:

1. Powerups. Powerups were something I considered very early on, even adding a class to the player for it. This class is currently empty, however.
If I were to add Powerups to the project, I would create a list of coordinates where they can spawn, assign a Player to the Powerup so that only the
corresponding Player could pick it up, and then, based on the function of the Powerup, affect the player.
For the Score and Time powerups, they would simply add to the Player's score and time respectively, and then disappear.
For the Speed Up Powerup, I would have the Powerup attach itself to the Player, and then increase their movement and chopping speed by a specified amount. A timer on the Powerup would tick down, and when it expired, it would return the Player's speed back to normal, remove itself, and then destroy itself.
I left room on the HUD to indicate that a Speedup Powerup was active for a player, and even put the icon for it in the scene, but I did not get to adding the feature in time.

2. A HUD to display the progress of a Player chopping vegetables. My process for this would be similar to how I handled the Customer's meter displaying their waiting time, only it would appear and then disappear during and after the Player has chopped a vegetable.

3. An indicator for what tile the Player could interact with. This would be as simple as having a "highlight" object appear/disappear if the Player is in range to interact with a tile, and have the indicator match the position of the tile, like I did with the Vegetables and Salads when one is placed onto a Table or CuttingBoard.
I would need to make sure that the sprite layering doesn't conflict, so some of the layer ordering may have to change, but given the small size of the project, that would not take more than a few minutes to change.

3. A scoreboard. This would be a static List that would keep track of the top scores between play sessions, and I would indicate which player got the high score (Blue or Red).
If I wanted to, I could also make the score save to and load from a file, so that the program can maintain the scores even after closing it.
