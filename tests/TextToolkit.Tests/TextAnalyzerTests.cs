namespace TextToolkit.Tests;

public class TextAnalyzerTests
{
    [Theory]
    [InlineData(null, 1)]
    [InlineData("", 0)]
    [InlineData("   ", 0)]
    [InlineData("hello", 1)]
    [InlineData("Hello, world!", 2)]
    [InlineData("one  two\tthree\nfour", 4)]
    [InlineData("Привіт, світе! 2026", 3)]
    public void CountWords_ReturnsExpectedCount(string? text, int expected)
    {
        Assert.Equal(expected, TextAnalyzer.CountWords(text));
    }

    [Theory]
    [InlineData("level")]
    [InlineData("A man, a plan, a canal: Panama")]
    [InlineData("Never odd or even")]
    [InlineData("12321")]
    [InlineData("А роза упала на лапу Азора")]
    public void IsPalindrome_ReturnsTrue_ForPalindromes(string text)
    {
        Assert.True(TextAnalyzer.IsPalindrome(text));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("!!!")]
    [InlineData("hello")]
    [InlineData("ab")]
    public void IsPalindrome_ReturnsFalse_ForNonPalindromesAndEmptyInput(string? text)
    {
        Assert.False(TextAnalyzer.IsPalindrome(text));
    }

    [Theory]
    [InlineData("hello big world", "world big hello")]
    [InlineData("  one   two  ", "two one")]
    [InlineData("single", "single")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void ReverseWords_ReversesOrderOfWords(string? text, string expected)
    {
        Assert.Equal(expected, TextAnalyzer.ReverseWords(text));
    }

    [Fact]
    public void WordFrequency_IsCaseInsensitive()
    {
        var result = TextAnalyzer.WordFrequency("the cat and The dog and THE bird");

        Assert.Equal(5, result.Count);
        Assert.Equal(3, result["the"]);
        Assert.Equal(2, result["and"]);
        Assert.Equal(1, result["cat"]);
        Assert.Equal(1, result["dog"]);
        Assert.Equal(1, result["bird"]);
    }

    [Fact]
    public void WordFrequency_ReturnsEmpty_ForEmptyInput()
    {
        Assert.Empty(TextAnalyzer.WordFrequency(null));
        Assert.Empty(TextAnalyzer.WordFrequency("   "));
    }

    [Theory]
    [InlineData("Hello, World!", "hello-world")]
    [InlineData("  Multiple   spaces  ", "multiple-spaces")]
    [InlineData("C# 14 is out", "c-14-is-out")]
    [InlineData("Привіт, Світе!", "привіт-світе")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void ToSlug_BuildsLowercaseHyphenatedString(string? text, string expected)
    {
        Assert.Equal(expected, TextAnalyzer.ToSlug(text));
    }
}
