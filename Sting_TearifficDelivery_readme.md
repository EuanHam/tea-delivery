# Tea-riffic Delivery
Brian Park, Euan Ham, Hebe Huang, Isiah Julian Nogal, Madeline Simpson
### Starting Scene

TitleScreen.unity

### Gameplay

Start by pressing “Play” on the title screen and start with Level 0. Click through the instructions using the space bar and start making deliveries within the time limit. You can pause the game by pressing escape at any time.
During each level of the game, you unlock more features that make the game harder. Also, you have less time and lose more points for collisions.
In Level 0, you are limited to a smaller section of the town and win by completing one order.
In Level 1, the rest of the town is unlocked and the NPCs start to move.
In Level 2, blue ice cream trucks are added to the map and you are penalized for colliding with them.
In Level 3, you face the strictest time and point penalty conditions.
Some notable features include AI navigation for NPCs and vehicles, pathfinding for the minimap feature, randomized powerups, sound effects/background music

### Known Problems

If you don’t move away quickly after delivering a drink, you may kill the NPC you just delivered to. Also while the timer doesn’t start until you are finished reading the instructions, it is still possible for vehicles and NPCs to collide with you. Sometimes the mouse may disappear, but pressing esc brings it back.

### Manifest

Euan Ham: worked on navigation, level design, sound, and gameplay loop
/Assets/Scripts
AudioManager.cs
LevelManager.cs
/Assets/Scripts/Player
RobbiController.cs
VehicleCollision.cs
/Assets/Scripts/UI
UIManager.cs
WinScreenManager.cs
/Assets/Scripts/npc
NPCCollision.cs
npcAI.cs
/Assets/Scripts/BobaSystem
BobaDriver.cs
/Assets/Scripts/Level
InstructionManager.cs
/Assets/Scripts/Navigation
LevelSelectionController.cs
TitleScreenController.cs

Brian Park: Made a new level template layout. Rewrote Minimap + BobaDelivery System. Added moving vehicles. Implemented Level, UI, Audio + Sound Effects, NPC, PowerUp Manager Scripts. Implemented player interactions w/ NPC + Vehicle via collisions + triggers. Created 3D assets in Blender. Created ToonShader w/ Unity Shader Graphs.
/Assets/Scripts/BobaSystem/
BobaDriver.cs
NewBobaSystem.cs
NewOrder.cs
/Assets/Scripts/Minimap/
DynamicMinimap.cs
/Assets/Scripts/NPC/
npcAI.cs
NPCCollision.cs
NPCController.cs
/Assets/Scripts/Player/
RobbiController.cs
VehicleCollision.cs
/Assets/Scripts/PowerUps/
PowerUpCollision.cs
PowerUpManager.cs
/Assets/Scripts/UI/
LowResScaling.cs
UIManager.cs
/Assets/Scripts/Vehicle/
VehiclePath.cs
/Assets/Scripts/
LevelManager.cs
AudioManager.cs
/Assets/Models/
character_rig.fbx
robbi.fbx
exclamation_mark.fbx
Question.fbx
/Assets/Models/UpdatedBuildings
4-way-intersection.fbx
90-degreebend.fbx
Base-tile.fbx
Straight-var1.fbx
Straight-var2.fbx
T-intersection.fbx

Hebe Huang: Implemented pausing and high score system, added features to win screen, improved ui, added differences in logic and instructions for each level, initial delivery system
/Assets/Scenes/
Level0Tutorial.unity, Level1.unity, Level2.unity, Level3.unity
/Assets/Scripts/
LevelManager.cs
/Assets/Scripts/Navigation/
LevelSelectionController.cs
CreditsScreenController.cs
/Assets/Scripts/UI/
WinScreenManager.cs, UIManager.cs, PauseScreenManager.cs
/Assets/Scripts/Minimap/
DyanmicMinimap.cs
/Assets/Scripts/npc/
NPCCollision.cs
/Assets/Scripts/Player/
VehicleCollision.cs
/Assets/Scripts/BobaSystem/
BobaDriver.cs
/Assets/Scripts/PowerUps/
PowerUpCollision.cs

Isiah Julian Nogal: UI fixes and responsive resizing, audio based on collisions and button clicking, assisted with general bug fixes across levels, modified level parameters (time limit, balance required, etc.) based on difficulty, edited trailer
/Assets/Scenes/
Level0Tutorial.unity, Level1.unity, Level2.unity, Level3.unity, LevelSelection.unity, TitleScreen.unity
/Assets/Scripts/Level
InstructionManager.cs
NPCCollission.cs
LevelManager.cs
/Assets/Scripts/Navigation
LevelSelectionController.cs
TitleScreenController.cs

Madeline Simpson: Minimap implementation and design, win screen design and functionality, animations, NPC boba order system, balance penalties for collisions, assisting with jumping feature
/Assets/Textures/
RobbiRenderTexture.renderTexture
/Assets/Scripts/UI/
WinScreenManager.cs
UIManager.cs
WinScreenRobot.cs
/Assets/Scripts/Player/
RobbiController.cs
/Assets/Scripts/npc/
NPCCollision.cs
/Assets/Scripts/Minimap/
Minimap.cs
AStarPathFinder.cs
IntersectionNodes.cs
/Assets/Scripts/BobaSystem/
BobaDriver.cs
/Assets/Prefab/
Minimap.prefab
Road Nodes.prefab
Minimap Icon.prefab
Minimap UI Element.prefab
