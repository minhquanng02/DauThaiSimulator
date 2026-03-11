using UnityEngine;

public class CharacterData : MonoBehaviour 
{
    public string characterID;
    [Header("Top")]
    public int age = 0;
    public string gender;
    public string job;

    [Header("Left")]
    public int health;
    public int appearance;
    public int mirrage;
    public int stress;
    public int discipline;
    public int risk;

    [Header("Right")]
    public int iq;
    public int eq;
    public int finance;
    public int social;
    public int reputation;
    public int debt;

    [Header("Hide")]
    public int luck;
}