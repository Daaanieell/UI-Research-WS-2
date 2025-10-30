# 🧙‍♂️ Unity Editor UI Workshop
## 🔥 Follow along with the steps listed below to create an Editor UI in Unity!

### 🧑‍💻 Step 1: Clone the GitHub repository to your machine.
Using the terminal:
1. Grab the HTTPS link from the repository's main branch.
2. Use the ```git clone [repo-link-here]``` command inside a folder you created for this workshop.

Using GitHub desktop:
1. Click **“Code”** followed by **“Open with GitHub Desktop.”**  
2. Choose a local folder where the repository should be cloned.  
3. Click **“Clone.”**  

Or use your own preferred method. 💅

---
### 🙆‍♂️ Step 2: 
Opening the project in Unity Hub.
1. Launch Unity Hub.
2. Click *'Add project from disk'* and select the cloned folder.
3. After loading in make sure to load UIToolkitScene before moving on.

---
### 💰 Step 3 & 4
HealthBar and Floating Name Tutorial.

1. Right-Click on your 'Assets' folder, click on 'Create', click on 'UI Toolkit' and finally on UI Document. Open the created file, it should open it automatically in the UI Builder. You will know this has worked if the newly opened page says 'UI Builder' at the top!
2. (Still inside the UI Builder) In the Hierarchy on the left, drag a Visual Element in (which you can find in the Containers section inside the Library, under the Hierarchy) Also drag a label (under controls) in as a child of the Visual Element. Then drag a progressbar (also under controls) in as a child of the Visual Element.
3. Rename 'Label' to 'nameLabel' and 'ProgressBar' to 'healthBar'.
4. Click on the + sign in the Stylesheet section in the top left of your screen to create a new uss (Unity Style Sheet) and give it the same name as the UI Document.
5. Click on the 'Add new selector...' field (under Stylesheet) to add classes, these classes can then be customized so that every progressbar looks the same. You can also add new classes by clicking on a Visual Element in the Hiearchy and then click on 'Add Style Class To List' under the Stylesheet on the right side of your screen in the Inspector.
6. You can now design the health-bar and give it name you prefer, you can even design it by opening up the uss-file inside your editor of choice and edit it there. (like CSS)
7. After you're satisfied with your work on the beautiful health-bar you have created, right-click in your Assets folder, click on 'Create', click on 'UI Toolkit' and finally on 'Panel Setting Asset', inside the inspector, under 'Render Mode' choose 'World Space'.
8. Now add an empty GameObject to the NPC, right-click on the newly create GameObject and click on Add Component, click on UI Document. Now you can add the Panel Settings the UXML file. Afterwards a new panel will appear under the fields, here in Size Mode you set it to Dynamic.
9. You will now have a health-bar and a name above the player, good job! To get some actual logic for the health-bar you will need a health-bar script! Create a new script and add this script to the (empty) GameObject, don't worry this one is on us:

```c#
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteAlways]
public class HealthBarUI : MonoBehaviour
{
    [Header("Target NPC")]
    [SerializeField] private NPC npcTarget;
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, 0);

    private UIDocument uiDoc;
    private ProgressBar healthBar;
    private Label nameLabel;
    private VisualElement progressFill;
    private Camera mainCam;

    private float lastHealth;
    private float lastMaxHealth;
    private string lastName;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        uiDoc = GetComponent<UIDocument>();

        var root = uiDoc.rootVisualElement;
        healthBar = root.Q<ProgressBar>("healthBar");
        nameLabel = root.Q<Label>("nameLabel");
        progressFill = healthBar?.Q(className: "unity-progress-bar__progress");
        mainCam = Camera.main;

        UpdateUI(force:true);
    }

    private void LateUpdate()
    {
        if (npcTarget == null || uiDoc == null)
            return;

        if (mainCam == null)
            mainCam = Camera.main;

        if (mainCam != null)
        {
            transform.position = npcTarget.transform.position + offset;
            transform.rotation = Quaternion.Euler(0, mainCam.transform.eulerAngles.y, 0);
        }

        bool changed = npcTarget.Health != lastHealth ||
                       npcTarget.MaxHealth != lastMaxHealth ||
                       npcTarget.Name != lastName;

        if (changed)
            UpdateUI(force: false);
    }

    private void UpdateUI(bool force)
    {
        if (healthBar == null || nameLabel == null || npcTarget == null)
            return;

        float maxHealth = Mathf.Max(1, npcTarget.MaxHealth);
        float currentHealth = Mathf.Clamp(npcTarget.Health, 0, maxHealth);
        float ratio = currentHealth / maxHealth;

        nameLabel.text = npcTarget.Name;
        healthBar.highValue = maxHealth;
        healthBar.value = currentHealth;

        if (progressFill != null)
        {
            Color barColor = Color.Lerp(Color.red, Color.green, ratio);
            progressFill.style.backgroundColor = new StyleColor(barColor);
        }

        lastHealth = currentHealth;
        lastMaxHealth = maxHealth;
        lastName = npcTarget.Name;

        if (!Application.isPlaying)
        {
            EditorApplication.QueuePlayerLoopUpdate();
            SceneView.RepaintAll();
        }
        
    }

    public void RefreshFromEditor()
    {
        UpdateUI(force: true);
    }
}
```

### Step 5: Weapon Selector
In this step, you will create a Weapon Selector that allows you to change the weapon in the player's hand directly from the Scene view.
This tool will make it easier for designers to test and adjust configurations during the development phase without needing to enter Play Mode every time.

#### Step 5.1: Creating your first VisualElement

**Useful Unity documentation:** 
- [Labels](https://docs.unity3d.com/6000.2/Documentation/Manual/UIE-uxml-element-Label.html)
- [Get started with UI Builder](https://docs.unity3d.com/6000.2/Documentation/Manual/UIB-getting-started.html)

In this step you will be creating your first Visual Element, a title for the "Weapon Selector". Creating a static label can be done in two ways:
1. Using the UI Builder, which is best for static elements in your UI.
2. Opening the UXML file inside your preferred editor, which allows for more control over each element.

**Method 1: Using the UI Builder**
1. It's time to create an Editor Window! Right-click on your 'Assets' folder, click on 'Create', click on 'UI Toolkit' and finally on 'Editor Window'. 
2. Open the UI Builder once more by double-clicking on your UXML-document.
![[506619646-86926ca6-65b3-41d3-9ec5-b3be8e7cd2ff.png]]

3. Using the UI Builder window, navigate to the 'Library' section and search for a label.
4. Drag the Label to the canvas and give it a name, e.g. 'Weapon Selector'.

You can change the properties of this label using the **'Inspector'** window in the UI Builder on the right side of your screen.

**Method 2: Editing through the use of the UXML file**
1. Open the .UXML file with your preferred editor. (You can do so by clicking on the arrow of the UXML file, a stylesheet file will pop out and once double-clicked, it will open the actual UXML)
2. Add the following: `<ui:Label class="my-title" text="Weapon Selector" />`
3. Open the .USS file with your preferred editor.
- Add your own styling within the .USS file, e.g.: `.my-title { font-size: 15px; color: blue; }`

Your end result should look something like this:

![[506620557-51d6da6d-c693-4491-aba6-32c09fc776d1.png]]

#### Step 5.2: Create a Dropdown Field

This step explains how to add a **DropdownField** in UI Toolkit and make it filter through a list of weapons in your custom Unity Editor Window.

1. **Add a Dropdown Field**
   - In **UI Builder**, go to the **Library**, search for `DropdownField`, and add it to your visual element.
   - Once you have added a `VisualElement`, you can add a `DropdownField` to it.

2. **Make the Dropdown Visible**
   - In the **UXML** file, make sure the `DropdownField` is visible in your layout and has a unique name (e.g., `WeaponDropDown`).

3. **Instantiate Your VisualTreeAsset**
   - In your editor script, make sure to instantiate your `VisualTreeAsset` and add it to the `rootVisualElement`:
    ```c#
     var visualTree = m_VisualTreeAsset.Instantiate();
     rootVisualElement.Add(visualTree);
     ```

4. **Reference the Dropdown in Code**
   - Inside the `CreateGUI()` method, get your dropdown from the UXML:
     ```csharp
     typeDropdown = visualTree.Q<DropdownField>("WeaponDropDown");
     ```

5. **Assign Choices to the Dropdown**
   - Use `dropDownField.choices` to add a new list of weapon types:
     ```csharp
     typeDropdown.choices = new List<string> { "All", "Melee", "Ranged", "Magic" };
     ```
   - Set the first value to `"All"` so that all weapons are shown when the editor window first opens:
     ```csharp
     typeDropdown.value = "All";
     ```

6. **Register the Value Change Event**
   - To refresh your editor window when the selection changes, use:
     ```csharp
     typeDropdown.RegisterValueChangedCallback(evt =>
     {
         grid.Clear();
         VisualElement updatedGrid = Wg.FillWeaponGrid(allWeapons, typeDropdown);
         grid.Add(updatedGrid);
     });
     ```

7. **Render the Initial Grid**
   - When the editor window opens, build the grid once so that all weapons are shown:
     ```csharp
     grid.Clear();
     grid.Add(Wg.FillWeaponGrid(allWeapons, typeDropdown));
     ```

8. **Filter Weapons by Dropdown Value**
   - In your `FillWeaponGrid()` method, reference the dropdown’s value and check if it’s not `"All"`.
   - You can use a lambda function that selects only the weapons that match the dropdown value:
     ```csharp
     if (selectedType != "All")
         filteredWeapons = weapons.FindAll(w => w.weaponType.ToString() == selectedType);
     ```

9. **Return and Display the Filtered List**
   - Save the filtered list in a new `List<Weapon>` and return it to be displayed.
   - Pass the returned list into your loop that creates the weapon items in the grid.

10. **Test the Dropdown**
    - When you open your editor window:
      - `"All"` should be selected by default and display every weapon.
      - Selecting `"Melee"`, `"Ranged"`, or `"Magic"` should instantly update the grid to show only that weapon type.


#### Step 5.3: Weapon Grid

The weapon grid is a grid with weapons which an NPC can equip.

It currently lacks:
1. The ability to load weapons into the grid (use the given WeaponLoader script)
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
- To load weapons you first need to create a `List<>` of the Weapon Scriptable Object. You can store this list inside the main script.
  Example:
  ```C#
  List<Weapon> = new List<Weapon>();
  ```

- Now use the `LoadWeapons()` function from the WeaponLoader class to get all the weapons and add them to the list.
- Using parameters, pass the list to the `GridMaker()` function and have each item in the grid contain an image and the name of the weapon.
  > Note: images can be loaded using the `loadImageForWeapon()` function
- To add text and images to a VisualElement, you can create a new VisualElement in your C# script and then add the VisualElement to another using the `VisualElement.Add(VisualElement)`.

You should now have a weapon grid that dynamically loads weapons.

#### Styling
WeaponGrid.cs:
- For each VisualElement that is created, use the AddToClassList("my-class") function to add a class to the VisualElement. e.g.: `grid-container`, `grid-row`, and `grid-item`.

USS file:
- To style the grid, create and add 3 classes to the .USS file; `.grid-container`, `.grid-row`, and `.grid-item`.
- Give each class a different `background-color` to differentiate between each VisualElement in the UI.
- Give each class a fitting `margin` property.
- Use the CSS selectors to style the image within the items, e.g.:

  ```CSS
  .grid-item > .unity-image {
      <!-- this will style the image within every .grid-item -->
  }
  ```
- Give the image within an item the property: `position: absolute;` and other appropriate size properties.

#### Selecting and equipping a weapon

To equip a weapon on an NPC, you need to add the Weapon Scriptable Object. Using the `SetWeaponPrefab()` and `RefreshWeapon()` functions from the NPC script, a weapon can be equipped.

Using these functions within the item's OnClick event, you can make the (grid-)item select a weapon and equip it on the NPC.

Within the WeaponGrid class:

- Find the weapons OnClick event within the given code.
- Use the `SetWeaponPrefab()` function to set the weapon prefab.
- Use the `RefreshWeapon()` function to refresh the weapon on the NPC.
#### The end result

Your weapongrid is now finished and should look like this:

<img width="409" height="861" alt="image" src="https://github.com/user-attachments/assets/b88a789a-a287-4766-af24-18459d3ce974" />



#### Step 5.4: Selecting NPCs

Requirements: NPCHelper.cs

The field itself is created and managed by the helper class NPCHelper, which keeps your editor code clean and modular.

Open WeaponGridMain.cs and locate the CreateGUI() method

Once your npcHelper instance has been initialized, add the following code:

```c#
ObjectField npcField = npcHelper.NPCObjField();
npcField.RegisterValueChangedCallback(evt =>
{
        selectedNPC = evt.newValue as NPC;
        RefreshFields();
});
root.Add(npcField);
```

When you open your editor window (Tools -> WeaponGridMain) you’ll now see a field labeled “Select NPC” at the top.

Drag and drop or choose an NPC GameObject from your scene into this field.


#### Step 5.5: Changing weapons and stats

Requirements: NPC.cs, RefreshFields, RefreshHealthBarAndScene

Go to WeaponGridMain.cs and add a new Visuelelement (e.g. NPCEditorContainer)

Now you can start creating the fields for all attributes -> this works exactly as in step 5.4, so just try it out for yourself

-> don't forget that nameField, healthField and maxHealthField is not a ObjectField

-> use Mathf.Max for the healthField and Mathf.Clamp for the maxHealthField

-> after initializing healthField, add “healthField.isDelayed = false;” (this makes it work better).

-> add healthField.SetValueWithoutNotify(selectedNPC.Health); to the end of the maxHealthField

add the your container to root

to change weapons -> create a new method called EquipWeaon in NPCHelper.cs (use the functions of NPC.cs)

now open WeaponGrid.cs and modify the method "GenerateItemElement" -> call the the new created method when an item is clicked


## The End

Congratulations, you've completed the tutorial!
By now you learned how to use Unity's UI Toolkit to build both in-game and editor UI elements.
You've explored how the Editor UI can interact with objects in the Scene, and you've written a C# script for UI Toolkit.

### At this point you should have:

- A Health Bar and Floating Name displayed above your player which appears in-game
- A functional Weapon Selector built with the Editor UI

### 🤓 If you somehow got to this step before the time ran out, then here's an extra challenge for you, 10x developer! 

#### Step 6: Creating a minimap!
