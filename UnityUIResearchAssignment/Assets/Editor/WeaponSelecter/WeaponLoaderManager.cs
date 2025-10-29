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
        
    public  void SelectWeapon(GameObject weapon)
    {
        // This pings the prefab in the Project window
        EditorGUIUtility.PingObject(weapon);
        Debug.Log($"Selected weapon: {weapon.name}");
    }

    public void LoadWeaponImage(Weapon currentWeapon, VisualElement item)
    {
        Texture2D preview = AssetPreview.GetAssetPreview(currentWeapon.weaponPrefab);
        if (preview == null)
        {
            preview = AssetPreview.GetMiniThumbnail(currentWeapon.weaponPrefab);
        }

        var image = new Image();
        image.image = preview;

        VisualElement name = new Label("" + currentWeapon.weaponName);
        item.Add(image);
        item.Add(name);
    }
}
