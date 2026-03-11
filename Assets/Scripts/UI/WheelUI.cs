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
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
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
        public int index;
    }

    private List<SliceData> slices = new List<SliceData>();

    private float _totalWeight;
    private float radian = -360f;
    private float wheelRadian;
    
 

    
    //Tao vong quay
    public void CreateWheel(RateOption rate)
    {
        slices.Clear();


        //RateOption rate = gameManager.rateOption;
        

        uiManager.optionPanel.gameObject.SetActive(true);
        uiManager.titleOption.gameObject.SetActive(true);
        uiManager.titleOption.text = rate.title;
        



        rate.items = rate.items.OrderByDescending(item => item.weight).ToList();
        _totalWeight = rate.items.Sum(item => item.weight);

        //thong so goc
        int i = 0;
        wheelRadian = 0;

        foreach (var item in rate.items)
        {
            float rateValue = item.weight / _totalWeight;

            //fill amount
            Image img = uiManager.fillObject[i].GetComponent<Image>();
            img.fillAmount = rateValue;
            uiManager.fillObject[i].SetActive(true);
            img.color = uiManager.colors[i % uiManager.colors.Length];

            uiManager.fillObject[i].transform.rotation = Quaternion.Euler(0, 0, wheelRadian);

            float sliceAngle = rateValue * radian; //scale => 360

            float start = Mathf.Repeat(wheelRadian, -radian);
            float end = Mathf.Repeat(wheelRadian + sliceAngle, -radian);

            slices.Add(new SliceData
            {
                image = img,
                endAngle = wheelRadian,
                startAngle = wheelRadian + sliceAngle,
                index = i
            });

            wheelRadian += sliceAngle;

            i++;
        }
    }

    //Xoay mui ten
    public void SpinArrow()
    {
        float randomAngle = Random.Range(0f, 3600f);

        uiManager.arrow
        .DORotate(new Vector3(0, 0, 3600f + randomAngle), 4f, RotateMode.FastBeyond360)
        .SetEase(Ease.OutCubic)
        .OnUpdate(CheckSlice)
        .OnComplete(GetResult); ;
    }

    //Kiem tra vi tri mui ten
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

    //Bao ket qua khi quay  xong
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
                int index = slice.index;

                RewardItem reward = gameManager.rateOption.items[index];

                uiManager.resultOption.text = reward.optionName;

                Debug.Log("Result: " + reward.optionName);

                gameManager.statSystem.ApplyReward(reward);

                return;
            }
        }

        Debug.LogWarning("No slice found for angle: " + angle);
    }

}
