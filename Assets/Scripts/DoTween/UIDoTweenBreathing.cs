using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class UIDoTweenBreathing : MonoBehaviour
{
    [Header("Настройки анимации")]
    [Tooltip("Множитель увеличения (1 = без изменений, 1.1 = увеличение на 10%)")]
    [Range(1f, 2f)] public float scaleMultiplier = 1.1f;

    [Tooltip("Время полного цикла (увеличение + уменьшение) в секундах")]
    public float duration = 1.5f;

    [Tooltip("Тип плавности анимации")]
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
        // На всякий случай убиваем старую анимацию
        breathingTween?.Kill();

        // Сохраняем исходный масштаб
        Vector3 originalScale = rectTransform.localScale;

        // Создаём анимацию “вдох-выдох”
        breathingTween = rectTransform.DOScale(originalScale * scaleMultiplier, duration / 2f)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo); // бесконечно туда-сюда
    }

    private void StopBreathing()
    {
        breathingTween?.Kill();
        breathingTween = null;
    }
}
