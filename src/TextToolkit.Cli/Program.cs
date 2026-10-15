using TextToolkit;

// Использование: TextToolkit.Cli "любой текст"  либо  echo "текст" | TextToolkit.Cli
var text = args.Length > 0 ? string.Join(' ', args) : Console.In.ReadToEnd();

Console.WriteLine($"Words:      {TextAnalyzer.CountWords(text)}");
Console.WriteLine($"Palindrome: {TextAnalyzer.IsPalindrome(text)}");
Console.WriteLine($"Reversed:   {TextAnalyzer.ReverseWords(text)}");
Console.WriteLine($"Slug:       {TextAnalyzer.ToSlug(text)}");
Console.WriteLine("Frequency:");

foreach (var pair in TextAnalyzer.WordFrequency(text).OrderByDescending(p => p.Value).ThenBy(p => p.Key))
{
    Console.WriteLine($"  {pair.Key} = {pair.Value}");
}
