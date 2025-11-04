
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

    // STEP 5.3
    // TODO: Add dropdown variable

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

        // STEP 5.4
        // Create a WeaponGrid instance

        VisualElement root = rootVisualElement;

        VisualElement uxmlContent = m_VisualTreeAsset.Instantiate();
        root.Add(uxmlContent);

        // STEP 5.3
        // TODO: Follow bullet point 3 here

        var grid = uxmlContent.Q<ScrollView>("grid");

        var weaponSelectorContainer = uxmlContent.Q<VisualElement>("Weapon Selector");

        // STEP 5.2
        // TODO: Follow bullet point 2 here

        // ---------------------- NPC editor down here ---------------------- 
        #region

        // STEP 5.3
        // TODO: Follow bullet point 4 here
        // TODO: Follow bullet point 5 here
        // TODO: Follow bullet point 6 here

        // STEP 5.5
        // TODO: Add fields for:
        // - Name
        // - Health
        // - Max Health

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