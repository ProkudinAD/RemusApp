using System.Numerics;
using System.Text;

namespace Content.Application.Helper
{
internal static class CurrencyToStringHelper
{
    // Наименования сотен
    private static readonly string[] Hundreds =
    {
        string.Empty,
        "сто ",
        "двести ",
        "триста ",
        "четыреста ",
        "пятьсот ",
        "шестьсот ",
        "семьсот ",
        "восемьсот ",
        "девятьсот "
    };

    // Наименования десятков
    private static readonly string[] Tens =
    {
        string.Empty,
        "десять ",
        "двадцать ",
        "тридцать ",
        "сорок ",
        "пятьдесят ",
        "шестьдесят ",
        "семьдесят ",
        "восемьдесят ",
        "девяносто "
    };

    /// <summary>
    /// Перевод в строку числа с учётом падежного окончания относящегося к числу существительного
    /// </summary>
    /// <param name="number">Число</param>
    /// <param name="male">Род существительного, которое относится к числу</param>
    /// <param name="one">Форма существительного в единственном числе</param>
    /// <param name="two">Форма существительного от двух до четырёх</param>
    /// <param name="five">Форма существительного от пяти и больше</param>
    /// <returns></returns>
    private static string ToString<TNumber>(TNumber number, bool male, string one, string two, string five)
        where TNumber : INumber<TNumber>
    {
        number %= TNumber.CreateTruncating(1000);

        var oneNumber = TNumber.CreateTruncating(1);
        var tenNumber = TNumber.CreateTruncating(10);
        var twentyNumber = TNumber.CreateTruncating(20);
        var hundredNumber = TNumber.CreateTruncating(100);

        if (number < oneNumber || number == TNumber.Zero)
        {
            return string.Empty;
        }

        if (number < TNumber.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(number), "Параметр не может быть отрицательным");
        }

        string[] fractions =
        {
            string.Empty,
            "один ",
            "два ",
            "три ",
            "четыре ",
            "пять ",
            "шесть ",
            "семь ",
            "восемь ",
            "девять ",
            "десять ",
            "одиннадцать ",
            "двенадцать ",
            "тринадцать ",
            "четырнадцать ",
            "пятнадцать ",
            "шестнадцать ",
            "семнадцать ",
            "восемнадцать ",
            "девятнадцать "
        };

        if (!male)
        {
            fractions[1] = "одна ";
            fractions[2] = "две ";
        }

        var builder = new StringBuilder(Hundreds[int.CreateTruncating(number / hundredNumber)]);

        if (number % hundredNumber < twentyNumber)
        {
            builder.Append(fractions[int.CreateTruncating(number % hundredNumber)]);
        }
        else
        {
            builder.Append(Tens[int.CreateTruncating(number % hundredNumber / tenNumber)]);
            builder.Append(fractions[int.CreateTruncating(number % tenNumber)]);
        }

        builder.Append(Case(number, one, two, five));
        if (builder.Length != 0)
        {
            builder.Append(" ");
        }

        return builder.ToString();
    }

    /// <summary>
    /// Выбор правильного падежного окончания существительного
    /// </summary>
    /// <param name="number">Число</param>
    /// <param name="one">Форма существительного в единственном числе</param>
    /// <param name="two">Форма существительного от двух до четырёх</param>
    /// <param name="five">Форма существительного от пяти и больше</param>
    /// <returns>Возвращает существительное с падежным окончанием, которое соответствует числу</returns>
    private static string Case<TNumber>(TNumber number, string one, string two, string five)
        where TNumber : INumber<TNumber>
    {
        var tenNumber = TNumber.CreateTruncating(10);
        var twentyNumber = TNumber.CreateTruncating(20);
        var hundredNumber = TNumber.CreateTruncating(100);

        var value = number % hundredNumber > twentyNumber
            ? number % tenNumber
            : number % twentyNumber;

        switch (value)
        {
            case 1:
                return one;
            case 2:
            case 3:
            case 4:
                return two;
            default:
                return five;
        }
    }
    /// <summary>
    /// Перевод числа в строку
    /// </summary>
    /// <param name="value">Число</param>
    /// <returns>Возвращает строковую запись числа</returns>
    public static string Beautify<TNumber>(this TNumber value)
        where TNumber : INumber<TNumber>
    {
        value = TNumber.CreateTruncating(value);

        var thousand = TNumber.CreateTruncating(1000);

        var minus = value < TNumber.Zero;

        var builder = new StringBuilder();

        if (TNumber.Zero == value)
        {
            builder.Append("0 ");
        }

        if (value % thousand != TNumber.Zero)
        {
            builder.Insert(0, ToString(value, true, string.Empty, string.Empty, string.Empty));
        }

        value /= thousand;
        builder.Insert(0, ToString(value, false, "тысяча", "тысячи", "тысяч"));

        value /= thousand;
        builder.Insert(0, ToString(value, true, "миллион", "миллиона", "миллионов"));

        value /= thousand;
        builder.Insert(0, ToString(value, true, "миллиард", "миллиарда", "миллиардов"));

        value /= thousand;
        builder.Insert(0, ToString(value, true, "триллион", "триллиона", "триллионов"));

        if (minus)
        {
            builder.Insert(0, "минус ");
        }

        return builder.ToString().Trim();
    }
}
}