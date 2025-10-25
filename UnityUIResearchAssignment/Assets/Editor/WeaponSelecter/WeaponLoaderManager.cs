using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//TODO: This class have too much responsibility's 
// It is ideal to separate loading,selecting and building the grid
// Selecting should be handled with player-Input and adding placement position 
// should also be implemented. 
public class WeaponLoaderManager
{
    
   public  void LoadAllWeapons(ref List<Weapon> allWeapons)
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
    public void RefreshWeaponGrid(ref List<Weapon> allWeapons,ref DropdownField typeDropdown,ref ScrollView weaponGrid)
    {
        //TODO: uss file should be loaded instead of styling in the methode
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

            Image image = loadImageForWeapon(weapon);
            // Add a button for selecting the weapon
            var button = new Button(() => SelectWeapon(weapon.weaponPrefab))
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
    
    public  void SelectWeapon(GameObject weapon)
    {
        // This pings the prefab in the Project window
        EditorGUIUtility.PingObject(weapon);
        Debug.Log($"Selected weapon: {weapon.name}");
    }

    public Image loadImageForWeapon( Weapon weapon)
    {
        Texture2D preview = AssetPreview.GetAssetPreview(weapon.weaponPrefab);
        if (preview == null)
        {
            preview = AssetPreview.GetMiniThumbnail(weapon);
        }
            
        var image = new Image();
        image.image = preview;
        image.style.width = 64;
        image.style.height = 64;
        image.style.marginRight = 8;
        
        return image;
    }
}
