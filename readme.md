## Libraries used
* [SmartData](https://github.com/sigtrapgames/SmartData) - A 'data' inversion framework that's orientated around designer use in the Editor and based off the talk ['Unite Austin 2017 - Game Architecture with Scriptable Objects
](https://www.youtube.com/watch?v=raQ3iHhE_Kk). 
* [NaughtyAttrbiutes](https://github.com/dbrizov/NaughtyAttributes) - Attributes to add extra functionality to the inspector
* [CommandTerminal](https://github.com/stillwwater/command_terminal) - Quake like dropdown console to allow for debug commands. Examples of use here was to change scores, add/remove high scores on the leaderboard. Normally I use Touch Console Pro instead of this open source alternative
* [QuickSearch](https://github.com/appetizermonster/Unity3D-QuickSearch) - Editor tool to have search on a hot key


## Extra notes

### Improvements that could be made

There's some shared behaviour in terms of movement between Tank and Bullet that could be refactored out. 

I'm not a big fan of how I'm handling the pooling for tanks and bullets but have gone too far with and short on time to change it. Feels very custom and has forced Tank and Bullet to be poolable items when they don't have to be. 

It works but is too highly coupled. Ideally, I would like the Tank/Bullet component to not know that they belong to a pool.

Adding a second could be challenging and would require refactoring in a number of places. I would have to make use of the of the Smart Data multi* types and add the player index to GameObjects such as Bullets to identify who gets the score. 

### Features were tested with the Command Terminal

Using the Command Terminal, I was able to test features in the game easily without needing to add the game logic. This includes checking the game UI was being updated correctly (e.g Player lives) and adding/removing high scores in the leaderboard. 

Press the ` key and type help for the commands. Search the code base for "DebugCommand" for the functions that are tied to the commands.

Command Terminal isn't the most user friendly of terminals for an end user and Touch Console Pro is much better allowing for descriptions and default values for the arguments.

### Jusitifications on SmartData

I have my own framework in place of SmartData based on the same talk but involves the use of other libraries such as OdinInspector for some functionality and therefore couldn't use it for this test. 

This is the first time I've used SmartData so hitting issues during the test.

SmartData isn't perfect and wouldn't use it in a Production environment as both the release and the GitHub repo have bugs (e.g the dispatch button on the SmartEvent in the inspector doesn't work) that have been addressed by the team but haven't yet been made public on the repo.

However, as it's the closest Open Source framework to my own, I'm using it for the test as the runtime logic seems to work fine.

The downside of this approach is that it is very Editor which can make it difficult to follow logic flow from code to Editor and back as well as being restrictive with automated testing (it has to be done via the Editor).

Upsides include that it is more of a 'Unity' way of implementing features and logic so requires less of working around Unity itself and taking advantage of tool features.

Ideally, I would like to explore Entitas or DOTs but haven't yet used them in a signifcant capactity so I'm sticking with what I know due to time constraints. I really want to investigate into DOD with Unity as I have trouble understanding the how to 'glue' between the data and the visuals.


## Making the game 'fun'

TBD