using System.Collections.Generic;
using UnityEngine.UIElements;

public class WeaponGrid
{
    private List<VisualElement> gridItems = new List<VisualElement>();

    private int maxRows = 5;
    private int maxColumns = 3;

    private int count;

    public VisualElement FillWeaponGrid()
    {
        gridItems.Clear();

        var gridContainer = new VisualElement();
        gridContainer.AddToClassList("weapon-grid");

        for (int row = 0; row < maxRows; row++)
        {
            var rowElement = new VisualElement();
            rowElement.AddToClassList("grid-row");

            for (int col = 0; col < maxColumns; col++)
            {
                count++;
                var item = new VisualElement();
                item.AddToClassList("grid-item");

                var name = new Label("itemName: " + count.ToString());
                item.Add(name);

                rowElement.Add(item);
                gridItems.Add(item);
            }
            gridContainer.Add(rowElement);
        }
        return gridContainer;
    }
}