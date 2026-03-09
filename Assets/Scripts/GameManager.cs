using DG.Tweening;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [Header("Class")]
    [SerializeField] public RateOption rateOption;
    [SerializeField] private WheelUI wheelUI;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CharacterDataUI characterDataUI;

    public void OnClick()
    {
        optionPanel.SetActive(true);
        wheelUI.CreateWheel();
        wheelUI.SpinArrow();
    }


    public void GameStart()
    {
        uIManager.ShowPanel(uIManager.agePanel);
        wheelUI.CreateAge();
    }

    public void NewStat()
    {
        characterDataUI.age.text = characterData.age.ToString();
        
    }
}
