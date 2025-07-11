using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image animalImage;
    [SerializeField] private TextMeshProUGUI nameText;
    public AnimalCardData Data { get; private set; }

    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Transform parentToReturnTo;
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = transform.GetComponentInParent<Canvas>();
    }

    public void Setup(AnimalCardData newData)
    {
        Data = newData;
        nameText.text = Data.animalName.ToString();
        animalImage.sprite = Data.animalImage;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        QuizManager.Instance.ShowDescription(Data);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        canvasGroup.blocksRaycasts = false;
        parentToReturnTo = transform.parent;
        transform.SetParent(transform.root);  // move to top layer
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, canvas.worldCamera, out worldPosition);
        rectTransform.position = worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parentToReturnTo);
        transform.position = originalPosition;
    }
}
