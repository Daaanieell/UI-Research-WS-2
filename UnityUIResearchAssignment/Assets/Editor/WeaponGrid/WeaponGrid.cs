using UnityEngine.UIElements;

public class WeaponGrid
{
    public VisualElement FillWeaponGrid()
    {
        var gridItem = new VisualElement();
        gridItem.AddToClassList("grid-item");
        return gridItem;
    }
}