> i kept the file seperate in case of merge conflicts

### Step 5.1: Creating your first VisualElement

**Unity documentation:** 
- [Labels](https://docs.unity3d.com/6000.2/Documentation/Manual/UIE-uxml-element-Label.html)
- [Get started with UI Builder](https://docs.unity3d.com/6000.2/Documentation/Manual/UIB-getting-started.html)

In this step you will be creating your first VisualElement, a title for the "Weapon Selector". Creating a static labels can be done in two ways:
1. Using the UI Builder, best for static elements in your UI
2. Typing your own UXML, best for controlling each element

#### Using the UI Builder:
- Open the UI Builder by clicking on your .UXML document.
<img width="1919" height="1145" alt="image" src="https://github.com/user-attachments/assets/86926ca6-65b3-41d3-9ec5-b3be8e7cd2ff" />
- Using UI Builder window, navigate to the "Library" tab and search for a label.
- Drag the Label to the canvas and give it a name, like "Weapon Selector".

You can change the properties of this label using the "Inspector" window in the UI Builder.

#### Typing your own UXML:
- Open the .UXML file with an IDE.
- Add: `<ui:Label class="my-title" text="Weapon Selector" />`
- Open the .USS file with an IDE.
- Add your own styling within the .USS file, e.g.: `.my-title { font-size: 15px; color: red; }`

Your end result should look something like this:

<img width="678" height="736" alt="image" src="https://github.com/user-attachments/assets/51d6da6d-c693-4491-aba6-32c09fc776d1" />

  


