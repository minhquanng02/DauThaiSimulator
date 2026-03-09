using DG.Tweening;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public WheelUI wheelUI;
    public RateOption rateOption;
    public GameObject optionPanel;

    
    [SerializeField] public List<GameObject> fillObject;

    [SerializeField] public RectTransform arrow;


    public void OnClick()
    {
        optionPanel.SetActive(true);
        wheelUI.CreateWheel();
        wheelUI.SpinArrow();
    }




}
