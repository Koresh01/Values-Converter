using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ��������� ����� ����� ����� ���������.
/// </summary>
public class LengthConverter : MonoBehaviour
{
    public List<LengthRowUI> lengthUIList = new List<LengthRowUI>();
    private bool isUpdating = false; // ������ �� �������� ��� ���������� �����

    /// <summary>
    /// ����������, ����� ������������ ������ �������� � ����� �� �����.
    /// </summary>
    public void OnLengthChanged(LengthRowUI sourceUI, string newValue)
    {
        if (isUpdating) return; // ��� ��� ��������
        if (!double.TryParse(newValue, out double value)) return; // �� ����� � �������

        LengthUnit fromUnit = sourceUI.unitType;
        double meters = ToMeters(value, fromUnit); // � �����

        isUpdating = true;
        foreach (var ui in lengthUIList)
        {
            if (ui == sourceUI) continue; // ���������� ����-��������
            double converted = FromMeters(meters, ui.unitType); // ��������
            ui.inputField.text = converted.ToString("0.####");  // ��������� �����
        }
        isUpdating = false;
    }

    /// <summary>
    /// ������������� ����� ��������.
    /// </summary>
    public double ConvertLength(double value, LengthUnit from, LengthUnit to)
    {
        return FromMeters(ToMeters(value, from), to);
    }

    /// <summary>
    /// ������� � �����.
    /// </summary>
    private double ToMeters(double value, LengthUnit from)
    {
        return from switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value * 1000,
            LengthUnit.Centimeter => value / 100,
            LengthUnit.Millimeter => value / 1000,
            LengthUnit.Mile => value * 1609.344,
            LengthUnit.Yard => value * 0.9144,
            LengthUnit.Foot => value * 0.3048,
            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// ������� �� ������.
    /// </summary>
    private double FromMeters(double value, LengthUnit to)
    {
        return to switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value / 1000,
            LengthUnit.Centimeter => value * 100,
            LengthUnit.Millimeter => value * 1000,
            LengthUnit.Mile => value / 1609.344,
            LengthUnit.Yard => value / 0.9144,
            LengthUnit.Foot => value / 0.3048,
            _ => throw new System.NotImplementedException()
        };
    }
}
