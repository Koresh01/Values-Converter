using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI-������ � ��������� � ����� ����� �������.
/// </summary>
public class LengthRowUI : MonoBehaviour
{
    [Header("UI ����������:")]
    public TMP_InputField inputField;
    public TMP_Text label;

    [Header("���������:")]
    public LengthUnit unitType;


    private LengthConverter converter;

    private void Start()
    {
        // ���� ��������� � �������� (��������, �� Canvas)
        converter = FindFirstObjectByType<LengthConverter>();

        // ������������� �� ��������� ������
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        converter.OnLengthChanged(this, newValue);
    }
}
