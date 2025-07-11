using UnityEngine;

[CreateAssetMenu(fileName = "AnimalCardData", menuName = "Quiz/Animal Card Data")]
public class AnimalCardData : ScriptableObject
{
    public AnimalInformation.Name animalName;
    public Sprite animalImage;
    [TextArea(3, 5)] public string description;

    public AnimalInformation.FlightType flightType;
    public AnimalInformation.InsectType insectType;
    public AnimalInformation.DietType dietType;
    public AnimalInformation.SocialType socialType;
    public AnimalInformation.ReproductionType reproductionType;
}
