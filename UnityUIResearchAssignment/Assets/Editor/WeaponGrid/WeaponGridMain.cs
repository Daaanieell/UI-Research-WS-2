
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponGridMain : EditorWindow
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;

    private WeaponGrid Wg = new WeaponGrid();

    [MenuItem("Tools/WeaponGridMain")]
    public static void ShowExample()
    {
        WeaponGridMain wnd = GetWindow<WeaponGridMain>();
        wnd.titleContent = new GUIContent("WeaponGridMain");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement uxmlContent = m_VisualTreeAsset.Instantiate();
        root.Add(uxmlContent);

        var grid = uxmlContent.Q<ScrollView>("grid");

        VisualElement gridItems = Wg.FillWeaponGrid();
        grid.Add(gridItems);
    }
}