using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс для перевода единиц ТЕМПЕРАТУРЫ из ЛЮБОЙ в ЛЮБУЮ.
/// </summary>
public class TemperatureConverter : MonoBehaviour
{
    // Функция конвертации из любой единицы в любую
    public double ConvertTemperature(double value, TemperatureUnit from, TemperatureUnit to)
    {
        double celsius = ToCelsius(value, from);
        return FromCelsius(celsius, to);
    }

    // Перевод в базовую единицу (Цельсий)
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

    // Перевод из базовой единицы (Цельсий) в целевую
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
