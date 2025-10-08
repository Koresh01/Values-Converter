using UnityEngine;
using TMPro;

public class BaseRowUI<TUnit, TConverter, TRowUI> : MonoBehaviour   // Я — базовый класс для UI-строки. У меня есть три шаблонных типа: единица измерения, конвертер и сама строка UI.
    where TConverter : BaseConverter<TUnit, TRowUI> // Тип, который ты подставишь вместо TConverter, обязан быть наследником класса BaseConverter<TUnit, TRowUI>
    where TRowUI : BaseRowUI<TUnit, TConverter, TRowUI> // Тип, который ты подставишь как TRowUI, обязан быть наследником самого BaseRowUI, но уже со своими конкретными типами внутри.
{
    [Header("UI Компоненты:")]
    public TMP_InputField inputField;
    public TMP_Text label;

    [Header("Настройки:")]
    public TUnit unitType;

    private TConverter converter;

    private void Start()
    {
        converter = FindFirstObjectByType<TConverter>();
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        converter?.OnValueChanged((TRowUI)this, newValue);
    }
}
