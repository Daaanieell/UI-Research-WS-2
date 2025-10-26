using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
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
    private List<string> weaponNames;

    [MenuItem("Tools/NPC Editor")]
    public static void ShowWindow()
    {
        var window = GetWindow<NPCEditorWindow>();
        window.titleContent = new GUIContent("NPC Editor");
        window.minSize = new Vector2(280, 240);
    }

    public void CreateGUI()
    {
        var root = rootVisualElement;
        root.style.paddingLeft = 10;
        root.style.paddingRight = 10;
        root.style.paddingTop = 10;

        npcField = new ObjectField("Select NPC") { objectType = typeof(NPC) };
        npcField.RegisterValueChangedCallback(evt =>
        {
            selectedNPC = evt.newValue as NPC;
            RefreshFields();
        });
        root.Add(npcField);

        nameField = new TextField("Name");
        nameField.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC == null) return;
            selectedNPC.Name = evt.newValue;
            RefreshHealthBarAndScene();
        });
        root.Add(nameField);

        healthField = new IntegerField("Health");
        healthField.isDelayed = false; // direktes Feedback
        healthField.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC == null) return;
            selectedNPC.Health = Mathf.Clamp(evt.newValue, 0, selectedNPC.MaxHealth);
            healthSlider.SetValueWithoutNotify(selectedNPC.Health);
            RefreshHealthBarAndScene();
        });
        root.Add(healthField);

        maxHealthField = new IntegerField("Max Health");
        maxHealthField.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC == null) return;
            selectedNPC.MaxHealth = Mathf.Max(1, evt.newValue);
            selectedNPC.Health = Mathf.Clamp(selectedNPC.Health, 0, selectedNPC.MaxHealth);

            healthSlider.highValue = selectedNPC.MaxHealth;
            healthSlider.SetValueWithoutNotify(selectedNPC.Health);
            healthField.SetValueWithoutNotify(selectedNPC.Health);

            RefreshHealthBarAndScene();
        });
        root.Add(maxHealthField);

        healthSlider = new SliderInt("Health Slider", 0, 100) { showInputField = true };
        healthSlider.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC == null) return;

            selectedNPC.Health = Mathf.Clamp(evt.newValue, 0, selectedNPC.MaxHealth);
            healthField.SetValueWithoutNotify(selectedNPC.Health);
            RefreshHealthBarAndScene();
        });

        healthSlider.RegisterCallback<PointerMoveEvent>(_ =>
        {
            if (selectedNPC == null) return;
            selectedNPC.Health = healthSlider.value;
            RefreshHealthBarAndScene();
        });
        root.Add(healthSlider);

        LoadWeaponPrefabs();
        weaponPopup = new PopupField<string>("Weapon", weaponNames, weaponNames.Count > 0 ? 0 : -1);
        weaponPopup.RegisterValueChangedCallback(evt =>
        {
            if (selectedNPC == null) return;

            int index = weaponNames.IndexOf(evt.newValue);
            if (index >= 0 && index < weaponPrefabs.Count)
            {
                GameObject prefab = weaponPrefabs[index];
                selectedNPC.SetWeaponPrefab(prefab);
                selectedNPC.RefreshWeapon();
                RefreshHealthBarAndScene();
            }
        });
        root.Add(weaponPopup);

        RefreshFields();
    }

    private void LoadWeaponPrefabs()
    {
        weaponPrefabs = new List<GameObject>();
        weaponNames = new List<string>();

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
