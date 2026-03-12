using UnityEngine;

[CreateAssetMenu(fileName = "AgeEvent", menuName = "Scriptable Objects/AgeEvent")]
public class AgeEvent : ScriptableObject
{
    public int age;
    public RateOption rateOption;
}
