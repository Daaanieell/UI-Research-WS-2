Step 5.5. Changing weapons and stats

Requirements: NPC.cs, RefreshFields, RefreshHealthBarAndScene

Go to WeaponGridMain.cs and add a new Visuelelement (e.g. NPCEditorContainer)

Now you can start creating the fields for all attributes -> this works exactly as in step 5.4, so just try it out for yourself

-> don't forget that nameField, healthField and maxHealthField is not a ObjectField

-> use Mathf.Max for the healthField and Mathf.Clamp for the maxHealthField

-> after initializing healthField, add “healthField.isDelayed = false;” (this makes it work better).

-> add healthField.SetValueWithoutNotify(selectedNPC.Health); to the end of the maxHealthField

add the your container to root

to change weapons -> create a new method called EquipWeaon in NPCHelper.cs (use the functions of NPC.cs)

now open WeaponGrid.cs and modify the method "GenerateItemElement" -> call the the new created method when an item is clicked
