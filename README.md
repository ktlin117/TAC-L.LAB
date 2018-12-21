# TAC-L.LAB

## Game Description
Follow the tragic story of a cat named Ball Cat, who was abandoned at a young age by lab scientists as a smol kitty. His sorrow calls for action, and you the player, will help Ball Cat enact his revenge. Traverse through levels full of treacherous terrain and difficult obstacles to reach the lab. You will roll around in a ball and interact with many different objects to achieve this goal. There are explosive barrels, flamethrowers, and endless falls to death, so be careful and keep rolling!

## Playing the Game
You will have to make sure you have Unity installed. Navigate to `TAC-L.LAB\Assets\Scenes\Title.unity`. This will open the Unity editor. From there you can play from the editor by pressing the play button in the top. You may also make modifications to game values and scripts from this window.

For a fuller experience, go to `File -> Build Settings`. Make sure "PC, Mac, & Linux Standalone" is selected as the platform, then press build and run. Build the file to a folder, and from there, you may play the game at the best graphics settings and have an enhanced experience. Doing this will also create an executable, which can be used to easily and quickly open up the game.

## Controls
* Arrow/WASD keys to move Ball Cat.
* Move the mouse/cursor to move the third person camera.
* Scroll the mouse wheel to change camera zoom.
* Space bar to interact with objects (e.g. puller, launcher, etc.) 
* Z/X to slow/speed up time respectivelty.
* P to pause.
* Q to exit back to the title screen.
* R to restart the level (No life penalty).
* E to explode and restart the level (The true way of restarting).

## Video Demo
[YouTube Video](https://www.youtube.com/watch?v=PPTq59U9-YA)

## Full Details
*This section contains spoilers and specific details regarding the game, so skip through if you're uninterested.*

In this game, you will take control of a cat in a ball to traverse levels until you reach the lab to enact revenge on those who abandoned you. You start off with the title screen. There are 5 buttons: Play, Instructions, Options, Credits, and Quit. All of them are self-explanatory, though a few things should be noted. You can give yourself lives in the options menu to avoid game overs if the game is too hard. Credits is also a bit vague, so look in this document for full credits.

When you start the game, a UI will appear. In the top left, lives are kept track of. Dying deducts a life, and losing a life while at 0 will cause you to game over, losing all your progress. HP starts at 100, and is displayed in the top right corner. You will die if this reaches 0. A timer in the top middle will display how much time has elapsed since starting the level. In the bottom, items will be kept track of. These items will help you with completing the level.

There are numerous keys to success in this game. Launchers are donut shaped objects, that will shoot you up if you press space while in them. Keys will open doors if they’re locked, but the key must be the correct one for the door if they’re locked. Levels are completed by entering a green pipe. You will be rewarded a life for doing so. Pullers are spherical objects in the sky that glow when you are close enough. Pressing space will cause them to pull you towards you, reaching otherwise unreachable areas. 	When you reach the end, you get to destroy the lab with a detonator! You will have completed the game by then, and the game will tell you your total time.

Unfortunately, there are also a lot of dangers. Rolling off the edge will cause you to die, and have to restart from the beginning of the level. Explosive barrels deduct 40 hp and knock you back. Flamethrowers spew out fire that does a lot of damage if you stay in it. Even after leaving, Ball Cat will become enflamed, taking small tick damage afterwards.

Many features helped with creating a good atmosphere. Motion blur makes changing the camera turns appear more fluid. Makes flames appear to be very hot and fog really creates a sense of distance. It was really challenging getting the physics just how I wanted, as well as getting objects and sounds to interact correctly. Colliders were also a bit of a pain with many components I was unfamiliar with, and getting variables to successfully pass along classes and scenes was also challenging. I really enjoyed level creation and adding new features as I developed the process.

Overall, I had a ton of fun with this project! I will probably continue making improvements going into the future, and I can definitely say this is something I’m proud of.

## Credits
* All songs are from Waterflame. In order of appearance, they are called “The Show”, “Nibbo Flower Tower”, “Jumper”, and “Final Battle”. 
* To create the levels and various geometries, I used Probuilder. 95% of the levels were manually created using Probuilder. 
* Unity Particle Effects and Unity Post-Processing helped liven up the game. Those are where the flame, explosion, and motion blur effects come from. 
* Tillium text font was used to enhance font to look much better. Many assets were downloaded off the internet, or the app store. 
* The trees, doors, keys, detonator, barrels & crates, Ball Cat, etc. were models that I found, although all functionality had to be implemented myself. 
* The textures can be found from Brackey’s website. Many colliders had to be created myself, and interactions were challenging, but feasible between these objects. 
* There was A LOT of coding to get this working, so a ton of help with implementing this project came from numerous YouTube videos and unity forum threads (too many to list). 