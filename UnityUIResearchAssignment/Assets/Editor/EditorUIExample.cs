using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(TestScript))]
public class EditorUIExample : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var container = new VisualElement();
        var dropdown = new DropdownField();
        dropdown.choices = new List<string> { "Melee", "Ranged", "Magic" };
        
        dropdown.choices.Add("Telepathically");
        dropdown.value = "Melee";
        
        container.Add(dropdown);
        return container;
    }
}