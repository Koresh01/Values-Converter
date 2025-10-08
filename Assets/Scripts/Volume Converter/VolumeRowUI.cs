using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI-������ � ��������� � ����� ����� ������� ������.
/// </summary>
public class VolumeRowUI : MonoBehaviour
{
    [Header("UI ����������:")]
    public TMP_InputField inputField;
    public TMP_Text label;

    [Header("���������:")]
    public VolumeUnit unitType;

    private VolumeConverter converter;

    private void Start()
    {
        // ���� ��������� � �������� (��������, �� Canvas)
        converter = FindFirstObjectByType<VolumeConverter>();

        // ������������� �� ��������� ������
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        converter.OnVolumeChanged(this, newValue);
    }
}
