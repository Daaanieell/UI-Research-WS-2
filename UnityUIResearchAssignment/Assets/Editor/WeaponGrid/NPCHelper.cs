using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
public class NPCHelper
{
    private NPC selectedNPC;
    private ObjectField npcField;
    public void SetSelectedNPC(NPC npc)
    {
        selectedNPC = npc;
    }
    public NPC GetSelectedNPC(NPC npc)
    {
        return selectedNPC;
    }

    public void EquipWeapon(Weapon currentWeapon)
    {
        selectedNPC.SetWeaponPrefab(currentWeapon.weaponPrefab);
        selectedNPC.RefreshWeapon();
    }

    public VisualElement NPCObjField()
    {
        npcField = new ObjectField("Select NPC") { objectType = typeof(NPC) };
        npcField.RegisterValueChangedCallback(evt =>
        {
            SetSelectedNPC(evt.newValue as NPC);
        });
       
        return npcField;
    }
}
