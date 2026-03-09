using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
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
    private float maxduration = 0.5f;
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

        panel.DOScale(1f, maxduration)
            .SetEase(Ease.OutBack);
        cg.DOFade(1f, maxduration);
    }

    public void HidePanel(RectTransform panel)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();

        panel.DOScale(0f, maxduration)
            .SetEase(Ease.InBack);
        cg.DOFade(0f, maxduration)
            .OnComplete(() =>
            {
                panel.gameObject.SetActive(false);
            });
    }

    public void ShowMidpanel()
    {
        Sequence seq = DOTween.Sequence();

        seq.Join(corePanel.DOAnchorPosY(panelPosY, maxduration).SetEase(Ease.InOutCubic));
        seq.Join(pauseBtn.DOAnchorPosX(pauseBtnPosX, maxduration).SetEase(Ease.InOutCubic));

        seq.AppendCallback(() =>
        {
            gameManager.NewStat();
            NewStatUI();
        });
    }

    void NewStatUI()
    {
        ScaleStat(characterDataUI.age.transform);
    }

    public void ScaleStat(Transform trans)
    {
        trans.DOKill();
        Sequence seq = DOTween.Sequence();
        seq.Append(trans.DOScale(1.5f, minDuration));
        seq.Append(trans.DOScale(1f, maxduration));
    }
}
