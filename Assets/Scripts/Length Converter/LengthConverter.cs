using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Конвертер длины между всеми единицами.
/// </summary>
public class LengthConverter : MonoBehaviour
{
    public List<LengthRowUI> lengthUIList = new List<LengthRowUI>();
    private bool isUpdating = false; // защита от рекурсии при обновлении полей

    /// <summary>
    /// Вызывается, когда пользователь меняет значение в одном из полей.
    /// </summary>
    public void OnLengthChanged(LengthRowUI sourceUI, string newValue)
    {
        if (isUpdating) return; // уже идёт пересчёт
        if (!double.TryParse(newValue, out double value)) return; // не число — выходим

        LengthUnit fromUnit = sourceUI.unitType;
        double meters = ToMeters(value, fromUnit); // в метры

        isUpdating = true;
        foreach (var ui in lengthUIList)
        {
            if (ui == sourceUI) continue; // пропускаем поле-источник
            double converted = FromMeters(meters, ui.unitType); // пересчёт
            ui.inputField.text = converted.ToString("0.####");  // обновляем текст
        }
        isUpdating = false;
    }

    /// <summary>
    /// Универсальный метод перевода.
    /// </summary>
    public double ConvertLength(double value, LengthUnit from, LengthUnit to)
    {
        return FromMeters(ToMeters(value, from), to);
    }

    /// <summary>
    /// Перевод в метры.
    /// </summary>
    private double ToMeters(double value, LengthUnit from)
    {
        return from switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value * 1000,
            LengthUnit.Centimeter => value / 100,
            LengthUnit.Millimeter => value / 1000,
            LengthUnit.Mile => value * 1609.344,
            LengthUnit.Yard => value * 0.9144,
            LengthUnit.Foot => value * 0.3048,
            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// Перевод из метров.
    /// </summary>
    private double FromMeters(double value, LengthUnit to)
    {
        return to switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value / 1000,
            LengthUnit.Centimeter => value * 100,
            LengthUnit.Millimeter => value * 1000,
            LengthUnit.Mile => value / 1609.344,
            LengthUnit.Yard => value / 0.9144,
            LengthUnit.Foot => value / 0.3048,
            _ => throw new System.NotImplementedException()
        };
    }
}
