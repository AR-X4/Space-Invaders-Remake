# Space-Invaders-Remake
Remake of the 1978 Space Invaders arcade game.  Built from scratch using 15 real-time design patterns and a custom OpenGL based game engine. 
Please see design documentation for breakdown of design patterns.

Run in Visual Studio.

The game can be played in 1 player mode or 2 player mode.  In 2 player mode, the game will switch players each time a player dies.

Built to be played in on a monitor with a 143Hz refresh rate.  It is not optimized for lower refresh rates.  Objects will move slower 
than intended with lower refresh rates.

Controls:
    Cycle from Title scene to Player Select scene:          'Space Bar'
    Select 1 or 2 player mode in Player Select scene:       '1' key or '2' key
    
    Move player ship left and right in Game scene:          'Left Arrow' key and 'Right Arrow' key
    Fire player ship missile in Game scene:                 'Space Bar'
    Toggle visualization of collision boxes in Game scene:  'C'

    Cycle from Game Over scene to Player Select scene:      'Space Bar'

