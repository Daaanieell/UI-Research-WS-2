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
    private WeaponLoaderManager _weaponLoaderManager = new WeaponLoaderManager();
        
    

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
        
        typeDropdown.RegisterValueChangedCallback(evt => _weaponLoaderManager.RefreshWeaponGrid(ref allWeapons ,ref typeDropdown,ref weaponGrid));
        _weaponLoaderManager.LoadAllWeapons(ref allWeapons);

    }

}
