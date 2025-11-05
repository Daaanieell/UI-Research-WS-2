using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MyFirstEditorWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/MyFirstEditorWindow")]
    public static void ShowExample()
    {
        MyFirstEditorWindow wnd = GetWindow<MyFirstEditorWindow>();
        wnd.titleContent = new GUIContent("MyFirstEditorWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}
