
public class PressureConverter : BaseConverter<PressureUnit, PressureRowUI>
{
    protected override double ToBase(double value, PressureUnit from)
    {
        return from switch
        {
            // --- СИ и метрическая система ---
            PressureUnit.Pascal => value,
            PressureUnit.Kilopascal => value * 1_000,
            PressureUnit.Megapascal => value * 1_000_000,
            PressureUnit.Gigapascal => value * 1_000_000_000,
            PressureUnit.Hectopascal => value * 100,
            PressureUnit.Bar => value * 100_000,
            PressureUnit.Millibar => value * 100,
            PressureUnit.TechnicalAtmosphere => value * 98_066.5,
            PressureUnit.StandardAtmosphere => value * 101_325,

            // --- Английская и американская системы ---
            PressureUnit.Psi => value * 6_894.757,
            PressureUnit.Torr => value * 133.322,
            PressureUnit.InchOfMercury => value * 3_386.389,
            PressureUnit.InchOfWater => value * 249.0889,
            PressureUnit.PoundPerSquareFoot => value * 47.88026,

            // --- Другие и технические ---
            PressureUnit.DynePerSquareCentimeter => value * 0.1,
            PressureUnit.KilogramForcePerSquareCentimeter => value * 98_066.5,
            PressureUnit.MillimeterOfMercury => value * 133.322,
            PressureUnit.Barye => value * 0.1,

            _ => throw new System.NotImplementedException()
        };
    }

    protected override double ToTarget(double value, PressureUnit to)
    {
        return to switch
        {
            // --- СИ и метрическая система ---
            PressureUnit.Pascal => value,
            PressureUnit.Kilopascal => value / 1_000,
            PressureUnit.Megapascal => value / 1_000_000,
            PressureUnit.Gigapascal => value / 1_000_000_000,
            PressureUnit.Hectopascal => value / 100,
            PressureUnit.Bar => value / 100_000,
            PressureUnit.Millibar => value / 100,
            PressureUnit.TechnicalAtmosphere => value / 98_066.5,
            PressureUnit.StandardAtmosphere => value / 101_325,

            // --- Английская и американская системы ---
            PressureUnit.Psi => value / 6_894.757,
            PressureUnit.Torr => value / 133.322,
            PressureUnit.InchOfMercury => value / 3_386.389,
            PressureUnit.InchOfWater => value / 249.0889,
            PressureUnit.PoundPerSquareFoot => value / 47.88026,

            // --- Другие и технические ---
            PressureUnit.DynePerSquareCentimeter => value / 0.1,
            PressureUnit.KilogramForcePerSquareCentimeter => value / 98_066.5,
            PressureUnit.MillimeterOfMercury => value / 133.322,
            PressureUnit.Barye => value / 0.1,

            _ => throw new System.NotImplementedException()
        };
    }

    protected override PressureUnit GetUnitType(PressureRowUI rowUI) => rowUI.unitType;

    protected override void SetUIValue(PressureRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
