using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

public class WindowEditor : EditorWindow
{
    [MenuItem("Window/Dropdown Test")]
    static void ShowWindow()
    {
        GetWindow<WindowEditor>("Dropdown Test");
    }

    public void CreateGUI()
    {
        var dropdown = new DropdownField();
        dropdown.choices = new List<string> {"weapon1", "weapon2", "weapon3"};
        
        dropdown.choices.Add("weapon4");
        dropdown.value = "weapon1";
        
        dropdown.style.width = 300;
        dropdown.style.height = 50;
        rootVisualElement.Add(dropdown);
    }
}
