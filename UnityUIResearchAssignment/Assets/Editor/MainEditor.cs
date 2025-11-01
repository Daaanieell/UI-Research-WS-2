
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class MainEditor : EditorWindow
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

    [MenuItem("Tools/MainEditor")]
    public static void ShowExample()
    {
        MainEditor wnd = GetWindow<MainEditor>();
        wnd.titleContent = new GUIContent("MainEditor");
    }

    public void CreateGUI()
    {
        wlm.LoadAllWeapons(ref allWeapons);
        npcHelper = new NPCHelper(new MainEditor());
        Wg = new WeaponGrid(npcHelper);

        VisualElement root = rootVisualElement;

        VisualElement uxmlContent = m_VisualTreeAsset.Instantiate();
        root.Add(uxmlContent);
        typeDropdown = uxmlContent.Q<DropdownField>("WeaponDropDown");
        var grid = uxmlContent.Q<ScrollView>("grid");

        var weaponSelectorContainer = uxmlContent.Q<VisualElement>("Weapon Selector");
        
        ObjectField npcField = npcHelper.NPCObjField();
        npcField.RegisterValueChangedCallback(evt =>
        {
            selectedNPC = evt.newValue as NPC;

            RefreshFields();
        });
        weaponSelectorContainer.Add(npcField);


        // ---------------------- NPC editor down here ---------------------- 
        #region
        
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

        #endregion
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