using UnityEngine;
using UnityEngine.UIElements;

public class DragFunction : PointerManipulator
{
    private Vector3 originalPosition;
    private VisualElement root;

    public DragFunction(VisualElement target)
    {
        this.target = target;
        root = target.parent;
        originalPosition = target.transform.position;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(StartDrag);
        target.RegisterCallback<PointerMoveEvent>(OnDrag);
        target.RegisterCallback<PointerUpEvent>(StopDrag);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(StartDrag);
        target.UnregisterCallback<PointerMoveEvent>(OnDrag);
        target.UnregisterCallback<PointerUpEvent>(StopDrag);
    }

    private void OnDrag(PointerMoveEvent evt)
    {
        Debug.Log("ondrag");
        if (!target.HasPointerCapture(evt.pointerId))
            return;
        target.transform.position = evt.position;
    }

    private void StartDrag(PointerDownEvent evt)
    {
        Debug.Log("startdrag");
        target.CapturePointer(evt.pointerId);
    }

    private void StopDrag(PointerUpEvent evt)
    {
        Debug.Log("stopdrag");
        if (target.HasPointerCapture(evt.pointerId))
            target.ReleasePointer(evt.pointerId);
        target.transform.position = originalPosition;
    }
}