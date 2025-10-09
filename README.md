<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/be45d2af-4ffe-4c19-8a65-f644b81e057f" />

# Описание программы
Это android приложение "конвертер величин". Сделал я его на C# Unity. Пользователь выбирает раздел(длина / площадь / температура...), а дальше вводит значение в определённую ячейку и видит сколько это будет в других единицах измерения.

# Что нового я для себя узнал
В этой работе я активно применял дженерики. Хочу поделиться своим подходом с вами.

# Постановка задачи
Программа имеет разделы:

<center><img width="397/2" height="300/2" alt="image" src="https://github.com/user-attachments/assets/e51b5dd9-f366-4a2f-af8f-c5015a4fb3fa" /></center>

Возьмём например длину:

<img width="432" height="330" alt="image" src="https://github.com/user-attachments/assets/9d874173-e507-40f0-b69d-08746e2f696a" />

Я хочу, чтобы когда пользователь вводил значение в любую из ячеек, значения в других ячейках -  автоматически пересчитывались. И так для каждой темы: длина / площадь / температура...

# Попробуем сделать конвертер для величин длины.
Так что давайте создадим скрипт ```Units.cs```, в котором будут перечисления единиц измерения:

```C#
/// <summary>
/// Единицы измерения длины.
/// </summary>
public enum LengthUnit
{
    Meter,      // Метр (основная единица длины в СИ)
    Kilometer,  // Километр (1000 метров)
    Centimeter, // Сантиметр (0.01 метра)
    Millimeter, // Миллиметр (0.001 метра)
    Decimeter,  // Дециметр (0.1 метра) 
    Mile,       // Миля (1609.344 метров, английская миля)
    Yard,       // Ярд (0.9144 метра, английская единица)
    Foot        // Фут (0.3048 метра, английская единица)
}

// Пока что тут только единицы измерения длины.
```


Теперь нам нужен сам ```LengthConverter.cs```. Но как мы будем переводить из одной единицы измерения в другую? У нас много ячеек. Не будешь же писать функцию для каждого возможного перевода, к примеру:
```
из (мм) -> (все остальные)
из (см) -> (все остальные)
...
...
```

Я подумал и решил, что лучше всего научить наш ```LengthConverter.cs``` переводить некоторую величину сначала в базовую, а затем в необходимую. Вот как выглядит код:
```C#
/// <summary>
/// Конвертер единиц длины (без наследования).
/// </summary>
public class LengthConverter : MonoBehaviour
{
    ...                  <-------------------- пока что оставим этот момент
    
    // Переводит значение из заданной единицы в метры.
    private double ToBase(double value, LengthUnit from)
    {
        return from switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value * 1000,
            LengthUnit.Decimeter => value / 10,
            LengthUnit.Centimeter => value / 100,
            LengthUnit.Millimeter => value / 1000,
            LengthUnit.Mile => value * 1609.344,
            LengthUnit.Yard => value * 0.9144,
            LengthUnit.Foot => value * 0.3048,
            _ => throw new System.NotImplementedException($"Неизвестная единица: {from}")
        };
    }

    // Переводит значение из метров в указанную единицу.
    private double ToTarget(double value, LengthUnit to)
    {
        return to switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value / 1000,
            LengthUnit.Decimeter => value * 10,
            LengthUnit.Centimeter => value * 100,
            LengthUnit.Millimeter => value * 1000,
            LengthUnit.Mile => value / 1609.344,
            LengthUnit.Yard => value / 0.9144,
            LengthUnit.Foot => value / 0.3048,
            _ => throw new System.NotImplementedException($"Неизвестная единица: {to}")
        };
    }
}
```

Отлично. Но что если мы захотим написать конвертер для величин ТЕМПЕРАТУРЫ? Тогда опять создаём enum с единицами измерения, то есть дополняем наш ```Units.cs```:
```C#
public enum LengthUnit  // <----------------- уже было
{
    Meter,
    Kilometer,
    Centimeter,
    Millimeter,
    Decimeter,
    Mile,
    Yard,
    Foot
}

/// <summary>
/// Единицы измерения температуры.
/// </summary>
public enum TemperatureUnit   // <----------------- НОВОЕ!
{
    Celsius,        // Градусы Цельсия (°C) — базовая единица для повседневных измерений, используется в СИ
    Fahrenheit,     // Градусы Фаренгейта (°F) — англосаксонская система, вода замерзает при 32°F и кипит при 212°F
    Kelvin,         // Кельвины (K) — абсолютная шкала температуры, 0 K = -273.15°C
    Rankine,        // Градусы Ранкина (°R) — абсолютная шкала в англосаксонской системе, 0°R = 0 K
    Delisle,        // Градусы Делиля (°De) — историческая шкала, 0°De = 100°C, увеличение °De уменьшает температуру
    Newton,         // Градусы Ньютона (°N) — историческая шкала, 0°N = 0°C, 33°N = 100°C
    Réaumur,        // Градусы Реомюра (°Ré) — историческая шкала, 0°Ré = 0°C, 80°Ré = 100°C
    Rømer,          // Градусы Рёмера (°Rø) — историческая шкала, 0°Rø = -7.5°C, 60°Rø = 60°C
}
```

Ну чтож, напишем теперь ```TemperatureConveter.cs```:
```C#
/// <summary>
/// Конвертер единиц температуры (без наследования).
/// Базовая единица — градусы Цельсия (°C).
/// </summary>
public class TemperatureConverter : MonoBehaviour
{
    ...                  <-------------------- пока что оставим этот момент

    // Переводит значение из указанной единицы температуры в градусы Цельсия.
    private double ToBase(double value, TemperatureUnit from)
    {
        return from switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9,
            TemperatureUnit.Kelvin => value - 273.15,
            TemperatureUnit.Rankine => (value - 491.67) * 5 / 9,
            TemperatureUnit.Delisle => 100 - value * 2 / 3,
            TemperatureUnit.Newton => value * 100 / 33,
            TemperatureUnit.Réaumur => value * 5 / 4,
            TemperatureUnit.Rømer => (value - 7.5) * 40 / 21,
            _ => throw new System.NotImplementedException($"Неизвестная единица: {from}")
        };
    }

    // Переводит значение из градусов Цельсия в указанную единицу температуры.
    private double ToTarget(double value, TemperatureUnit to)
    {
        return to switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => value * 9 / 5 + 32,
            TemperatureUnit.Kelvin => value + 273.15,
            TemperatureUnit.Rankine => (value + 273.15) * 9 / 5,
            TemperatureUnit.Delisle => (100 - value) * 3 / 2,
            TemperatureUnit.Newton => value * 33 / 100,
            TemperatureUnit.Réaumur => value * 4 / 5,
            TemperatureUnit.Rømer => value * 21 / 40 + 7.5,
            _ => throw new System.NotImplementedException($"Неизвестная единица: {to}")
        };
    }
}
```

Давайте сравним ```LengthConverter.cs``` и ```TemperatureConverter.cs```:
* У них разные формулы конвертации.
* Одинаковое - как раз те самые "...", где я писал                   "<-------------------- пока что оставим этот момент"

"..." - это работа скриптов с сущностями на сцене. А именно с префабом вида:

<img width="425" height="57" alt="image" src="https://github.com/user-attachments/assets/2583bef7-cef0-4a07-80a1-766b4171405f" />

 И вот что из себя этот префаб представляет:
 
 <img width="340" height="49" alt="image" src="https://github.com/user-attachments/assets/fd69b6d0-d53f-4da7-b58a-34c15c2b5777" />



Прикол в том, что если мы с вами начнём дальше создавать ``` TemperatureConverter.cs, MassaConverter.cs ... ``` То у всех у них будут общими эти "...", и каждый раз будет получаться дублирование кода. Давайте пока что временно забудем про наши конвертеры и поговорим про префабы и scroll view.

# Префабы и scroll view:
Итак по сути, для каждой темы у нас будет:
* своя страничка (на картинке обозначены белыми галочками)
* в каждой страничке scroll view с компонентом vertical layout group.
* в content - префаб "input_field <-> TextMeshPro"

<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/4da36b3e-563e-49aa-a72e-9deddc0ca40f" />

Чтобы такой префаб был не просто UI элементом, а отвечал за какую то конкретную величину, к примеру метр (м) или миллиметр (мм) нужно повесить на него скрипт, с возможностью выбора прямо из инспектора единицы измерения за которую он отвечает. Вот как может выглядеть такой скрипт:
```C#
using UnityEngine;
using TMPro;

// Скрипт префаба "input_field <-> TextMeshPro". 
public class LengthRowUI : MonoBehaviour
{
    [Header("UI Компоненты:")]
    public TMP_InputField inputField;
    public TMP_Text label;

    [Header("Тип единиц измерения:")]
    public LengthUnit unitType;

    private LengthConverter converter;  // уже создавали этот конвертер

    private void Start()
    {
        // Находим конвертер на сцене (можно также сделать через инспектор)
        converter = FindFirstObjectByType<LengthConverter>();

        // Добавляем слушатель изменения поля ввода
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        // Удаляем слушатель при уничтожении объекта
        inputField.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        // Передаём событие конвертеру
        converter.OnValueChanged(this, newValue);
    }
}
```

Тогда "..." в ```LengthConverter.cs``` можно заменить кодом, который будет обновлять значения в других префабах "input_field <-> TextMeshPro", но нужно быть осторожным с рекурсиями. Поэтому используя флаг, решаем эту проблему. Полный листинг кода ```LengthConverter.cs```:

```C#
public class LengthConverter : MonoBehaviour
{
    ------------------------------------------------------------------- вместо "..."
    [System.Serializable]
    public class LengthRowUI
    {
        public LengthUnit unitType;   // Тип единицы длины
        public InputField inputField; // Поле ввода
    }

    [Header("Список всех UI-строк конвертера")]
    public List<LengthRowUI> uiList = new List<LengthRowUI>();

    private bool isUpdating = false;

    public void OnValueChanged(LengthRowUI sourceUI, string newValue)
    {
        if (isUpdating) return;
        if (!double.TryParse(newValue, out double value)) return;

        // Переводим из выбранной единицы в метры
        double baseValue = ToBase(value, sourceUI.unitType);

        isUpdating = true;
        foreach (var ui in uiList)
        {
            if (ui == sourceUI) continue;

            double converted = ToTarget(baseValue, ui.unitType);
            ui.inputField.text = converted.ToString("0.####");
        }
        isUpdating = false;
    }
    --------------------------------------------------------------------------

    private double ToBase(double value, LengthUnit from)    // <------------ без изменений
    {
        return from switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value * 1000,
            LengthUnit.Decimeter => value / 10,
            LengthUnit.Centimeter => value / 100,
            LengthUnit.Millimeter => value / 1000,
            LengthUnit.Mile => value * 1609.344,
            LengthUnit.Yard => value * 0.9144,
            LengthUnit.Foot => value * 0.3048,
            _ => throw new System.NotImplementedException($"Неизвестная единица: {from}")
        };
    }

    private double ToTarget(double value, LengthUnit to)     // <------------ без изменений
    {
        return to switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value / 1000,
            LengthUnit.Decimeter => value * 10,
            LengthUnit.Centimeter => value * 100,
            LengthUnit.Millimeter => value * 1000,
            LengthUnit.Mile => value / 1609.344,
            LengthUnit.Yard => value / 0.9144,
            LengthUnit.Foot => value / 0.3048,
            _ => throw new System.NotImplementedException($"Неизвестная единица: {to}")
        };
    }
}

```

Но чтобы расширять такой код, для случая, когда разделов для конвертации (длина, площадь, температура, мощность...) - очень много, прийдётся каждый раз создавать новый ``` ?Converter.cs ``` и ``` ?RowUI ```, где "?" - это название новой темы.

# Обобщение нашего подхода для случая когда очень много разделов
Короче раз для всех разделов конвертации (длина, площадь, температура, мощность...) у нас с вами:
* одинаковый подход "переведи в базовую единицу измерения, и из базовой в необходимую"
* одинаковый алгоритм под внегласным названием "..." для всех конвертеров (```LengthConverter.cs```, ```TemperatureConverter.cs```, ```ОбъёмConverter.cs```, ```ПлощадьConverter.cs```,  ...)

Предлагается создать базовый класс конвертерам ```BaseConverter.cs```, и все конвертеры наследовать от него:
```C#
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
```
, где ```TRowUI``` - скрипт префаба элемента списка. То есть префаб с "input_field <-> TextMeshPro". К примеру если передать сюда ```LengthRowUI.cs``` или ```TemperatureRowUI``` то он будет понимать с какой категорией (длина / темература) он работает. Он даже поймёт с каким конкретно префабом в данный момент взаимодействует пользователь.
      ```TUnit``` - произвольный тип единиц измерения. То есть если передать сюда из нашего ```Units.cs``` к примеру ```LengthUnit``` или ```TemperatureUnit```... он будет работать с этими единицами измерения.

И тогда ```LengthConverter.cs``` пример вид:
```C#
using UnityEngine;

public class LengthConverter : BaseConverter<LengthUnit, LengthRowUI>  // будем наследоваться от BaseConverter, единица измерения = LengthUnit(Meter, Kilometer...), а префаб тот, на котором висит скрипт LengthRowUI.cs.
{
    protected override double ToBase(double value, LengthUnit from)
    {
        return from switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value * 1000,
            LengthUnit.Decimeter => value / 10,
            LengthUnit.Centimeter => value / 100,
            LengthUnit.Millimeter => value / 1000,
            LengthUnit.Mile => value * 1609.344,
            LengthUnit.Yard => value * 0.9144,
            LengthUnit.Foot => value * 0.3048,
            _ => throw new System.NotImplementedException()
        };
    }

    protected override double ToTarget(double value, LengthUnit to)
    {
        return to switch
        {
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value / 1000,
            LengthUnit.Decimeter => value * 10,
            LengthUnit.Centimeter => value * 100,
            LengthUnit.Millimeter => value * 1000,
            LengthUnit.Mile => value / 1609.344,
            LengthUnit.Yard => value / 0.9144,
            LengthUnit.Foot => value / 0.3048,
            _ => throw new System.NotImplementedException()
        };
    }

    protected override LengthUnit GetUnitType(LengthRowUI rowUI) => rowUI.unitType;

    protected override void SetUIValue(LengthRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
```

И тогда каждый раз когда мы будем добавлять новые разделы, мы будем наследоваться от ```BaseConverter.cs```, вот например код для конвертера температуры ```TemperatureConverter.cs```:
```C#
using UnityEngine;

public class TemperatureConverter : BaseConverter<TemperatureUnit, TemperatureRowUI>  // "я тот же BaseConverter, но работаю с конкретными единицами измерения - TemperatureUnit(Celsius, Fahrenheit...). К тому же я работаю только с префабами на которых висит TemperatureRowUI"
{
    /// <summary>
    /// Перевод из выбранной единицы температуры в базовую (°C).
    /// </summary>
    protected override double ToBase(double value, TemperatureUnit from)
    {
        return from switch
        {
            TemperatureUnit.Celsius => value,                              // Базовая единица
            TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9,            // °C = (°F - 32) × 5/9
            TemperatureUnit.Kelvin => value - 273.15,                      // °C = K - 273.15
            TemperatureUnit.Rankine => (value - 491.67) * 5 / 9,           // °C = (°R - 491.67) × 5/9
            TemperatureUnit.Delisle => 100 - value * 2 / 3,                // °C = 100 - °De × 2/3
            TemperatureUnit.Newton => value * 100 / 33,                    // °C = °N × 100/33
            TemperatureUnit.Réaumur => value * 1.25,                       // °C = °Ré × 1.25
            TemperatureUnit.Rømer => (value - 7.5) * 40 / 21,              // °C = (°Rø - 7.5) × 40/21
            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// Перевод из базовой единицы (°C) в целевую единицу температуры.
    /// </summary>
    protected override double ToTarget(double value, TemperatureUnit to)
    {
        return to switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => value * 9 / 5 + 32,              // °F = °C × 9/5 + 32
            TemperatureUnit.Kelvin => value + 273.15,                      // K = °C + 273.15
            TemperatureUnit.Rankine => (value + 273.15) * 9 / 5,           // °R = (°C + 273.15) × 9/5
            TemperatureUnit.Delisle => (100 - value) * 3 / 2,              // °De = (100 - °C) × 3/2
            TemperatureUnit.Newton => value * 33 / 100,                    // °N = °C × 33/100
            TemperatureUnit.Réaumur => value * 0.8,                        // °Ré = °C × 0.8
            TemperatureUnit.Rømer => value * 21 / 40 + 7.5,                // °Rø = °C × 21/40 + 7.5
            _ => throw new System.NotImplementedException()
        };
    }


    protected override TemperatureUnit GetUnitType(TemperatureRowUI rowUI) => rowUI.unitType;

    protected override void SetUIValue(TemperatureRowUI rowUI, double value)
    {
        rowUI.inputField.text = value.ToString("0.####");
    }
}
```

# Осталось разобраться со скриптом который висит на префабе "input_field <-> TextMeshPro".
Это скрипты ```LengthRowUI.cs```, ```TemperatureRowUI.cs```. Такой скрипт висит на префабе, чтобы мы могли задать единицу измерения нашей ячейке.

<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/96d2f90c-cab7-4170-9804-77a2e3046808" />

Но вот проблема. Единицы измерения определяются как бы разделом (длина, площадь...). Получается что для каждого раздела прийдётся занова писать этот скрипт. А суть одинаковая. Разное только то, что этот скрипт работает с разными enum единиц измерения. И разный конвертер обрабатывает вычисления. Так что этот скрипт тоже можно обощить:
Создадим ```BaseRowUI.cs``` и пусть он работает с произвольными единицами измерения ```TUnit``` и с произвольным ковертером ```TConverter```. И в конвертрет передаёт сам себя ```TRowUI```.
```C#
public class BaseRowUI<TUnit, TConverter, TRowUI> : MonoBehaviour   // Я — базовый класс для UI-строки. У меня есть три шаблонных типа: единица измерения, конвертер и сама строка UI.
    where TConverter : BaseConverter<TUnit, TRowUI>                 // Тип, который ты подставишь вместо TConverter, обязан быть наследником класса BaseConverter<TUnit, TRowUI>
    where TRowUI : BaseRowUI<TUnit, TConverter, TRowUI>             // Тип, который ты подставишь как TRowUI, обязан быть наследником самого BaseRowUI, но уже со своими конкретными типами внутри.
{
    [Header("UI Компоненты:")]
    public TMP_InputField inputField;
    public TMP_Text label;

    [Header("Настройки:")]
    public TUnit unitType;

    private TConverter converter;

    private void Start()
    {
        converter = FindFirstObjectByType<TConverter>();
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        converter?.OnValueChanged((TRowUI)this, newValue);
    }
}
```


И тогда достаточно унаследовать все ```LengthRowUI.cs```, ```TemperatureRowUI.cs``` от ```BaseRowUI```. Выглядеть это будет так:
```C#
public class TemperatureRowUI : BaseRowUI<TemperatureUnit, TemperatureConverter, TemperatureRowUI>
{
    // ничего не нужно, вся логика в BaseRowUI
}

public class LengthRowUI: BaseRowUI<LengthUnit, LengthConverter, LengthRowUI>  // Здесь TRowUI = LengthRowUI, и компилятор проверяет: “А LengthRowUI действительно наследуется от BaseRowUI<LengthUnit, LengthConverter, LengthRowUI>?” Да ✅ — всё совпадает. 
{
    // ничего не нужно, вся логика в BaseRowUI
}

public class SquareRowUI : BaseRowUI<SquareUnit, SquareConverter, SquareRowUI>
{
    // ничего не нужно, вся логика в BaseRowUI
}

... и т.д.
```

Префаб кидается в content нашего scroll view, а на него кидается соответствующий скрипт ```LengthRowUI.cs``` или ```TemperatureRowUI.cs``` или ... Так что переписывать код каждый раз занова нам не нужно.





