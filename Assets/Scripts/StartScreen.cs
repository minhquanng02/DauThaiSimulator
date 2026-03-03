using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    
    [SerializeField] private RectTransform button;
    [SerializeField] private RectTransform text;
    [SerializeField] private RectTransform panel;
    [SerializeField] private RectTransform header;

    [SerializeField] private float moveDistance = 210f;
    [SerializeField] private float panelPosY = 70f;
    [SerializeField] private float headerPosY = -33f;
    [SerializeField] private float textPosY = 420f;

    [SerializeField] private float startDuration = 2f;
    [SerializeField] private float panelDuration = 1f;
    [SerializeField] private Button startButton;


   private void Awake()
    {
       
            button = transform.Find("Button").GetComponent<RectTransform>(); 
        if (text == null)
            text = transform.Find("Text").GetComponent<RectTransform>();
        if (panel == null)
            panel = transform.Find("Panel").GetComponent<RectTransform>();

      
    }

  


    public void OnPressPlay() 
    {
        startButton.gameObject.SetActive(false);
        startButton.GetComponent<CanvasGroup>().DOFade(0, 0.25f);
        text.GetComponent<CanvasGroup>().DOFade(0, 0.25f);

        Sequence seq = DOTween.Sequence();

        float insertTime = startDuration * 0.5f;




        seq.Insert(insertTime, panel.DOAnchorPosY(panelPosY, panelDuration).SetEase(Ease.InOutCubic));
        seq.Insert(insertTime, header.DOAnchorPosY(headerPosY, panelDuration).SetEase(Ease.InOutCubic));

    }
}
