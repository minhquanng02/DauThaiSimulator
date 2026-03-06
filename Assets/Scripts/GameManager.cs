using DG.Tweening;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public RateOption rateOption;

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

    [SerializeField]
    private List<GameObject> fillObject;

    private float _totalWeight;
    private float radian = -360f;
    private float wheelRadian;

    [SerializeField] RectTransform arrow;

    public void OnClick()
    {
        CreateWheel();
        SpinArrow();
    }


    public void CreateWheel()
    {
        //xep thu tu va tinh tong
        slices.Clear();
        rateOption._items = rateOption._items.OrderByDescending(item => item.weight).ToList();
        _totalWeight = rateOption._items.Sum(item => item.weight);

        //thong so goc
        int index = 0;
        wheelRadian = 0;

        foreach (var item in rateOption._items)
        {
            float rate = item.weight / _totalWeight;

            //fill amount
            Image img = fillObject[index].GetComponent<Image>();
            img.fillAmount = rate;
            fillObject[index].SetActive(true);
            img.color = colors[index % colors.Length];

            fillObject[index].transform.rotation = Quaternion.Euler(0, 0, wheelRadian);

            float sliceAngle = rate * radian; //scale => 360

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

        arrow
        .DORotate(new Vector3(0, 0, randomAngle), 4f, RotateMode.FastBeyond360)
        .SetEase(Ease.OutCubic)
        .OnUpdate(CheckSlice)
        .OnComplete(GetResult); ;
    }

    void CheckSlice()
    {
        float angle =  -arrow.eulerAngles.z;

        foreach (var slice in slices)
        {
            //Debug.Log(slice.startAngle + " " + slice.endAngle + " " + angle);

            if (angle >= slice.startAngle && angle < slice.endAngle)
            {
                slice.image.transform.DOScale(1.1f, 0.1f);
                slice.image.DOFade(0.5f, 0.1f);
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
        float angle = -arrow.eulerAngles.z;

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
