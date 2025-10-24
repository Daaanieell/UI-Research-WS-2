using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EditorUIExample : EditorWindow
{
    [MenuItem("Window/Weapon Selector")]
    static void ShowWindow()
    {
        GetWindow<EditorUIExample>("Weapon Selector");
    }

    public void CreateGUI()
    {
        // basically CSS styling, this could also be done in the UI-Builder I believe!
        var label = new Label("Weapon Selector");
        label.style.unityFontStyleAndWeight = FontStyle.Bold;
        label.style.color = Color.aquamarine;
        label.style.fontSize = 15;
        label.style.marginBottom = 5;
        label.style.marginTop = 5;
        label.style.unityTextAlign = TextAnchor.MiddleCenter;
        
        var dropdown = new DropdownField();
        dropdown.choices = new List<string> { "Melee", "Ranged", "Magic" };
        dropdown.value = "Melee";

        // adding the created elements to the editor window to display them.
        rootVisualElement.Add(label);
        rootVisualElement.Add(dropdown);
    }
}