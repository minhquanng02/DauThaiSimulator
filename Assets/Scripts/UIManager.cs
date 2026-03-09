using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
  

    [SerializeField] private RectTransform panel;
    [SerializeField] private RectTransform pauseBtn;

    //[SerializeField] private float panelStart = -155f;
    //[SerializeField] private float pauseBtnStart = 35f;

    [SerializeField] private float duration = 1f;

    [SerializeField] private float panelPosY; 
    [SerializeField] private float pauseBtnPosX;

    private void Awake()
    {
       
    }

    //private void Start()
    //{
    //     panelPosY = panel.anchoredPosition.y;
    //    pauseBtnPosX = pauseBtn.anchoredPosition.x;

    //    panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, panelStart);
    //    pauseBtn.anchoredPosition = new Vector2(pauseBtnStart, pauseBtn.anchoredPosition.y);

    //}

    //public void OnPressPlay()
    //{

    //    panel.DOAnchorPosY(panelPosY, duration).SetEase(Ease.InOutCubic);
    //    pauseBtn.DOAnchorPosX(pauseBtnPosX, duration).SetEase(Ease.InOutCubic);
    //}
}
