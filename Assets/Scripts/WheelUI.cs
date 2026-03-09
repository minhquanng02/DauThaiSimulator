using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.Rendering.DebugUI;


public class WheelUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CharacterData characterData;

    [System.Serializable] private class SliceData
    {
        public Image image;
        public float startAngle;
        public float endAngle;
    }

    private List<SliceData> slices = new List<SliceData>();

    private float _totalWeight;
    private float radian = -360f;
    private float wheelRadian;
    

    private void Awake()
    {
        if (gameManager == null)    
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (uiManager == null)
            uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    

    public void CreateAge()
    {
        int rate = Random.Range(1, 100);
        if (rate <= 1)
            characterData.age = Random.Range(0, 21);
        else if (rate < 20)
            characterData.age = Random.Range(21, 51);
        else if (rate < 90)
            characterData.age = Random.Range(51, 101);
        else
            characterData.age = Random.Range(101, 201);

        DOVirtual.Int(0, characterData.age, 1f, value =>
        {
            uiManager.ageResult.text = value.ToString();
            uiManager.ScaleStat(uiManager.ageResult.transform);
        });
    }

    public void CreateWheel()
    {
        RateOption rate = gameManager.rateOption;

        uiManager.titleOption.gameObject.SetActive(true);
        uiManager.titleOption.text = rate.title;
        

        //xep thu tu va tinh tong
        slices.Clear();


        rate.items = rate.items.OrderByDescending(item => item.weight).ToList();
        _totalWeight = rate.items.Sum(item => item.weight);

        //thong so goc
        int index = 0;
        wheelRadian = 0;

        foreach (var item in rate.items)
        {
            float rateValue = item.weight / _totalWeight;

            //fill amount
            Image img = uiManager.fillObject[index].GetComponent<Image>();
            img.fillAmount = rateValue;
            uiManager.fillObject[index].SetActive(true);
            img.color = uiManager.colors[index % uiManager.colors.Length];

            uiManager.fillObject[index].transform.rotation = Quaternion.Euler(0, 0, wheelRadian);

            float sliceAngle = rateValue * radian; //scale => 360

            slices.Add(new SliceData
            {
                image = img,
                endAngle = wheelRadian,
                startAngle = wheelRadian + sliceAngle
            });

            wheelRadian += sliceAngle;

            index++;
        }
    }

    public void SpinArrow()
    {
        float randomAngle = Random.Range(0f, 3600f);

        uiManager.arrow
        .DORotate(new Vector3(0, 0, randomAngle), 4f, RotateMode.FastBeyond360)
        .SetEase(Ease.OutCubic)
        .OnUpdate(CheckSlice)
        .OnComplete(GetResult); ;
    }


    void CheckSlice()
    {
        RateOption rate = gameManager.rateOption;

        float angle = -uiManager.arrow.eulerAngles.z;

        for (int i = 0; i < slices.Count; i++)
        {
            var slice = slices[i];

            if (angle >= slice.startAngle && angle < slice.endAngle)
            {
                slice.image.transform.DOScale(1.1f, 0.1f);
                slice.image.DOFade(0.5f, 0.1f);

                uiManager.resultOption.text = rate.items[i].optionName;
            }
            else
            {
                slice.image.transform.DOScale(1f, 0.1f);
                slice.image.DOFade(1f, 0.1f);
            }
        }
    }

    void GetResult()
    {
        float angle = -uiManager.arrow.eulerAngles.z;

        foreach (var slice in slices)
        {
            bool inside;

            if (slice.startAngle < slice.endAngle)
            {
                inside = angle >= slice.startAngle && angle < slice.endAngle;
            }
            else
            {
                inside = angle >= slice.startAngle || angle < slice.endAngle;
            }

            if (inside)
            {
                Debug.Log("Result: " + slice.image.name);
                return;
            }
        }
    }

}
