using UnityEngine;

public class TemperatureConverter : BaseConverter<TemperatureUnit, TemperatureRowUI>
{
    /// <summary>
    /// Перевод из выбранной единицы температуры в базовую (°C).
    /// </summary>
    protected override double ToBase(double value, TemperatureUnit from)
    {
        return from switch
        {
            TemperatureUnit.Celsius => value,                              // Базовая единица
            TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9,            // °C = (°F - 32) × 5/9
            TemperatureUnit.Kelvin => value - 273.15,                      // °C = K - 273.15
            TemperatureUnit.Rankine => (value - 491.67) * 5 / 9,           // °C = (°R - 491.67) × 5/9
            TemperatureUnit.Delisle => 100 - value * 2 / 3,                // °C = 100 - °De × 2/3
            TemperatureUnit.Newton => value * 100 / 33,                    // °C = °N × 100/33
            TemperatureUnit.Réaumur => value * 1.25,                       // °C = °Ré × 1.25
            TemperatureUnit.Rømer => (value - 7.5) * 40 / 21,              // °C = (°Rø - 7.5) × 40/21
            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// Перевод из базовой единицы (°C) в целевую единицу температуры.
    /// </summary>
    protected override double ToTarget(double value, TemperatureUnit to)
    {
        return to switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => value * 9 / 5 + 32,              // °F = °C × 9/5 + 32
            TemperatureUnit.Kelvin => value + 273.15,                      // K = °C + 273.15
            TemperatureUnit.Rankine => (value + 273.15) * 9 / 5,           // °R = (°C + 273.15) × 9/5
            TemperatureUnit.Delisle => (100 - value) * 3 / 2,              // °De = (100 - °C) × 3/2
            TemperatureUnit.Newton => value * 33 / 100,                    // °N = °C × 33/100
            TemperatureUnit.Réaumur => value * 0.8,                        // °Ré = °C × 0.8
            TemperatureUnit.Rømer => value * 21 / 40 + 7.5,                // °Rø = °C × 21/40 + 7.5
            _ => throw new System.NotImplementedException()
        };
    }


    protected override TemperatureUnit GetUnitType(TemperatureRowUI rowUI) => rowUI.unitType;

    protected override void SetUIValue(TemperatureRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
