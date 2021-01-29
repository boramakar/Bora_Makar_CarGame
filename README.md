# Cars Demo Project

This project is done as a job interview for MobGe. Time limit was 5 days. Meant as a prototype implementation but depending on how much time is left I might implement additional features like menu/UI, animations

### DAY 1
#### COMPLETED:
- PREFABS
  - Base Prefabs
    - Car model
    - Obstacle base model (Might model proper obstacles later)
    - End marker model
  - Path Prefab
  - Camera/Canvas Prefab
  - Materials
  
- MOVEMENT
  - Keyboard inputs
  - Touch inputs (Not tested)
  
- Levels
  - Level 1
  - Level 2

##### PARTIAL

- PAST CARS
  - Method 1 - Save positions through FixedUpdate and go through them later on
  - Method 2 - Save delta timestamps for user inputs and simulate them through coroutines

##### PLANNED FOR LATER

- SFX
  - Drive sound/music (might do proximity based volume adjustment as bonus)
  - Crash sound
  - Path completion sound
  - Level completion sound
- MENU

##### BONUS

- ANIMATIONS
  - Car rewind
  - Car crash
  - Screen change effect
- PARTICLES
  - Car crash
- LEVEL STATS (Completion time, attempt/crash count, etc.)

### DAY 2
#### COMPLETED
- PAST CARS
  - Method 1
  
- COLLISION
  - Obstacle collision
  - End marker collision
  - Collision marker for identifying the object player collided with

#### FIXED
- LEVELS
  - Rearranged the level design to better fit the camera size
  - Moved camera further from flooe for better lighting of collision marker
  - Moved game controller script from canvas to a separate object
  
#### BUGS
- MOVEMENT
  - Last rotation applied when hitting the end marker is carried over to the new car as default rotation
