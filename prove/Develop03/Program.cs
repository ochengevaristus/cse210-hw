using System;
using System.Collections.Generic;

public class ScriptureWord
{
    private string _word;
    private bool _isHidden;

    public string Word
    {
        get { return _word; }
        set { _word = value; }
    }

    public bool IsHidden
    {
        get { return _isHidden; }
        set { _isHidden = value; }
    }

    public ScriptureWord(string word)
    {
        _word = word;
        _isHidden = false;
    }
}

public class ScriptureReference
{
    private string _book;
    private int _chapter;
    private int _verseStart;
    private int _verseEnd;

    public string Book
    {
        get { return _book; }
    }

    public int Chapter
    {
        get { return _chapter; }
    }

    public int VerseStart
    {
        get { return _verseStart; }
    }

    public int VerseEnd
    {
        get { return _verseEnd; }
    }

    public ScriptureReference(string reference)
    {
        string[] parts = reference.Split(':');
        if (parts.Length == 2)
        {
            _book = parts[0];
            string[] verseParts = parts[1].Split('-');
            if (verseParts.Length == 1)
            {
                _verseStart = _verseEnd = int.Parse(verseParts[0]);
            }
            else if (verseParts.Length == 2)
            {
                _verseStart = int.Parse(verseParts[0]);
                _verseEnd = int.Parse(verseParts[1]);
            }
        }
    }
}

public class Scripture
{
    private readonly List<ScriptureWord> _words;
    private int _hiddenWordCount;
    private readonly ScriptureReference _scriptureReference;

    public Scripture(string reference, string text)
    {
        _scriptureReference = new ScriptureReference(reference);

        _words = new List<ScriptureWord>();
        string[] wordArray = text.Split(' ');
        foreach (string word in wordArray)
        {
            _words.Add(new ScriptureWord(word));
        }
    }

    public void Display()
    {
        Console.WriteLine($"Scripture Reference: {_scriptureReference.Book} {_scriptureReference.Chapter}:{_scriptureReference.VerseStart}-{_scriptureReference.VerseEnd}\n");

        foreach (ScriptureWord word in _words)
        {
            Console.Write(word.IsHidden ? "____ " : $"{word.Word} ");
        }

        Console.WriteLine("\n");
    }

    public void HideRandomWord()
    {
        Random random = new Random();
        int randomIndex = random.Next(_words.Count);
        if (!_words[randomIndex].IsHidden)
        {
            _words[randomIndex].IsHidden = true;
            _hiddenWordCount++;
        }
    }

    public bool AllWordsHidden()
    {
        return _hiddenWordCount == _words.Count;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter scripture reference:");
        string reference = Console.ReadLine();

        Console.WriteLine("Enter scripture text:");
        string text = Console.ReadLine();

        Scripture scripture = new Scripture(reference, text);

        do
        {
            Console.Clear();
            scripture.Display();

            Console.WriteLine("Press Enter to continue or type 'quit' to end.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWord();

        } while (!scripture.AllWordsHidden());
    }
}
