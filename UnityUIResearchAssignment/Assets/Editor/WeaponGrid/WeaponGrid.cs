using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponGrid
{
    private int maxRows = 5;
    private int maxColumns = 3;
    private int count;

    public VisualElement FillWeaponGrid(List<Weapon> weapons)
    {
        if (weapons == null)
        {
            Debug.LogWarning("weapons is null");
            return null;
        }

        if (weapons.Count <= 0)
        {
            return ShowNoWeaponMessage();
        }

        VisualElement gridContainer = new VisualElement();
        gridContainer.AddToClassList("weapon-grid");

        for (int row = 0; row < weapons.Count; row++)
        {
            VisualElement rowElement = new VisualElement();
            rowElement.AddToClassList("grid-row");

            for (int col = 0; col < maxColumns; col++)
            {
                count++;
                if (count > weapons.Count) break;
                int itemNumber = count;
                var currentWeapon = weapons[itemNumber - 1];

                VisualElement item = GenerateItemElement(currentWeapon, itemNumber);
                rowElement.Add(item);
            }

            gridContainer.Add(rowElement);
        }

        return gridContainer;
    }

    private VisualElement GenerateItemElement(Weapon currentWeapon, int index)
    {
        VisualElement item = new VisualElement();
        item.AddToClassList("grid-item");


        item.AddManipulator(new Clickable(() =>
        {
            Debug.Log("clicked on: " + currentWeapon.weaponName);
            //TODO: add function here for equipping a weapon!
        }));

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

        return item;
    }

    private VisualElement ShowNoWeaponMessage()
    {
        Debug.Log("no weapons found");
        VisualElement warningContainer = new VisualElement();
        warningContainer.AddToClassList("warning-container");

        VisualElement warning = new Label("No weapons found");
        warning.AddToClassList("no-weapon-warning");

        warningContainer.Add(warning);

        return warningContainer;
    }
}