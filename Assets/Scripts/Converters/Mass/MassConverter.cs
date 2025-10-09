using UnityEngine;

public class MassConverter : BaseConverter<MassUnit, MassRowUI>
{
    protected override double ToBase(double value, MassUnit from)
    {
        return from switch
        {
            // --- СИ и метрическая система ---
            MassUnit.Kilogram => value,
            MassUnit.Gram => value / 1000.0,
            MassUnit.Milligram => value / 1_000_000.0,
            MassUnit.Microgram => value / 1_000_000_000.0,
            MassUnit.Tonne => value * 1000.0,
            MassUnit.Megagram => value * 1000.0,

            // --- Английская и американская системы ---
            MassUnit.Pound => value * 0.45359237,
            MassUnit.Ounce => value * 0.028349523125,
            MassUnit.Stone => value * 6.35029318,
            MassUnit.ShortTon => value * 907.18474,
            MassUnit.LongTon => value * 1016.0469088,
            MassUnit.Grain => value * 0.00006479891,
            MassUnit.HundredweightUS => value * 45.359237,
            MassUnit.HundredweightUK => value * 50.80234544,

            // --- Астрономические и физические ---
            MassUnit.EarthMass => value * 5.9722e24,
            MassUnit.SolarMass => value * 1.98847e30,
            MassUnit.AtomicMassUnit => value * 1.66053906660e-27,
            MassUnit.ElectronVoltPerC2 => value * 1.78266192e-36,

            // --- Другие ---
            MassUnit.Carat => value * 0.0002,
            MassUnit.Slug => value * 14.5939029372,
            MassUnit.Dram => value * 0.0017718451953125,

            _ => throw new System.NotImplementedException()
        };
    }

    protected override double ToTarget(double value, MassUnit to)
    {
        return to switch
        {
            // --- СИ и метрическая система ---
            MassUnit.Kilogram => value,
            MassUnit.Gram => value * 1000.0,
            MassUnit.Milligram => value * 1_000_000.0,
            MassUnit.Microgram => value * 1_000_000_000.0,
            MassUnit.Tonne => value / 1000.0,
            MassUnit.Megagram => value / 1000.0,

            // --- Английская и американская системы ---
            MassUnit.Pound => value / 0.45359237,
            MassUnit.Ounce => value / 0.028349523125,
            MassUnit.Stone => value / 6.35029318,
            MassUnit.ShortTon => value / 907.18474,
            MassUnit.LongTon => value / 1016.0469088,
            MassUnit.Grain => value / 0.00006479891,
            MassUnit.HundredweightUS => value / 45.359237,
            MassUnit.HundredweightUK => value / 50.80234544,

            // --- Астрономические и физические ---
            MassUnit.EarthMass => value / 5.9722e24,
            MassUnit.SolarMass => value / 1.98847e30,
            MassUnit.AtomicMassUnit => value / 1.66053906660e-27,
            MassUnit.ElectronVoltPerC2 => value / 1.78266192e-36,

            // --- Другие ---
            MassUnit.Carat => value / 0.0002,
            MassUnit.Slug => value / 14.5939029372,
            MassUnit.Dram => value / 0.0017718451953125,

            _ => throw new System.NotImplementedException()
        };
    }

    protected override MassUnit GetUnitType(MassRowUI rowUI) => rowUI.unitType;

    protected override void SetUIValue(MassRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
