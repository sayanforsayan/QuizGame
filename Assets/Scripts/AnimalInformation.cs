// Holds all enums related to animal attributes used in the quiz game
public class AnimalInformation
{
    // Enum for different animal names
    public enum Name
    {
        Ant, Bat, Beer, Bee, Sparrow, Beetle, Butterfly, Cat, Chicken, ClownFish,
        Cow, Deer, Dog, Dragonfly, Duck, Falcon, Fly, Grasshopper
    }

    // Enum defining if an animal can fly or not
    public enum FlightType
    {
        Flying,
        NonFlying
    }

    // Enum defining if an animal is an insect or not
    public enum InsectType
    {
        Insect,
        NonInsect
    }

    // Enum defining the diet type of an animal
    public enum DietType
    {
        Herbivorous,
        Omnivorous
    }

    // Enum defining if an animal lives in a group or alone
    public enum SocialType
    {
        Group,
        Solo
    }

    // Enum defining the reproduction method of an animal
    public enum ReproductionType
    {
        LayEggs,
        GiveBirth
    }

    // Enum for color types used for bucket zones
    public enum Color
    {
        None, Blue, Red
    }
}
