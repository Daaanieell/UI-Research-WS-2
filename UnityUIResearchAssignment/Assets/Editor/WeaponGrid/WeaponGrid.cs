using System.Collections.Generic;
using UnityEngine.UIElements;

public class WeaponGrid
{
    private List<VisualElement> gridItems = new List<VisualElement>();
    public List<VisualElement> FillWeaponGrid()
    {
        gridItems.Clear();
        
        //temporary loop
        for (int i = 0; i < 5; i++)
        {
            var item = new VisualElement();
            item.AddToClassList("grid-item");
            
            var name = new Label("ItemName");
            
            item.Add(name);
            
            gridItems.Add(item);
        }

        return gridItems;
    }
}