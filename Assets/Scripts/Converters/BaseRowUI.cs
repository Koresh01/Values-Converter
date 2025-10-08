using UnityEngine;
using TMPro;

public class BaseRowUI<TUnit, TConverter, TRowUI> : MonoBehaviour
    where TConverter : BaseConverter<TUnit, TRowUI>
    where TRowUI : BaseRowUI<TUnit, TConverter, TRowUI>
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
