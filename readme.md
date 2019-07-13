## Libraries used
* (SmartData)[https://github.com/sigtrapgames/SmartData] - A 'data' inversion framework that's orientated around designer use in the Editor and based off the talk ('Unite Austin 2017 - Game Architecture with Scriptable Objects
)[https://www.youtube.com/watch?v=raQ3iHhE_Kk]. 
* (NaughtyAttrbiutes)[https://github.com/dbrizov/NaughtyAttributes] - Attributes to add extra functionality to the inspector
* (CommandTerminal)[https://github.com/stillwwater/command_terminal] - Quake like dropdown console to allow for debug commands. Examples of use here was to change scores, add/remove high scores on the leaderboard. Normally I use Touch Console Pro instead of this open source alternative
* (QuickSearch)[https://github.com/appetizermonster/Unity3D-QuickSearch] - Editor tool to have search on a hot key


## Extra notes

### Jusitifications on SmartData
I have my own framework in place of SmartData based on the same talk but involves the use of other libraries such as OdinInspector for some functionality and therefore couldn't use it for this test. 

SmartData isn't perfect and wouldn't use it in a Production environment as both the release and the GitHub repo have bugs (e.g the dispatch button on the SmartEvent in the inspector doesn't work) that have been addressed by the team but haven't yet been made public on the repo.

However, as it's the closest Open Source framework to my own, I'm using it for the test as the runtime logic seems to work fine.

Ideally, I would like to try ZenInject, Entitas or DOTs but haven't yet used them in a signifcant capactity so I'm sticking with what I know due to time constraints.