using UnityEngine;
using TMPro;

/// <summary>
/// Представляет один UI-элемент списка конвертера величин.
/// Отвечает за отображение названия единицы измерения и ввод её значения пользователем.
/// </summary>
public class ValueUnitUI : MonoBehaviour
{
    [Header("UI Компоненты:")]
    public TMP_InputField inputField;
    public TMP_Text label;

    public void Initialize(string name)
    {
        label.text = name;
    }
}
