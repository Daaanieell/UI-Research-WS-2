
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponGridMain : EditorWindow
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;
    private NPCHelper npcHelper;
    private WeaponGrid Wg;
    DropdownField typeDropdown;

    private NPC selectedNPC;
    private TextField nameField;
    private IntegerField healthField;
    private IntegerField maxHealthField;
    private PopupField<string> weaponPopup;

    private WeaponLoaderManager wlm = new WeaponLoaderManager();

    private List<GameObject> weaponPrefabs;

    [SerializeField] private List<GameObject> weapons;

    List<Weapon> allWeapons = new List<Weapon>();

    [MenuItem("Tools/WeaponGridMain")]
    public static void ShowExample()
    {
        WeaponGridMain wnd = GetWindow<WeaponGridMain>();
        wnd.titleContent = new GUIContent("WeaponGridMain");
    }

    // //TODO: this needs to be properly implemented and replaced
    // //  (stolen from Akram)
    // void LoadAllWeapons()
    // {
    //     string[] guids = AssetDatabase.FindAssets("t:Weapon", new[] { "Assets/Test Weapons" });

    //     foreach (var guid in guids)
    //     {
    //         string path = AssetDatabase.GUIDToAssetPath(guid);
    //         Weapon prefab = AssetDatabase.LoadAssetAtPath<Weapon>(path);
    //         allWeapons.Add(prefab);
    //         Debug.Log(prefab);
    //     }
    // }

    public void CreateGUI()
    {
        wlm.LoadAllWeapons(ref allWeapons);
        npcHelper = new NPCHelper(new WeaponGridMain());
        Wg = new WeaponGrid(npcHelper);
   
        npcHelper.SetSelectedNPC(selectedNPC);

        VisualElement root = rootVisualElement;

        VisualElement uxmlContent = m_VisualTreeAsset.Instantiate();
        root.Add(uxmlContent);
        typeDropdown = uxmlContent.Q<DropdownField>("WeaponDropDown");
        var grid = uxmlContent.Q<ScrollView>("grid");

        ObjectField npcField = npcHelper.NPCObjField();
        npcField.RegisterValueChangedCallback(evt =>
        {
            selectedNPC = evt.newValue as NPC;

            RefreshFields();
        });
        root.Add(npcField);


        typeDropdown.choices = new List<string> { "All", "Melee", "Ranged", "Magic" };
        typeDropdown.value = "All";
        typeDropdown.RegisterValueChangedCallback(evt =>
        {
            grid.Clear();
            VisualElement updatedGrid = Wg.FillWeaponGrid(allWeapons, typeDropdown);
            grid.Add(updatedGrid);

        });
        grid.Clear();
        grid.Add(Wg.FillWeaponGrid(allWeapons, typeDropdown));


        nameField = new TextField("Name");
        nameField.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC == null)
            {
                Debug.LogWarning("selectedNPC = null!!!");
                return;
            }
            selectedNPC.Name = evt.newValue;
            RefreshHealthBarAndScene();
        });
        root.Add(nameField);

        healthField = new IntegerField("Health");
        healthField.isDelayed = false;
        healthField.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC == null)
            {
                Debug.LogWarning("selectedNPC = null!!!");
                return;
            }
            selectedNPC.Health = Mathf.Clamp(evt.newValue, 0, selectedNPC.MaxHealth);
            RefreshHealthBarAndScene();
        });
        root.Add(healthField);

        maxHealthField = new IntegerField("Max Health");
        maxHealthField.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC == null)
            {
                Debug.LogWarning("selectedNPC = null!!!");
                return;
            }
            ;
            selectedNPC.MaxHealth = Mathf.Max(1, evt.newValue);
            selectedNPC.Health = Mathf.Clamp(selectedNPC.Health, 0, selectedNPC.MaxHealth);

            healthField.SetValueWithoutNotify(selectedNPC.Health);

            RefreshHealthBarAndScene();
        });
        root.Add(maxHealthField);
    }

    public void RefreshFields()
    {
        if (selectedNPC == null)
            return;

        Debug.Log("RefreshFields");
        nameField.SetValueWithoutNotify(selectedNPC.Name);
        healthField.SetValueWithoutNotify(selectedNPC.Health);
        maxHealthField.SetValueWithoutNotify(selectedNPC.MaxHealth);
    }

    private void OnInspectorUpdate()
    {
        if (selectedNPC != null)
        {
            healthField.SetValueWithoutNotify(selectedNPC.Health);
            maxHealthField.SetValueWithoutNotify(selectedNPC.MaxHealth);
            nameField.SetValueWithoutNotify(selectedNPC.Name);
        }
    }

    private void RefreshHealthBarAndScene()
    {
        if (selectedNPC != null)
        {
            if (selectedNPC.TryGetComponent<HealthBarUI>(out var hb))
                hb.RefreshFromEditor();

            EditorUtility.SetDirty(selectedNPC);
            EditorSceneManager.MarkSceneDirty(selectedNPC.gameObject.scene);
        }

        SceneView.RepaintAll();
        Repaint();
    }
}