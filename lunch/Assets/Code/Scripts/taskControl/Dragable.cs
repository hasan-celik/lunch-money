using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public RectTransform parentRectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //parentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Görünürlüğü azalt
        canvasGroup.blocksRaycasts = false; // Diğer etkileşimlere izin ver
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = rectTransform.anchoredPosition + eventData.delta;

        // Yeni pozisyonu kontrol et
        if (IsWithinBounds(newPosition))
        {
            rectTransform.anchoredPosition = newPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Görünürlüğü geri getir
        canvasGroup.blocksRaycasts = true; // Diğer etkileşimleri tekrar aktif et
    }

    private bool IsWithinBounds(Vector2 position)
    {
        // Panel sınırlarını al
        Vector2 minBound = parentRectTransform.rect.min;
        Vector2 maxBound = parentRectTransform.rect.max;

        // Pozisyonun sınırlar içinde olup olmadığını kontrol et
        return position.x >= minBound.x && position.x <= maxBound.x &&
               position.y >= minBound.y && position.y <= maxBound.y;
    }
}
