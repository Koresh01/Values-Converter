using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI-строка с названием и полем ввода единицы.
/// </summary>
public class LengthRowUI : MonoBehaviour
{
    [Header("UI Компоненты:")]
    public TMP_InputField inputField;
    public TMP_Text label;

    [Header("Настройки:")]
    public LengthUnit unitType;


    private LengthConverter converter;

    private void Start()
    {
        // Ищем конвертер в родителе (например, на Canvas)
        converter = FindFirstObjectByType<LengthConverter>();

        // Подписываемся на изменение текста
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        converter.OnLengthChanged(this, newValue);
    }
}
