using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;
    [Header("#--- Card Properties ---#")]
    [SerializeField] private List<AnimalCardData> allCards;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject wrongCard;
    [SerializeField] private Transform cardHolder;

    [SerializeField] private TextMeshProUGUI attributeText;

    [Header("#--- Animal Information ---#")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI popupName, popupDescription;
    public Image popupImage;

    [Header("#--- Result Screen ---#")]
    [SerializeField] private GameObject finishScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform wrongCardHolder;
    [Header("#--- Bucket ---#")]
    [SerializeField] private BucketZone blueBucket;
    [SerializeField] private BucketZone redBucket;
    [Header("#--- Button ----#")]
    [SerializeField] private Button resetBtn;
    private int totalCount = 0, correctCount;
    private Dictionary<string, Sprite> wrongCards = new();


    private void Awake() => Instance = this;

    void Start()
    {
        SetupQuiz();
        resetBtn.onClick.AddListener(ReloadCurrentScene);
    }

    void SetupQuiz()
    {
        RandomizeAttribute();
        SpawnCards();
        RandomlyDecide();
    }

    void RandomizeAttribute()
    {
        attributeText.text = $"Sort the animals in Red or Blue buckets";
    }

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

    public void ShowDescription(AnimalCardData data)
    {
        popupPanel.SetActive(true);
        popupName.text = data.animalName.ToString();
        popupDescription.text = data.description;
        popupImage.sprite = data.animalImage;
    }

    private void RandomlyDecide()
    {
        int random = UnityEngine.Random.Range(0, 5);

        string typeA = "";
        string typeB = "";

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
        blueBucket.SetType(typeA);
        redBucket.SetType(typeB);
        /* // Randomly assign which bucket gets which type
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

    public void MatchCard(CardUI card, string boxCategory)
    {
        if (card.Data.flightType.ToString() == boxCategory || card.Data.insectType.ToString() == boxCategory || card.Data.dietType.ToString() == boxCategory || card.Data.socialType.ToString() == boxCategory || card.Data.reproductionType.ToString() == boxCategory)
            correctCount++;
        else
            wrongCards.Add(card.Data.name, card.Data.animalImage);

        totalCount++;
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

    private void EvaluateScore()
    {
        float percentage = (totalCount > 0) ? (correctCount * 100f) / totalCount : 0f;
        string message = "";

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

    private void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
