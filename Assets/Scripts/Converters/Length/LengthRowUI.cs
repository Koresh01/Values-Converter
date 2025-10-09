using UnityEngine;

public class LengthRowUI: BaseRowUI<LengthUnit, LengthConverter, LengthRowUI>  // Здесь TRowUI = LengthRowUI, и компилятор проверяет: “А LengthRowUI действительно наследуется от BaseRowUI<LengthUnit, LengthConverter, LengthRowUI>?” Да ✅ — всё совпадает. 
{
    // ничего не нужно, вся логика в BaseRowUI
}
