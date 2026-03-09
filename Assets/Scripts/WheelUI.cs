using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class WheelUI : MonoBehaviour
{
    [SerializeField]  private GameManager gameManager;

    [SerializeField]
    private Color[] colors =
    {
        Color.blue,
        Color.red,
        Color.green,
        Color.yellow
    };

    [System.Serializable]
    public class SliceData
    {
        public Image image;
        public float startAngle;
        public float endAngle;
    }

    private List<SliceData> slices = new List<SliceData>();

    private float _totalWeight;
    private float radian = -360f;
    private float wheelRadian;

    public TextMeshProUGUI title;
    public TextMeshProUGUI result;

    private void Awake()
    {
        if (gameManager == null)    
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }    
    }

    public void CreateWheel()
    {
        RateOption rate = gameManager.rateOption;

        title.gameObject.SetActive(true);
        title.text = rate.title;
        

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
            Image img = gameManager.fillObject[index].GetComponent<Image>();
            img.fillAmount = rateValue;
            gameManager.fillObject[index].SetActive(true);
            img.color = colors[index % colors.Length];

            gameManager.fillObject[index].transform.rotation = Quaternion.Euler(0, 0, wheelRadian);

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

        gameManager.arrow
        .DORotate(new Vector3(0, 0, randomAngle), 4f, RotateMode.FastBeyond360)
        .SetEase(Ease.OutCubic)
        .OnUpdate(CheckSlice)
        .OnComplete(GetResult); ;
    }


    void CheckSlice()
    {
        RateOption rate = gameManager.rateOption;

        float angle = -gameManager.arrow.eulerAngles.z;

        for (int i = 0; i < slices.Count; i++)
        {
            var slice = slices[i];

            if (angle >= slice.startAngle && angle < slice.endAngle)
            {
                slice.image.transform.DOScale(1.1f, 0.1f);
                slice.image.DOFade(0.5f, 0.1f);

                result.text = rate.items[i].optionName;
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
        float angle = -gameManager.arrow.eulerAngles.z;

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
