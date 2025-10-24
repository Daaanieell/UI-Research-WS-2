using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponSelector : EditorWindow
{
    [SerializeField]
    VisualTreeAsset selector;
    DropdownField typeDropdown;
    ScrollView weaponGrid; 
    List<Weapon> allWeapons = new List<Weapon>();
    

    [MenuItem("Tools/Weapon Selector")]
    public static void ShowWindow()
    {
        var wnd = GetWindow<WeaponSelector>();
        wnd.titleContent = new GUIContent("Weapon Selector");
    }

    public void CreateGUI()
    {
        selector.CloneTree(rootVisualElement);
        typeDropdown = rootVisualElement.Q<DropdownField>("WeaponDropDown");
        weaponGrid = rootVisualElement.Q<ScrollView>("weaponGrid");
        
        typeDropdown.choices = new List<string> { "All", "Melee", "Ranged", "Magic" };
        typeDropdown.value = "All";
        
        LoadAllWeapons();
        RefreshWeaponGrid();
    }
    void LoadAllWeapons()
    {
        string[] guids = AssetDatabase.FindAssets("t:Weapon");
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Weapon prefab = AssetDatabase.LoadAssetAtPath<Weapon>(path);
            allWeapons.Add(prefab);
            Debug.Log(prefab);
        }
    }
    void RefreshWeaponGrid()
    {
        string selectedWeaponType = typeDropdown.value;
        Debug.Log(typeDropdown.value);
        weaponGrid.Clear();
        foreach (var weapon in allWeapons)
        {
            if (selectedWeaponType != "All" && weapon.weaponType.ToString() != selectedWeaponType )
                continue;
            
            var weaponContainer = new VisualElement();
            weaponContainer.style.flexDirection = FlexDirection.Row;
            weaponContainer.style.marginBottom = 4;
            weaponContainer.style.alignItems = Align.Center;

            // Get preview texture
            Texture2D preview = AssetPreview.GetAssetPreview(weapon.prefab);
            if (preview == null)
            {
                preview = AssetPreview.GetMiniThumbnail(weapon);
            }
            
            var image = new Image();
            image.image = preview;
            image.style.width = 64;
            image.style.height = 64;
            image.style.marginRight = 8;
            
            // Add a button for selecting the weapon
            var button = new Button(() => SelectWeapon(weapon.prefab))
            { 
                text = weapon.name
            };
            button.style.flexGrow = 1;
            
            // Add both to the container
            weaponContainer.Add(image);
            weaponContainer.Add(button);

            // Add the container to the grid
            weaponGrid.Add(weaponContainer);
        }
    }
    
    void SelectWeapon(GameObject weapon)
    {
        // This pings the prefab in the Project window
        EditorGUIUtility.PingObject(weapon);
        Debug.Log($"Selected weapon: {weapon.name}");
    }
}
