using System.Collections.Generic;
using UnityEngine;

public abstract class BaseConverter<TUnit, TRowUI> : MonoBehaviour
    where TRowUI : MonoBehaviour
{
    public List<TRowUI> uiList = new List<TRowUI>();
    private bool isUpdating = false;

    public void OnValueChanged(TRowUI sourceUI, string newValue)
    {
        if (isUpdating) return;
        if (!double.TryParse(newValue, out double value)) return;

        TUnit fromUnit = GetUnitType(sourceUI);
        double baseValue = ToBase(value, fromUnit);

        isUpdating = true;
        foreach (var ui in uiList)
        {
            if (ui == sourceUI) continue;
            TUnit toUnit = GetUnitType(ui);
            double converted = ToTarget(baseValue, toUnit);
            SetUIValue(ui, converted);
        }
        isUpdating = false;
    }

    protected abstract double ToBase(double value, TUnit from);
    protected abstract double ToTarget(double value, TUnit to);
    protected abstract TUnit GetUnitType(TRowUI rowUI);
    protected abstract void SetUIValue(TRowUI rowUI, double value);
}
