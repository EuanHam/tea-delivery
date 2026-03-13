Add the CityMap prefab and then add black planes as barriers if there are any off limit areas for that level
- this may just apply to the tutorial if i'm being honest though

Add in Robbi. Look at Robbi.md for configuring that

Create a canvas (Ui -> canvas)
- Attach InstructionManager.cs. after the next steps configure the appropriate elements
Add ui elements to the canvas
- TextMeshPro: "InstructionText"
- Panel: "Congrats" (disable this initially)
  - on the hierarchy below: TextMeshPro: "Congrats you delivered the boba"

Put a vehicle tag on Robbi (on the outer game object)

For the target:
- add a box collider
- for the dummy placeholder we have make the center (0, 1, 0) and size (10, 2, 10)
- add the target script from scripts/level
