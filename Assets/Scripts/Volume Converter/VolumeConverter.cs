using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ��������� ������ ����� ����� ���������.
/// </summary>
public class VolumeConverter : MonoBehaviour
{
    public List<VolumeRowUI> volumeUIList = new List<VolumeRowUI>();
    private bool isUpdating = false; // ������ �� �������� ��� ���������� �����

    /// <summary>
    /// ����������, ����� ������������ ������ �������� � ����� �� �����.
    /// </summary>
    public void OnVolumeChanged(VolumeRowUI sourceUI, string newValue)
    {
        if (isUpdating) return; // ��� ��� ��������
        if (!double.TryParse(newValue, out double value)) return; // �� ����� � �������

        VolumeUnit fromUnit = sourceUI.unitType;
        double cubicMeters = ToCubicMeters(value, fromUnit); // � ���������� �����

        isUpdating = true;
        foreach (var ui in volumeUIList)
        {
            if (ui == sourceUI) continue; // ���������� ����-��������
            double converted = FromCubicMeters(cubicMeters, ui.unitType); // ��������
            ui.inputField.text = converted.ToString("0.####");  // ��������� �����
        }
        isUpdating = false;
    }

    /// <summary>
    /// ������������� ����� ��������.
    /// </summary>
    public double ConvertVolume(double value, VolumeUnit from, VolumeUnit to)
    {
        return FromCubicMeters(ToCubicMeters(value, from), to);
    }

    /// <summary>
    /// ������� � ���������� �����.
    /// </summary>
    private double ToCubicMeters(double value, VolumeUnit from)
    {
        return from switch
        {
            VolumeUnit.CubicMeter => value,
            VolumeUnit.Liter => value / 1000,
            VolumeUnit.Milliliter => value / 1_000_000,
            VolumeUnit.CubicCentimeter => value / 1_000_000,
            VolumeUnit.CubicMillimeter => value / 1_000_000_000,
            VolumeUnit.CubicFoot => value * 0.0283168,
            VolumeUnit.CubicInch => value * 0.0000163871,
            VolumeUnit.Gallon => value * 0.00378541, // US gallon
            VolumeUnit.Quart => value * 0.000946353,
            VolumeUnit.Pint => value * 0.000473176,
            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// ������� �� ���������� ������.
    /// </summary>
    private double FromCubicMeters(double value, VolumeUnit to)
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
            VolumeUnit.Quart => value / 0.000946353,
            VolumeUnit.Pint => value / 0.000473176,
            _ => throw new System.NotImplementedException()
        };
    }
}
