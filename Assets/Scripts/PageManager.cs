using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PageManager : MonoBehaviour
{
    [System.Serializable]
    public class Page
    {
        public string id;              // Уникальный идентификатор страницы (например: "Main", "Settings", "Help")
        public RectTransform panel;    // Панель (UI-объект) этой страницы
    }

    [Header("Ссылки:")]
    [SerializeField] private RectTransform canvasRect; // Ссылка на Canvas (устанавливается через инспектор)

    [Header("Страницы")]
    [SerializeField] private List<Page> pages = new List<Page>();

    [Header("Перелистывание страниц:")]
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private Ease transitionEase = Ease.InOutQuad;

    private Page currentPage;
    private float panelWidth;

    private void Start()
    {
        InitializeFirstPage();
    }

    /// <summary>
    /// Активирует первую страницу и скрывает остальные.
    /// </summary>
    private void InitializeFirstPage()
    {
        if (pages.Count == 0)
        {
            Debug.LogError("No pages assigned in PageManager!");
            return;
        }

        currentPage = pages[0];

        foreach (var page in pages)
            page.panel.gameObject.SetActive(page == currentPage);
    }

    /// <summary>
    /// Переключает страницу с анимацией. 
    /// Direction: 1 — движение вправо, -1 — движение влево.
    /// </summary>
    private void ShowPage(string pageId, int direction = 1)
    {
        panelWidth = canvasRect.rect.width; // получение ширины экрана

        if (pageId == currentPage.id)
            return;

        Page nextPage = pages.Find(p => p.id == pageId);
        if (nextPage == null)
        {
            Debug.LogError($"Page '{pageId}' not found!");
            return;
        }

        var previousPage = currentPage;

        // Анимация ухода текущей страницы
        previousPage.panel.DOAnchorPos(new Vector2(-panelWidth * direction, 0), transitionDuration)
            .SetEase(transitionEase)
            .OnComplete(() => previousPage.panel.gameObject.SetActive(false));

        // Анимация появления новой страницы
        nextPage.panel.anchoredPosition = new Vector2(panelWidth * direction, 0);
        nextPage.panel.gameObject.SetActive(true);
        nextPage.panel.DOAnchorPos(Vector2.zero, transitionDuration).SetEase(transitionEase);

        currentPage = nextPage;
    }

    /// <summary>
    /// Вызывается кнопкой в инспекторе для анимации влево.
    /// </summary>
    public void ShowPageLeft(string pageId)
    {
        ShowPage(pageId, -1);
    }

    /// <summary>
    /// Вызывается кнопкой в инспекторе для анимации вправо.
    /// </summary>
    public void ShowPageRight(string pageId)
    {
        ShowPage(pageId, 1);
    }
}
