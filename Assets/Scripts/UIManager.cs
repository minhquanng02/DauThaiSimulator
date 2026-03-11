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
    private float maxDuration = 0.15f;
    private float minDuration = 0.2f;

    [Header("Core UI")]
    [SerializeField] private RectTransform corePanel;
    [SerializeField] private RectTransform pauseBtn;
    private float panelPosY = 220f; 
    private float pauseBtnPosX = -96f;

    [Header("Class")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CharacterDataUI characterDataUI;

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

    public void ShowMidpanel()
    {
        Sequence seq = DOTween.Sequence();

        seq.Join(corePanel.DOAnchorPosY(panelPosY, maxDuration).SetEase(Ease.InOutCubic));
        seq.Join(pauseBtn.DOAnchorPosX(pauseBtnPosX, maxDuration).SetEase(Ease.InOutCubic));

        seq.AppendCallback(() =>
        {
            gameManager.NewStat();
        });
    }

    public void ScaleStat(Transform trans)
    {
        trans.DOKill();
        Sequence seq = DOTween.Sequence();
        seq.Append(trans.DOScale(1.2f, maxDuration));
        seq.Append(trans.DOScale(1f, minDuration));
    }

    public void ChangeValueUI(int numBe, int numThen,TextMeshProUGUI textM)
    {
        DOVirtual.Int(numBe, numThen, 1f, value =>
        {
            textM.text = value.ToString();
            ScaleStat(textM.transform);
        });


        
    }
}
