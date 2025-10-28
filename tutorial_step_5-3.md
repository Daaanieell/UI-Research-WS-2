> i kept the file seperate in case of merge conflicts

### Step 5.3: Weapon Grid

The weapon grid is a grid with weapons that can be equipped onto an NPC.

It currently lacks:

1. Ability to load weapons into the grid (use the given WeaponLoader script)
2. Styling (use the .USS file)
3. Ability to select/equip a weapon

**Pseudo code overview**

```C#
//WeaponGrid.cs

public class WeaponGrid {

	VisualElement GridMaker() {
		VisualElement gridContainer = new VisualElement();

		//rows
		for (row < maxRows) {
			VisualElement rowContainer = new VisualElement();

			//columns
			for (column < maxColumns) {
			VisualElement item = new VisualElement();

           item.OnClick(() => {
			    //this is an OnClick event for each item
               //add logic here for selecting/equipping a weapon
           })

			rowContainer.Add(item)
			}

			gridContainer.Add(rowContainer)
		}
		return gridContainer
	}
}
```

#### Loading weapons

- To load weapons you first need to create a `List<>` of the Weapon scriptable object. You can store this list in the main script.
  Example:

  ```C#
  List<Weapon> = new List<Weapon>();
  ```

- Now use the `LoadWeapons()` function from the WeaponLoader.cs to get all the weapons and add them to the list.
- Using parameters, pass the list to the `GridMaker()` function and have each item in the grid contain an image and the name of the weapon.
  > Note: images can be loaded using the `loadImageForWeapon()` function
- To add text and images to a VisualElement, you can create a new VisualElement in your C# script and then add it to another using the `VisualElement.Add(VisualElement)`.

You should now have a weapon grid that dynamically loads weapons.

#### Styling

USS Styling is similar to CSS, but be aware it lacks some modern features.

WeaponGrid.cs:

- For each VisualElement that is created, use the AddToClassList(".my-class") function to add a class to the VisualElement. e.g.: `grid-container`, `grid-row`, and `grid-item`.

USS file:

- To style the grid, create and add 3 classes to the .USS file; `.grid-container`, `.grid-row`, and `.grid-item`.
- Give each class a different `background-color` to see differentiate between each VisualElement in the UI.
- Give each class a fitting `margin` property
- Use the CSS selectors to style the image within the items, e.g.:
  ```CSS
  .grid-item > .unity-image {
      <!-- this will style the labels within the item -->
  }
  ```

- Give the image within an item the property: `position: absolute;` and other appropriate size properties.

#### Selecting and equipping a weapon

> WIP, implementation might change

Your weapongrid is now finished and should look like this:

<img width="409" height="861" alt="image" src="https://github.com/user-attachments/assets/b88a789a-a287-4766-af24-18459d3ce974" />

