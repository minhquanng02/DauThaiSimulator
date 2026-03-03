using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
  

    [SerializeField] private RectTransform panel;
    [SerializeField] private RectTransform header;
    [SerializeField] private RectTransform pauseBtn;

    [SerializeField] private float panelStart = -80f;
    [SerializeField] private float headetStart = 40f;
    [SerializeField] private float pauseBtnStart = 35f;

    [SerializeField] private float duration = 1f;

    [SerializeField] private float panelPosY; 
    [SerializeField] private float headerPosY;
    [SerializeField] private float pauseBtnPosX;

    private void Awake()
    {
       
    }

    private void Start()
    {
         panelPosY = panel.anchoredPosition.y;
         headerPosY = header.anchoredPosition.y;
        pauseBtnPosX = pauseBtn.anchoredPosition.x;

        panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, panelStart);
        header.anchoredPosition = new Vector2(header.anchoredPosition.x, headetStart);
        pauseBtn.anchoredPosition = new Vector2(pauseBtnStart, pauseBtn.anchoredPosition.y);

    }

    public void OnPressPlay()
    {

        panel.DOAnchorPosY(panelPosY, duration).SetEase(Ease.InOutCubic);
        header.DOAnchorPosY(headerPosY, duration).SetEase(Ease.InOutCubic);
        pauseBtn.DOAnchorPosX(pauseBtnPosX, duration).SetEase(Ease.InOutCubic);
    }
}
