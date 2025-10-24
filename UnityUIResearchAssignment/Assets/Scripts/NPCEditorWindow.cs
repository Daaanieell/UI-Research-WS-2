using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCEditorWindow : EditorWindow
{
    private NPC selectedNPC;
    private ObjectField npcField;
    private TextField nameField;
    private IntegerField healthField;
    private IntegerField maxHealthField;
    private SliderInt healthSlider;
    private PopupField<string> weaponPopup;
    private List<GameObject> weaponPrefabs;
    private Button equipButton;

    [MenuItem("Tools/NPC Editor")]
    public static void ShowWindow()
    {
        var window = GetWindow<NPCEditorWindow>();
        window.titleContent = new GUIContent("NPC Editor");
        window.minSize = new Vector2(250, 200);
    }

    public void CreateGUI()
    {
        
        var root = rootVisualElement;
        root.style.paddingLeft = 10;
        root.style.paddingRight = 10;
        root.style.paddingTop = 10;

        npcField = new ObjectField("Select NPC");
        npcField.objectType = typeof(NPC);
        npcField.RegisterValueChangedCallback(evt =>
        {
            selectedNPC = evt.newValue as NPC;
            RefreshFields();
        });
        root.Add(npcField);

        nameField = new TextField("Name");
        nameField.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC != null)
                selectedNPC.Name = evt.newValue;
        });
        root.Add(nameField);

        healthField = new IntegerField("Health");
        healthField.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC != null)
                selectedNPC.Health = evt.newValue;
        });
        root.Add(healthField);

        maxHealthField = new IntegerField("Max Health");
        maxHealthField.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC != null)
                selectedNPC.MaxHealth = evt.newValue;
        });
        root.Add(maxHealthField);

        healthSlider = new SliderInt(0, 100) { label = "Health Slider" };
        healthSlider.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC != null)
                selectedNPC.Health = evt.newValue;
        });
        root.Add(healthSlider);

        weaponPrefabs = new List<GameObject>();
        var weaponNames = new List<string>();

        string[] guids = AssetDatabase.FindAssets("t:GameObject", new[] { "Assets/Prefabs" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null)
            {
                weaponPrefabs.Add(prefab);
                weaponNames.Add(prefab.name);
            }
        }

        weaponPopup = new PopupField<string>("Weapon", weaponNames, 0);
        weaponPopup.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC == null)
                return;

            int index = weaponNames.IndexOf(evt.newValue);
            if (index >= 0 && index < weaponPrefabs.Count)
            {
                var selectedPrefab = weaponPrefabs[index];
                selectedNPC.SetWeaponPrefab(selectedPrefab);

                if (Application.isPlaying)
                    selectedNPC.EquipWeapon();
                else
                    EditorUtility.SetDirty(selectedNPC);
            }
        });
        root.Add(weaponPopup);
    }

    private void RefreshFields()
    {
        if (selectedNPC == null)
            return;

        nameField.SetValueWithoutNotify(selectedNPC.Name);
        healthField.SetValueWithoutNotify(selectedNPC.Health);
        maxHealthField.SetValueWithoutNotify(selectedNPC.MaxHealth);
        healthSlider.highValue = selectedNPC.MaxHealth;
        healthSlider.SetValueWithoutNotify(selectedNPC.Health);

    }

    private void OnInspectorUpdate()
    {
        if (selectedNPC != null)
        {
            healthField.SetValueWithoutNotify(selectedNPC.Health);
            maxHealthField.SetValueWithoutNotify(selectedNPC.MaxHealth);
            nameField.SetValueWithoutNotify(selectedNPC.Name);
            healthSlider.highValue = selectedNPC.MaxHealth;
            healthSlider.SetValueWithoutNotify(selectedNPC.Health);
        }
    }
}
