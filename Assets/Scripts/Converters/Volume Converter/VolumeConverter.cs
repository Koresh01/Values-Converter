using UnityEngine;

/// <summary>
/// Конвертер объёмов. 
/// Переводит все значения сначала в кубометры (базовую единицу),
/// затем из кубометров — в целевую единицу.
/// </summary>
public class VolumeConverter : BaseConverter<VolumeUnit, VolumeRowUI>
{
    /// <summary>
    /// Перевод из выбранной единицы измерения в базовую (кубометры).
    /// </summary>
    protected override double ToBase(double value, VolumeUnit from)
    {
        return from switch
        {
            VolumeUnit.CubicMeter => value,                       // базовая единица
            VolumeUnit.Liter => value / 1000,                 // 1 л = 0.001 м³
            VolumeUnit.Milliliter => value / 1_000_000,            // 1 мл = 1e-6 м³
            VolumeUnit.CubicCentimeter => value / 1_000_000,            // 1 см³ = 1 мл = 1e-6 м³
            VolumeUnit.CubicMillimeter => value / 1_000_000_000,        // 1 мм³ = 1e-9 м³
            VolumeUnit.CubicFoot => value * 0.0283168,            // 1 фут³ = 0.0283168 м³
            VolumeUnit.CubicInch => value * 0.0000163871,         // 1 дюйм³ = 1.63871e-5 м³
            VolumeUnit.Gallon => value * 0.00378541,           // 1 галлон (US) = 0.00378541 м³
            VolumeUnit.Quart => value * 0.0009463525,         // 1 кварта = 1/4 галлона = 0.0009463525 м³
            VolumeUnit.Pint => value * 0.00047317625,        // 1 пинта = 1/8 галлона = 0.00047317625 м³
            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// Перевод из базовой единицы (кубометров) в целевую единицу измерения.
    /// </summary>
    protected override double ToTarget(double value, VolumeUnit to)
    {
        return to switch
        {
            VolumeUnit.CubicMeter => value,
            VolumeUnit.Liter => value * 1000,
            VolumeUnit.Milliliter => value * 1_000_000,
            VolumeUnit.CubicCentimeter => value * 1_000_000,
            VolumeUnit.CubicMillimeter => value * 1_000_000_000,
            VolumeUnit.CubicFoot => value / 0.0283168,
            VolumeUnit.CubicInch => value / 0.0000163871,
            VolumeUnit.Gallon => value / 0.00378541,
            VolumeUnit.Quart => value / 0.0009463525,
            VolumeUnit.Pint => value / 0.00047317625,
            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// Возвращает тип единицы, с которой связан данный UI-элемент.
    /// </summary>
    protected override VolumeUnit GetUnitType(VolumeRowUI rowUI) => rowUI.unitType;

    /// <summary>
    /// Устанавливает значение в поле ввода.
    /// </summary>
    protected override void SetUIValue(VolumeRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
