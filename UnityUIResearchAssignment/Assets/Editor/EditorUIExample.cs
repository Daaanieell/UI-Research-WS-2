using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(TestScript))]
public class EditorUIExample : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var container = new VisualElement();
        
        var label = new Label("Weapon Selector");
        label.style.unityFontStyleAndWeight = FontStyle.Bold;
        label.style.marginBottom = 4;
        label.style.unityTextAlign = TextAnchor.MiddleCenter;
        
        var dropdown = new DropdownField();
        dropdown.choices = new List<string> { "Melee", "Ranged", "Magic" };
        dropdown.value = "Melee";

        container.Add(label);
        container.Add(dropdown);
        return container;
    }
}