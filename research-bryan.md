# How to implement a dropdown with editor UI? & How to implement a dropdown in editor UI for weapons?

## Sources

[Unity Manual DropdownField](https://docs.unity3d.com/6000.0/Documentation/Manual/UIE-uxml-element-DropdownField.html)

### Code from the link, changed, so it's weapons
```
var dropdown = new DropdownField(new List<string> { "AK-47", "Crossbow", "Dagger" }, 0);

// Add another weapon
dropdown.choices.Add("Tomahawk");

// Register to the value changed callback.
dropdown.RegisterValueChangedCallback(evt => Debug.Log(evt.newValue));

// Style the dropdown.
dropdown.style.width = 200;
dropdown.style.height = 50;
```

It will need the namespace **UnityEngine.UIElements**

<img width="390" height="281" alt="image" src="https://github.com/user-attachments/assets/e3ba15bc-4fc9-4dd7-ac62-100553e642f0" />

*_Quick example I made with toolkit for EditorWindow_*

```
public class TestEditor : EditorWindow
{
    [MenuItem("Window/Dropdown Test")]
    static void ShowWindow()
    {
        GetWindow<TestEditor>("Dropdown Test");
    }
    public void CreateGUI()
    {
        var dropdown = new DropdownField();
        dropdown.choices = new List<string> {"Tomahawk", "AK-47", "Shotgun", "Idk any more guns"};
        dropdown.value = "Tomahawk";

        dropdown.style.width = 200;
        dropdown.style.height = 50;
        rootVisualElement.Add(dropdown);
    }
}
```

