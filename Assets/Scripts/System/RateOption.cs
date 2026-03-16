using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using static CharacterData;


[CreateAssetMenu(fileName = "RateOption", menuName = "Scriptable Objects/Rate Option")]
public class RateOption : ScriptableObject
{
    public string title;

    [SerializeField] public List<RewardItem> items;
}
    

[System.Serializable]
public class RewardItem
{
    public string optionName;
    public float weight;
    public List<Stat> stats;
    
}

[System.Serializable] public class Stat
{
    public StatType stat;
    public int value;
}


