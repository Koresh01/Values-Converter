using UnityEngine;

public class LengthConverter : BaseConverter<LengthUnit, LengthRowUI>
{
    protected override double ToBase(double value, LengthUnit from)
    {
        return from switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value * 1000,
            LengthUnit.Decimeter => value / 10,
            LengthUnit.Centimeter => value / 100,
            LengthUnit.Millimeter => value / 1000,
            LengthUnit.Mile => value * 1609.344,
            LengthUnit.Yard => value * 0.9144,
            LengthUnit.Foot => value * 0.3048,
            _ => throw new System.NotImplementedException()
        };
    }

    protected override double ToTarget(double value, LengthUnit to)
    {
        return to switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value / 1000,
            LengthUnit.Decimeter => value * 10,
            LengthUnit.Centimeter => value * 100,
            LengthUnit.Millimeter => value * 1000,
            LengthUnit.Mile => value / 1609.344,
            LengthUnit.Yard => value / 0.9144,
            LengthUnit.Foot => value / 0.3048,
            _ => throw new System.NotImplementedException()
        };
    }

    protected override LengthUnit GetUnitType(LengthRowUI rowUI) => rowUI.unitType;

    protected override void SetUIValue(LengthRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
