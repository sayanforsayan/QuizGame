using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

// Handles drop interactions for the bucket zone in the quiz game
public class BucketZone : MonoBehaviour, IDropHandler
{
    // Defines the color type associated with this bucket zone
    [SerializeField] private AnimalInformation.Color type;

    // UI Text element to display the type name on the bucket
    [SerializeField] private TextMeshProUGUI typeText;

    // Sets the display text for the bucket type
    public void SetType(string type)
    {
        if (typeText != null)
            typeText.text = "" + type;
    }

    // Called when a draggable object is dropped onto this bucket zone
    public void OnDrop(PointerEventData eventData)
    {
        // Try to get the CardUI component from the dropped object
        CardUI card = eventData.pointerDrag.GetComponent<CardUI>();
        if (card != null)
        {
            // Check if the dropped card matches the bucket's type
            QuizManager.Instance.MatchCard(card, typeText.text.ToString());

            // Animate the bucket scaling down and back up for visual feedback
            transform.DOScale(Vector3.one * 0.8f, 0.3f).OnComplete(() =>
            { transform.DOScale(Vector3.one, 0.3f); });

            // Remove the card from the scene after dropping
            Destroy(card.gameObject);
        }
    }
}
