using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
public class NPCHelper
{
    private NPC selectedNPC;
    private ObjectField npcField;
    private MainEditor wg;


    public NPCHelper(MainEditor wg)
    {
        this.wg = wg;
    }
    public void SetSelectedNPC(NPC npc)
    {
        selectedNPC = npc;
    }
    public NPC GetSelectedNPC()
    {
        return selectedNPC;
    }

    // STEP 5.5
    // public void EquipWeapon(Weapon currentWeapon)

    public ObjectField NPCObjField()
    {
        npcField = new ObjectField("Select NPC") { objectType = typeof(NPC) };
        npcField.RegisterValueChangedCallback(evt =>
        {
            SetSelectedNPC(evt.newValue as NPC);
            wg.RefreshFields();
        });

        return npcField;
    }
}
