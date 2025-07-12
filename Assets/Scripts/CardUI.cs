using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

// Handles UI behavior for animal cards, including drag-and-drop and click interactions
public class CardUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Image component to display the animal's sprite
    [SerializeField] private Image animalImage;

    // Text component to display the animal's name
    [SerializeField] private TextMeshProUGUI nameText;

    // Reference to the associated animal data
    public AnimalCardData Data { get; private set; }

    // CanvasGroup used to manage raycast blocking during drag
    private CanvasGroup canvasGroup;

    // Stores the original position before dragging starts
    private Vector3 originalPosition;

    // Stores the original parent transform before dragging starts
    private Transform parentToReturnTo;

    // Cached RectTransform for position calculations
    private RectTransform rectTransform;

    // Reference to the canvas containing this card
    private Canvas canvas;

    // Initializes component references
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = transform.GetComponentInParent<Canvas>();
    }

    // Sets up the card's visual elements using provided data
    public void Setup(AnimalCardData newData)
    {
        Data = newData;
        nameText.text = Data.animalName.ToString();
        animalImage.sprite = Data.animalImage;
    }

    // Called when the card is clicked to show its description
    public void OnPointerClick(PointerEventData eventData)
    {
        QuizManager.Instance.ShowDescription(Data);
    }

    // Called when dragging begins, prepares the card for dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        canvasGroup.blocksRaycasts = false;
        parentToReturnTo = transform.parent;
        transform.SetParent(transform.root);  // move to top layer
    }

    // Called while dragging, updates card position based on pointer
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, canvas.worldCamera, out worldPosition);
        rectTransform.position = worldPosition;
    }

    // Called when dragging ends, resets the card's parent and position
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parentToReturnTo);
        transform.position = originalPosition;
    }
}
