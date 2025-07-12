using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

// Manages the quiz logic, including card spawning, attribute selection, scoring, and UI updates
public class QuizManager : MonoBehaviour
{
    // Singleton instance for easy access
    public static QuizManager Instance;

    [Header("#--- Card Properties ---#")]
    // List of all animal card data used in the quiz
    [SerializeField] private List<AnimalCardData> allCards;
    // Prefab for creating animal cards
    [SerializeField] private GameObject cardPrefab;
    // Prefab for displaying wrong cards in the result screen
    [SerializeField] private GameObject wrongCard;
    // Parent transform to hold spawned cards
    [SerializeField] private Transform cardHolder;

    // UI text to show the current attribute being sorted
    [SerializeField] private TextMeshProUGUI attributeText;

    [Header("#--- Animal Information ---#")]
    // Popup panel showing animal information on click
    [SerializeField] private GameObject popupPanel;
    // UI text for animal name in popup
    [SerializeField] private TextMeshProUGUI popupName, popupDescription;
    // UI image for animal image in popup
    public Image popupImage;

    [Header("#--- Result Screen ---#")]
    // Finish screen UI shown after quiz ends
    [SerializeField] private GameObject finishScreen;
    // UI text displaying the final score and message
    [SerializeField] private TextMeshProUGUI scoreText;
    // Parent transform to hold wrong cards in result screen
    [SerializeField] private Transform wrongCardHolder;

    [Header("#--- Bucket ---#")]
    // Blue bucket zone reference
    [SerializeField] private BucketZone blueBucket;
    // Red bucket zone reference
    [SerializeField] private BucketZone redBucket;

    [Header("#--- Button ----#")]
    // Button to reset and reload the quiz scene
    [SerializeField] private Button resetBtn;

    // Total number of cards processed and correct count
    private int totalCount = 0, correctCount;
    // Dictionary to store wrongly placed cards with their images
    private Dictionary<string, Sprite> wrongCards = new();

    // Sets up the singleton instance
    private void Awake() => Instance = this;

    // Initializes quiz setup and button events
    void Start()
    {
        SetupQuiz();
        resetBtn.onClick.AddListener(ReloadCurrentScene);
    }

    // Prepares the quiz with randomized cards and attributes
    void SetupQuiz()
    {
        RandomizeAttribute();
        SpawnCards();
        RandomlyDecide();
    }

    // Displays sorting instruction text
    void RandomizeAttribute()
    {
        attributeText.text = $"Sort the animals in Red or Blue buckets";
    }

    // Instantiates and displays all animal cards in random order
    void SpawnCards()
    {
        allCards = allCards.OrderBy(x => System.Guid.NewGuid()).ToList();
        foreach (var data in allCards)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardHolder);
            CardUI ui = cardGO.GetComponent<CardUI>();
            ui.Setup(data);
        }
    }

    // Shows the animal's description and image in the popup panel
    public void ShowDescription(AnimalCardData data)
    {
        popupPanel.SetActive(true);
        popupName.text = data.animalName.ToString();
        popupDescription.text = data.description;
        popupImage.sprite = data.animalImage;
    }

    // Randomly selects which attribute will be used for sorting and assigns it to buckets
    private void RandomlyDecide()
    {
        int random = UnityEngine.Random.Range(0, 5);

        string typeA = "";
        string typeB = "";

        // Decide which attribute type to use based on random value
        switch (random)
        {
            case 0:
                typeA = AnimalInformation.FlightType.Flying.ToString();
                typeB = AnimalInformation.FlightType.NonFlying.ToString();
                break;
            case 1:
                typeA = AnimalInformation.InsectType.Insect.ToString();
                typeB = AnimalInformation.InsectType.NonInsect.ToString();
                break;
            case 2:
                typeA = AnimalInformation.DietType.Herbivorous.ToString();
                typeB = AnimalInformation.DietType.Omnivorous.ToString();
                break;
            case 3:
                typeA = AnimalInformation.SocialType.Group.ToString();
                typeB = AnimalInformation.SocialType.Solo.ToString();
                break;
            case 4:
                typeA = AnimalInformation.ReproductionType.GiveBirth.ToString();
                typeB = AnimalInformation.ReproductionType.LayEggs.ToString();
                break;
        }
        // Set bucket labels with selected types
        blueBucket.SetType(typeA);
        redBucket.SetType(typeB);

        /* // Optionally randomize which bucket gets which type
         if (Random.value > 0.5f)
         {
             blueBucket.SetType(typeA);
             redBucket.SetType(typeB);
         }
         else
         {
             blueBucket.SetType(typeB);
             redBucket.SetType(typeA);
         }
         */
    }

    // Checks if the dropped card matches the assigned bucket type and updates score
    public void MatchCard(CardUI card, string boxCategory)
    {
        if (card.Data.flightType.ToString() == boxCategory || card.Data.insectType.ToString() == boxCategory || card.Data.dietType.ToString() == boxCategory || card.Data.socialType.ToString() == boxCategory || card.Data.reproductionType.ToString() == boxCategory)
            correctCount++;
        else
            wrongCards.Add(card.Data.name, card.Data.animalImage);

        totalCount++;
        // If all cards are processed, show result screen
        if (totalCount == allCards.Count)
        {
            EvaluateScore();
            foreach (var item in wrongCards)
            {
                GameObject obj = Instantiate(wrongCard, wrongCardHolder);
                obj.GetComponent<Image>().sprite = item.Value;
                obj.transform.GetChild(0).GetComponent<TMP_Text>().text = "" + item.Key;
            }
            finishScreen.SetActive(true);
        }
    }

    // Calculates the final score percentage and updates result text
    private void EvaluateScore()
    {
        float percentage = (totalCount > 0) ? (correctCount * 100f) / totalCount : 0f;
        string message = "";

        // Set message based on score percentage
        if (percentage == 100f)
        {
            message = "Excellent";
        }
        else if (percentage >= 80f)
        {
            message = "Intelligent";
        }
        else if (percentage >= 50f)
        {
            message = "Intermediate";
        }
        else
        {
            message = "Needs to Study";
        }

        scoreText.text = $"Score: {correctCount}/{totalCount}\n{message}";
    }

    // Reloads the current scene to restart the quiz
    private void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
