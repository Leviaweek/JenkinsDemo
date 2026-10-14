using System.Text.RegularExpressions;

namespace TextToolkit;

/// <summary>
/// Небольшой набор функций для анализа текста.
/// "Слово" - это непрерывная последовательность букв и цифр (любой алфавит).
/// </summary>
public static partial class TextAnalyzer
{
    [GeneratedRegex(@"[\p{L}\p{N}]+")]
    private static partial Regex WordRegex();

    /// <summary>Возвращает количество слов в тексте.</summary>
    public static int CountWords(string? text)
    {
        return ExtractWords(text).Count();
    }

    /// <summary>
    /// Проверяет, является ли текст палиндромом (без учёта регистра,
    /// пробелов и знаков препинания). Пустой текст палиндромом не считается.
    /// </summary>
    public static bool IsPalindrome(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        var chars = text
            .Where(char.IsLetterOrDigit)
            .Select(char.ToLowerInvariant)
            .ToArray();

        if (chars.Length == 0)
        {
            return false;
        }

        for (int i = 0, j = chars.Length - 1; i < j; i++, j--)
        {
            if (chars[i] != chars[j])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>Переставляет слова в обратном порядке ("a b c" -> "c b a").</summary>
    public static string ReverseWords(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        var parts = text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        Array.Reverse(parts);
        return string.Join(' ', parts);
    }

    /// <summary>Считает частоту слов без учёта регистра (ключи - в нижнем регистре).</summary>
    public static IReadOnlyDictionary<string, int> WordFrequency(string? text)
    {
        return ExtractWords(text)
            .GroupBy(w => w, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key.ToLowerInvariant(), g => g.Count());
    }

    /// <summary>Превращает текст в slug для URL: "Hello, World!" -> "hello-world".</summary>
    public static string ToSlug(string? text)
    {
        return string.Join('-', ExtractWords(text).Select(w => w.ToLowerInvariant()));
    }

    private static IEnumerable<string> ExtractWords(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return [];
        }

        return WordRegex().Matches(text).Select(m => m.Value);
    }
}
