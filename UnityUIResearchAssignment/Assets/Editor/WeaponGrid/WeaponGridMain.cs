
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
        
        VisualElement NPCEditorContainer = new VisualElement();
        NPCEditorContainer.AddToClassList("npc-container");

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
        NPCEditorContainer.Add(nameField);

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
        NPCEditorContainer.Add(healthField);

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
        NPCEditorContainer.Add(maxHealthField);

        root.Add(NPCEditorContainer);
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