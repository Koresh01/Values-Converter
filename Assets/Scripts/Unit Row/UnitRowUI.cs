using UnityEngine;
using TMPro;
using UnityEditor.SearchService;

/// <summary>
/// UI-строка с названием и полем ввода единицы.
/// </summary>
public class UnitRowUI : MonoBehaviour
{
    [Header("UI Компоненты:")]
    public TMP_InputField inputField;
    public TMP_Text label;

    [Header("Настройки:")]
    public TemperatureUnit unitType;

    private TemperatureConverter converter;

    private void Start()
    {
        // Ищем конвертер в родителе (например, на Canvas)
        converter = FindFirstObjectByType<TemperatureConverter>();

        // Подписываемся на изменение текста
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        // Чистим подписку (важно!)
        inputField.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        // Просто передаём событие конвертеру
        converter?.OnTemperatureChanged(this, newValue);
    }
}
