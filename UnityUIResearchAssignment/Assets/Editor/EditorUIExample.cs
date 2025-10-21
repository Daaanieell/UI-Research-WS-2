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
        dropdown.choices = new List<string> { "Tomahawk", "Shotgun", "Glock" };
        
        dropdown.choices.Add("Microphone");
        dropdown.value = "Tomahawk";
        
        container.Add(dropdown);
        return container;
    }
}