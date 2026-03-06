using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;


[CreateAssetMenu(fileName = "RateOption", menuName = "Scriptable Objects/Rate Option")]
public class RateOption : ScriptableObject
{
    [SerializeField] public List<RewardItem> _items;
}

[System.Serializable]
public class RewardItem
{
    public string itemName;
    public float weight;
}
