using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Конвертер температуры между всеми единицами.
/// </summary>
public class TemperatureConverter : MonoBehaviour
{
    public List<UnitRowUI> temperatureUIList = new List<UnitRowUI>();
    private bool isUpdating = false; // защита от рекурсии при обновлении полей

    /// <summary>
    /// Вызывается, когда пользователь меняет значение в одном из полей.
    /// </summary>
    public void OnTemperatureChanged(UnitRowUI sourceUI, string newValue)
    {
        if (isUpdating) return; // уже идёт пересчёт
        if (!double.TryParse(newValue, out double value)) return; // не число — выходим

        TemperatureUnit fromUnit = sourceUI.unitType;
        double celsius = ToCelsius(value, fromUnit); // в Цельсий

        isUpdating = true;
        foreach (var ui in temperatureUIList)
        {
            if (ui == sourceUI) continue; // пропускаем поле-источник
            double converted = FromCelsius(celsius, ui.unitType); // пересчёт
            ui.inputField.text = converted.ToString("F2"); // обновляем текст
        }
        isUpdating = false;
    }

    /// <summary>
    /// Перевод из одной единицы в другую.
    /// </summary>
    public double ConvertTemperature(double value, TemperatureUnit from, TemperatureUnit to)
    {
        return FromCelsius(ToCelsius(value, from), to);
    }

    /// <summary>
    /// В Цельсий.
    /// </summary>
    private double ToCelsius(double value, TemperatureUnit from)
    {
        return from switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9,
            TemperatureUnit.Kelvin => value - 273.15,
            TemperatureUnit.Rankine => (value - 491.67) * 5 / 9,
            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// Из Цельсия.
    /// </summary>
    private double FromCelsius(double value, TemperatureUnit to)
    {
        return to switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => value * 9 / 5 + 32,
            TemperatureUnit.Kelvin => value + 273.15,
            TemperatureUnit.Rankine => (value + 273.15) * 9 / 5,
            _ => throw new System.NotImplementedException()
        };
    }
}
