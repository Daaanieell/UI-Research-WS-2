# How to implement a dropdown with editor UI & How to implement a dropdown in editor UI for weapons? (same question almost)

## Sources

[Unity Manual DropdownField](https://docs.unity3d.com/6000.0/Documentation/Manual/UIE-uxml-element-DropdownField.html)

### Code from the link, changed, so it's weapons

For the EditorUI you need to create a folder specifically called **Editor**, the script created inside has to inherit from **Editor**.
You will have to override a function _*'CreateInspectorGUI()'*_ inside this function you can create and return a VisualElement, which you have to instantiate first using the 'new' keyword.
Inside the code block below you can see just that and afterwards there's a dropdownfield instantiated, filled with a few options and in the end added to the container and returned.

The script inside the **Editor** folder will be attached to a GameObject which has a MonoBehaviour script, the name of this script is also the name you reference in your EditorUI script, in this case it's 'TestScript'.(_[CustomEditor(typeof(testscript))]_)



<img width="720" height="640" alt="2025-10-21-043848_hyprshot" src="https://github.com/user-attachments/assets/ecd74340-bf4d-4f0f-80cc-feafefe61278" />


---
```
[CustomEditor(typeof(TestScript))]
public class EditorUIExample : Editor
{

    public override VisualElement CreateInspectorGUI()
    {
        var container = new VisualElement();
        var dropdown = new DropdownField();
        dropdown.choices = new List<string> {"Melee", "Ranged", "Magic"};
        
        dropdown.choices.Add("Telepathically");
        dropdown.value = "Melee";

        container.Add(dropdown);
        return container;
    }
}
```


