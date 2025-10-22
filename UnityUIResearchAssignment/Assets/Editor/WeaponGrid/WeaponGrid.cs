using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponGrid
{
    private int maxRows = 5;
    private int maxColumns = 3;

    private int testAmountOfItems = 32;

    private int count;
    List<int> weaponPlaceholder = new List<int>();
    
    //TODO: needs parameters for List<Weapon>
    public VisualElement FillWeaponGrid()
    {
        //test
        for (int i = 1; i <= testAmountOfItems; i++)
        {
            weaponPlaceholder.Add(i);
        }
        
        VisualElement gridContainer = new VisualElement();
        gridContainer.AddToClassList("weapon-grid");

        for (int row = 0; row < weaponPlaceholder.Count; row++)
        {
            VisualElement rowElement = new VisualElement();
            rowElement.AddToClassList("grid-row");

            for (int col = 0; col < maxColumns; col++)
            {
                count++;
                if (count > weaponPlaceholder.Count) break;
                
                int itemNumber = count;
                VisualElement item = new VisualElement();
                item.AddToClassList("grid-item");
                item.AddManipulator(new Clickable(() =>
                {
                    //TODO: this needs logic for equipping a weapon on an NPC
                    //  maybe returning the weapon SO?
                    Debug.Log("clicked number: " + itemNumber + " placeholder number: " + weaponPlaceholder[itemNumber-1]);

                }));

                VisualElement name = new Label("itemName: " + count.ToString());
                item.Add(name);

                rowElement.Add(item);
            }

            gridContainer.Add(rowElement);
        }

        return gridContainer;
    }
}