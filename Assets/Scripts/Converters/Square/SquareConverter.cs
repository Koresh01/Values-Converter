using UnityEngine;

/// <summary>
/// Конвертер для перевода между различными единицами измерения площади.
/// Базовая единица — квадратный метр (м²).
/// </summary>
public class SquareConverter : BaseConverter<SquareUnit, SquareRowUI>
{
    /// <summary>
    /// Перевод из выбранной единицы площади в базовую (квадратные метры).
    /// </summary>
    protected override double ToBase(double value, SquareUnit from)
    {
        return from switch
        {
            // --- СИ и метрическая система ---
            SquareUnit.SquareMeter => value,                       // 1 м² = 1 м²
            SquareUnit.SquareKilometer => value * 1_000_000,        // 1 км² = 1,000,000 м²
            SquareUnit.SquareCentimeter => value / 10_000,          // 1 см² = 0.0001 м²
            SquareUnit.SquareMillimeter => value / 1_000_000,       // 1 мм² = 1e-6 м²
            SquareUnit.SquareMicrometer => value / 1_000_000_000_000, // 1 мкм² = 1e-12 м²
            SquareUnit.Hectare => value * 10_000,                   // 1 га = 10,000 м²
            SquareUnit.Are => value * 100,                          // 1 ар = 100 м²

            // --- Английская система ---
            SquareUnit.SquareFoot => value * 0.092903,              // 1 фут² = 0.092903 м²
            SquareUnit.SquareInch => value * 0.00064516,            // 1 дюйм² = 0.00064516 м²
            SquareUnit.SquareYard => value * 0.836127,              // 1 ярд² = 0.836127 м²
            SquareUnit.SquareMile => value * 2_589_988.110336,      // 1 миля² = 2.58999 × 10⁶ м²
            SquareUnit.Acre => value * 4_046.8564224,               // 1 акр = 4046.8564224 м²

            // --- Морские и географические ---
            SquareUnit.SquareNauticalMile => value * 3_429_904,     // 1 морская миля² = 3.429904 × 10⁶ м²
            SquareUnit.Barn => value * 1e-28,                       // 1 барн = 1e-28 м²

            // --- Азиатские единицы ---
            SquareUnit.Tsubo => value * 3.305785,                   // 1 цубо = 3.305785 м²
            SquareUnit.Pyeong => value * 3.305785,                  // 1 пён = 3.305785 м²
            SquareUnit.Mu => value * 666.6667,                      // 1 му = 666.6667 м²
            SquareUnit.Rai => value * 1600,                         // 1 рай = 1600 м²
            SquareUnit.Dunam => value * 1000,                       // 1 дунам = 1000 м²
            SquareUnit.Ping => value * 3.305785,                    // 1 пин = 3.305785 м²

            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// Перевод из базовой единицы (квадратных метров) в целевую единицу площади.
    /// </summary>
    protected override double ToTarget(double value, SquareUnit to)
    {
        return to switch
        {
            // --- СИ и метрическая система ---
            SquareUnit.SquareMeter => value,
            SquareUnit.SquareKilometer => value / 1_000_000,
            SquareUnit.SquareCentimeter => value * 10_000,
            SquareUnit.SquareMillimeter => value * 1_000_000,
            SquareUnit.SquareMicrometer => value * 1_000_000_000_000,
            SquareUnit.Hectare => value / 10_000,
            SquareUnit.Are => value / 100,

            // --- Английская система ---
            SquareUnit.SquareFoot => value / 0.092903,
            SquareUnit.SquareInch => value / 0.00064516,
            SquareUnit.SquareYard => value / 0.836127,
            SquareUnit.SquareMile => value / 2_589_988.110336,
            SquareUnit.Acre => value / 4_046.8564224,

            // --- Морские и географические ---
            SquareUnit.SquareNauticalMile => value / 3_429_904,
            SquareUnit.Barn => value / 1e-28,

            // --- Азиатские единицы ---
            SquareUnit.Tsubo => value / 3.305785,
            SquareUnit.Pyeong => value / 3.305785,
            SquareUnit.Mu => value / 666.6667,
            SquareUnit.Rai => value / 1600,
            SquareUnit.Dunam => value / 1000,
            SquareUnit.Ping => value / 3.305785,

            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// Возвращает тип единицы измерения, связанный с данной строкой UI.
    /// </summary>
    protected override SquareUnit GetUnitType(SquareRowUI rowUI) => rowUI.unitType;

    /// <summary>
    /// Устанавливает вычисленное значение в UI-элемент.
    /// </summary>
    protected override void SetUIValue(SquareRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
