using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loadScene;
    [SerializeField] private GameObject startScene;

    [SerializeField] private RectTransform panel;
    [SerializeField] private RectTransform header;

    [SerializeField] private float panelPosY = 70f;
    [SerializeField] private float headerPosY = -33f;

    [SerializeField] private float panelDuration = 1f;

    public void OnPressStart()
    {
        startScene.SetActive(false);
    }

    public void OnPressPlay()
    {
        loadScene.SetActive(false);

        panel.DOAnchorPosY(panelPosY, panelDuration).SetEase(Ease.InOutCubic);
        header.DOAnchorPosY(headerPosY, panelDuration).SetEase(Ease.InOutCubic);
    }
}
