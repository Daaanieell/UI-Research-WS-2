> i kept the file seperate in case of merge conflicts

### Step 5.1: Creating your first VisualElement

**Unity documentation:** 
- [Labels](https://docs.unity3d.com/6000.2/Documentation/Manual/UIE-uxml-element-Label.html)

In this step you will be creating your first VisualElement, a title for the "Weapon Selector". Creating a static labels can be done in two ways:
1. Using the UI Builder, best for static elements in your UI
2. Typing your own UXML, best for controlling each element

Using the UI Builder:
- Open the UI Builder by clicking on your .UXML document.
- Using UI Builder window, navigate to the "Library" tab and search for a label.
- Drag the Label to the canvas and give it a name, like "Weapon Selector".

You can change the properties of this label using the "Inspector" window in the UI Builder.

Typing your own UXML:
- Open the .UXML file with an IDE.
- Add: `<ui:Label class="my-title" text="Weapon Selector" />`
- Open the .USS file with an IDE.
- Add your own styling within the .USS file, e.g.: `.my-title { font-size: 15px; color: red; }`


