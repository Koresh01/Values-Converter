using UnityEngine;

public class FrequencyConverter : BaseConverter<FrequencyUnit, FrequencyRowUI>
{
    protected override double ToBase(double value, FrequencyUnit from)
    {
        return from switch
        {
            FrequencyUnit.Hertz => value,
            FrequencyUnit.Kilohertz => value * 1_000,
            FrequencyUnit.Megahertz => value * 1_000_000,
            FrequencyUnit.Gigahertz => value * 1_000_000_000,
            FrequencyUnit.Terahertz => value * 1_000_000_000_000,
            FrequencyUnit.Millihertz => value / 1_000,
            FrequencyUnit.Microhertz => value / 1_000_000,
            FrequencyUnit.Nanohertz => value / 1_000_000_000,
            FrequencyUnit.RadianPerSecond => value / (2 * Mathf.PI),
            FrequencyUnit.BeatsPerMinute => value / 60.0,
            FrequencyUnit.CyclesPerMinute => value / 60.0,
            FrequencyUnit.RevolutionsPerSecond => value,
            FrequencyUnit.RevolutionsPerMinute => value / 60.0,
            FrequencyUnit.PerDay => value / 86_400.0,
            _ => throw new System.NotImplementedException()
        };
    }

    protected override double ToTarget(double value, FrequencyUnit to)
    {
        return to switch
        {
            FrequencyUnit.Hertz => value,
            FrequencyUnit.Kilohertz => value / 1_000,
            FrequencyUnit.Megahertz => value / 1_000_000,
            FrequencyUnit.Gigahertz => value / 1_000_000_000,
            FrequencyUnit.Terahertz => value / 1_000_000_000_000,
            FrequencyUnit.Millihertz => value * 1_000,
            FrequencyUnit.Microhertz => value * 1_000_000,
            FrequencyUnit.Nanohertz => value * 1_000_000_000,
            FrequencyUnit.RadianPerSecond => value * (2 * Mathf.PI),
            FrequencyUnit.BeatsPerMinute => value * 60.0,
            FrequencyUnit.CyclesPerMinute => value * 60.0,
            FrequencyUnit.RevolutionsPerSecond => value,
            FrequencyUnit.RevolutionsPerMinute => value * 60.0,
            FrequencyUnit.PerDay => value * 86_400.0,
            _ => throw new System.NotImplementedException()
        };
    }

    protected override FrequencyUnit GetUnitType(FrequencyRowUI rowUI) => rowUI.unitType;

    protected override void SetUIValue(FrequencyRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
