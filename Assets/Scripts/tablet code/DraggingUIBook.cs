using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public RectTransform rectTransform;
    public Canvas canvas;
    private Vector2 offset;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Bring this object to front if needed
        rectTransform.SetAsLastSibling();

        // Calculate offset so it doesn’t snap when you click
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            rectTransform.localPosition = localPoint - offset;
        }
    }
}