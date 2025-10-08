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

