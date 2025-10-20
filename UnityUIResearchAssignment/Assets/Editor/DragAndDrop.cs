using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDrop : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/DragAndDrop")]
    public static void ShowExample()
    {
        DragAndDrop wnd = GetWindow<DragAndDrop>();
        wnd.titleContent = new GUIContent("DragAndDrop");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);
        
        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
        
        
        VisualElement draggable = root.Q<VisualElement>("draggable-item");
        draggable.AddManipulator(new DragFunction(draggable));
    }
}
