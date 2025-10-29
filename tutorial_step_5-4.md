Step 5.4. Selecting NPCs

Requirements: NPCHelper.cs

The field itself is created and managed by the helper class NPCHelper, which keeps your editor code clean and modular

Open WeaponGridMain.cs and locate the CreateGUI() method

Once your npcHelper instance has been initialized, add the following code:

ObjectField npcField = npcHelper.NPCObjField();
        npcField.RegisterValueChangedCallback(evt =>
        {
            selectedNPC = evt.newValue as NPC;

            RefreshFields();
        });
        root.Add(npcField);

When you open your editor window (Tools -> WeaponGridMain) you’ll now see a field labeled “Select NPC” at the top.

Drag and drop or choose an NPC GameObject from your scene into this field.
