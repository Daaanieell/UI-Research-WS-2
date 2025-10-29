
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponGridMain : EditorWindow
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;
    private ObjectField npcField;
    private NPCHelper npcHelper;
    private WeaponGrid Wg;

    [SerializeField] private List<GameObject> weapons;

    //TODO: temp list for testing weapon grid replace this
    List<Weapon> allWeapons = new List<Weapon>();

    [MenuItem("Tools/WeaponGridMain")]
    public static void ShowExample()
    {
        WeaponGridMain wnd = GetWindow<WeaponGridMain>();
        wnd.titleContent = new GUIContent("WeaponGridMain");
    }

    //TODO: this needs to be properly implemented and replaced
    //  (stolen from Akram)
    void LoadAllWeapons()
    {
        string[] guids = AssetDatabase.FindAssets("t:Weapon", new[] { "Assets/Test Weapons" });

        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Weapon prefab = AssetDatabase.LoadAssetAtPath<Weapon>(path);
            allWeapons.Add(prefab);
            Debug.Log(prefab);
        }
    }

    public void CreateGUI()
    {
        LoadAllWeapons();

        npcHelper = new NPCHelper();
        Wg = new WeaponGrid(npcHelper);

        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement uxmlContent = m_VisualTreeAsset.Instantiate();
        root.Add(uxmlContent);

        var grid = uxmlContent.Q<ScrollView>("grid");

        VisualElement npcField = npcHelper.NPCObjField();
        root.Add(npcField);

        VisualElement gridItems = Wg.FillWeaponGrid(allWeapons);
        grid.Add(gridItems);
    }
}