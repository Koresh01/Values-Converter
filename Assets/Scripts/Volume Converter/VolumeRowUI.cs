using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI-строка с названием и полем ввода единицы объёма.
/// </summary>
public class VolumeRowUI : MonoBehaviour
{
    [Header("UI Компоненты:")]
    public TMP_InputField inputField;
    public TMP_Text label;

    [Header("Настройки:")]
    public VolumeUnit unitType;

    private VolumeConverter converter;

    private void Start()
    {
        // Ищем конвертер в родителе (например, на Canvas)
        converter = FindFirstObjectByType<VolumeConverter>();

        // Подписываемся на изменение текста
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        converter.OnVolumeChanged(this, newValue);
    }
}
