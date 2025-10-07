using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PageManager : MonoBehaviour
{
    [System.Serializable]
    public class Page
    {
        public string id;              // например "Main", "Length", "Weight"
        public RectTransform panel;    // сам объект страницы
    }

    [Header("Pages")]
    public List<Page> pages = new List<Page>();

    [Header("Transition Settings")]
    public float transitionDuration = 0.5f;
    public Ease transitionEase = Ease.InOutQuad;

    
    private Page currentPage;

    void Start()
    {
        // Находим и показываем первую страницу
        currentPage = pages[0];
        foreach (var p in pages)
            p.panel.gameObject.SetActive(p == currentPage);
    }

    public void ShowPage(string pageId)
    {
        if (pageId == currentPage.id)
            return;

        Page nextPage = pages.Find(p => p.id == pageId);
        if (nextPage == null)
        {
            Debug.LogError($"Page '{pageId}' not found!");
            return;
        }

        // Сохраняем ссылку на старую страницу
        var previousPage = currentPage;

        // Анимация ухода старой страницы
        previousPage.panel.DOAnchorPos(new Vector2(-Screen.width, 0), transitionDuration)
            .SetEase(transitionEase)
            .OnComplete(() => previousPage.panel.gameObject.SetActive(false));

        // Анимация прихода новой
        nextPage.panel.gameObject.SetActive(true);
        nextPage.panel.anchoredPosition = new Vector2(Screen.width, 0);
        nextPage.panel.DOAnchorPos(Vector2.zero, transitionDuration).SetEase(transitionEase);

        // Обновляем текущую страницу после запуска всех анимаций
        currentPage = nextPage;
    }

}
