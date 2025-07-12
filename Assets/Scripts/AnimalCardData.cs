using UnityEngine;

// Creates a new menu item in Unity's asset creation menu for Animal Card Data
[CreateAssetMenu(fileName = "AnimalCardData", menuName = "Quiz/Animal Card Data")]
public class AnimalCardData : ScriptableObject
{
    // The name of the animal (defined in AnimalInformation.Name enum)
    public AnimalInformation.Name animalName;

    // The image representing the animal
    public Sprite animalImage;

    // A brief description of the animal, editable in a multi-line text area (3 to 5 lines)
    [TextArea(3, 5)] public string description;

    // Specifies if the animal can fly or not (defined in AnimalInformation.FlightType enum)
    public AnimalInformation.FlightType flightType;

    // Specifies if the animal is an insect or not (defined in AnimalInformation.InsectType enum)
    public AnimalInformation.InsectType insectType;

    // Specifies the diet type of the animal (defined in AnimalInformation.DietType enum)
    public AnimalInformation.DietType dietType;

    // Specifies if the animal lives in groups or solo (defined in AnimalInformation.SocialType enum)
    public AnimalInformation.SocialType socialType;

    // Specifies the reproduction type (lays eggs or gives birth) (defined in AnimalInformation.ReproductionType enum)
    public AnimalInformation.ReproductionType reproductionType;
}
