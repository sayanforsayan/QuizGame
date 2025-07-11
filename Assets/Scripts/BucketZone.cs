using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
public class BucketZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private AnimalInformation.Color type;
    [SerializeField] private TextMeshProUGUI typeText;

    public void SetType(string type)
    {
        if (typeText != null)
            typeText.text = "" + type;
    }

    public void OnDrop(PointerEventData eventData)
    {
        CardUI card = eventData.pointerDrag.GetComponent<CardUI>();
        if (card != null)
        {
            QuizManager.Instance.MatchCard(card, typeText.text.ToString());
            transform.DOScale(Vector3.one * 0.8f, 0.3f).OnComplete(() =>
            { transform.DOScale(Vector3.one, 0.3f); });
            Destroy(card.gameObject);
        }
    }
}
