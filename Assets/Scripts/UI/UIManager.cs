using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [Header("Wheel Color")]
    [SerializeField] public Color[] colors =
    {
        Color.blue,
        Color.red,
        Color.green,
        Color.yellow
    };

    [Header("Panel Text")]
    public TextMeshProUGUI titleOption;
    public TextMeshProUGUI resultOption;
    public TextMeshProUGUI ageResult;
    [SerializeField] public RectTransform optionPanel;
    [SerializeField] public RectTransform agePanel;

    [Header("Wheel Object")]
    [SerializeField] public List<GameObject> fillObject;
    [SerializeField] public RectTransform arrow;

    [Header("Value")]
    private float maxDuration = 0.2f;
    private float minDuration = 0.151f;

    [Header("Core UI")]
    [SerializeField] private RectTransform corePanel;
    [SerializeField] private RectTransform pauseBtn;
    [SerializeField] public GameObject loadScene;

    private float endPanelPosY = 220f; 
    private float endPauseBtnPosX = -96f; 
    private float startPanelPosY = -420f;
    private float startPauseBtnPosX = 120f;

    [Header("Class")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CharacterDataUI characterDataUI;


    // Mo panel chinh
    public void ShowMidpanel()
    {
        corePanel.DOAnchorPosY(endPanelPosY, maxDuration).SetEase(Ease.InOutCubic);
        pauseBtn.DOAnchorPosX(endPauseBtnPosX, maxDuration).SetEase(Ease.InOutCubic);
    }

    // An panel chinh
    public void HideMidPanel()
    {
        corePanel.anchoredPosition = new Vector2(corePanel.anchoredPosition.x, startPanelPosY);
        pauseBtn.anchoredPosition = new Vector2(startPauseBtnPosX, pauseBtn.anchoredPosition.y);
    }

    // Hieu ung text khi thay doi
    public void ChangeValueUI(int numBe, int numThen,TextMeshProUGUI textM) 
    {
        DOVirtual.Int(numBe, numThen, 1f, value =>
        {
            textM.text = value.ToString();

            textM.transform.DOKill();
            Sequence seq = DOTween.Sequence();
            seq.Append(textM.transform.DOScale(1.2f, maxDuration));
            seq.Append(textM.transform.DOScale(1f, minDuration));
        });   
    }

    // Hieu ung mo panel
    public void ShowPanel(RectTransform panel) 
    {
        panel.gameObject.SetActive(true);

        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        panel.localScale = Vector3.zero;
        cg.alpha = 0;

        panel.DOScale(1f, maxDuration)
            .SetEase(Ease.OutBack);
        cg.DOFade(1f, maxDuration);
    }

    // Hieu ung tat panel
    public void HidePanel(RectTransform panel) 
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();

        panel.DOScale(0f, maxDuration)
            .SetEase(Ease.InBack);
        cg.DOFade(0f, maxDuration)
            .OnComplete(() =>
            {
                panel.gameObject.SetActive(false);
            });
    }
}
