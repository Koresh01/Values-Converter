using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class UIDoTweenBreathing : MonoBehaviour
{
    [Header("��������� ��������")]
    [Tooltip("��������� ���������� (1 = ��� ���������, 1.1 = ���������� �� 10%)")]
    [Range(1f, 2f)] public float scaleMultiplier = 1.1f;

    [Tooltip("����� ������� ����� (���������� + ����������) � ��������")]
    public float duration = 1.5f;

    [Tooltip("��� ��������� ��������")]
    public Ease easeType = Ease.InOutSine;

    private RectTransform rectTransform;
    private Tween breathingTween;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        StartBreathing();
    }

    private void OnDisable()
    {
        StopBreathing();
    }

    private void StartBreathing()
    {
        // �� ������ ������ ������� ������ ��������
        breathingTween?.Kill();

        // ��������� �������� �������
        Vector3 originalScale = rectTransform.localScale;

        // ������ �������� �����-������
        breathingTween = rectTransform.DOScale(originalScale * scaleMultiplier, duration / 2f)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo); // ���������� ����-����
    }

    private void StopBreathing()
    {
        breathingTween?.Kill();
        breathingTween = null;
    }
}
