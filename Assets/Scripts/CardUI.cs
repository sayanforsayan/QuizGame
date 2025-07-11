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

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parentToReturnTo);
        transform.position = originalPosition;
    }
}
