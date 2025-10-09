
public class PowerConverter : BaseConverter<PowerUnit, PowerRowUI>
{
    protected override double ToBase(double value, PowerUnit from)
    {
        return from switch
        {
            // --- СИ и метрическая система ---
            PowerUnit.Watt => value,
            PowerUnit.Kilowatt => value * 1_000,
            PowerUnit.Megawatt => value * 1_000_000,
            PowerUnit.Gigawatt => value * 1_000_000_000,
            PowerUnit.Milliwatt => value / 1_000,
            PowerUnit.Microwatt => value / 1_000_000,
            PowerUnit.Nanowatt => value / 1_000_000_000,

            // --- Электрические и тепловые ---
            PowerUnit.Horsepower => value * 745.7,
            PowerUnit.MetricHorsepower => value * 735.49875,
            PowerUnit.ElectricalHorsepower => value * 746.0,
            PowerUnit.BoilerHorsepower => value * 9809.5,
            PowerUnit.CaloriePerSecond => value * 4.184,
            PowerUnit.CaloriePerMinute => value * 4.184 / 60.0,
            PowerUnit.BTUPerHour => value * 0.29307107,

            // --- Астрономические ---
            PowerUnit.SolarLuminosity => value * 3.828e26,

            // --- Другие ---
            PowerUnit.ErgPerSecond => value * 1e-7,
            PowerUnit.FootPoundPerSecond => value * 1.3558179483,

            _ => throw new System.NotImplementedException()
        };
    }

    protected override double ToTarget(double value, PowerUnit to)
    {
        return to switch
        {
            // --- СИ и метрическая система ---
            PowerUnit.Watt => value,
            PowerUnit.Kilowatt => value / 1_000,
            PowerUnit.Megawatt => value / 1_000_000,
            PowerUnit.Gigawatt => value / 1_000_000_000,
            PowerUnit.Milliwatt => value * 1_000,
            PowerUnit.Microwatt => value * 1_000_000,
            PowerUnit.Nanowatt => value * 1_000_000_000,

            // --- Электрические и тепловые ---
            PowerUnit.Horsepower => value / 745.7,
            PowerUnit.MetricHorsepower => value / 735.49875,
            PowerUnit.ElectricalHorsepower => value / 746.0,
            PowerUnit.BoilerHorsepower => value / 9809.5,
            PowerUnit.CaloriePerSecond => value / 4.184,
            PowerUnit.CaloriePerMinute => value / (4.184 / 60.0),
            PowerUnit.BTUPerHour => value / 0.29307107,

            // --- Астрономические ---
            PowerUnit.SolarLuminosity => value / 3.828e26,

            // --- Другие ---
            PowerUnit.ErgPerSecond => value / 1e-7,
            PowerUnit.FootPoundPerSecond => value / 1.3558179483,

            _ => throw new System.NotImplementedException()
        };
    }

    protected override PowerUnit GetUnitType(PowerRowUI rowUI) => rowUI.unitType;

    protected override void SetUIValue(PowerRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
