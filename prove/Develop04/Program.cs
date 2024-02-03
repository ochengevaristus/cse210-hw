using System;
using System.Threading;

public abstract class MindfulnessActivity
{
    protected string activityName;
    protected string activityDescription;
    protected int durationInSeconds;

    public MindfulnessActivity(string name, string description)
    {
        activityName = name;
        activityDescription = description;
    }

    public void StartActivity()
    {
        DisplayStartingMessage();
        Thread.Sleep(3000);
        PerformActivity();
        DisplayEndingMessage();
    }

    protected void DisplayStartingMessage()
    {
        Console.WriteLine($"Starting {activityName} activity:");
        Console.WriteLine(activityDescription);
        SetDuration();
        Console.WriteLine("Get ready to begin...");
        Thread.Sleep(3000); 
    }

    protected abstract void PerformActivity();

    protected void DisplayEndingMessage()
    {
        Console.WriteLine("Good job!");
        Console.WriteLine($"You have completed the {activityName} activity for {durationInSeconds} seconds.");
        Thread.Sleep(3000); 
    }

    private void SetDuration()
    {
        Console.Write("Enter the duration (in seconds): ");
        while (!int.TryParse(Console.ReadLine(), out durationInSeconds) || durationInSeconds <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer for duration.");
            Console.Write("Enter the duration (in seconds): ");
        }
    }
}

public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Breathe in...");
        Thread.Sleep(durationInSeconds * 500); 
        Console.WriteLine("Breathe out...");
        Thread.Sleep(durationInSeconds * 500);
    }
}

public class ReflectionActivity : MindfulnessActivity
{
    private readonly string[] reflectionPrompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();
        string prompt = reflectionPrompts[random.Next(reflectionPrompts.Length)];
        Console.WriteLine(prompt);

        foreach (string question in GetReflectionQuestions())
        {
            Console.WriteLine(question);
            Thread.Sleep(3000); 
        }
    }

    private string[] GetReflectionQuestions()
    {
        return new string[]
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
    }
}

public class ListingActivity : MindfulnessActivity
{
    private readonly string[] listingPrompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();
        string prompt = listingPrompts[random.Next(listingPrompts.Length)];
        Console.WriteLine(prompt);

        Console.WriteLine($"You have {durationInSeconds} seconds to start listing...");

        Thread.Sleep(durationInSeconds * 1000);

    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice (1-4): ");
            int choice;

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                Console.Write("Enter your choice (1-4): ");
            }

            if (choice == 4)
            {
                break;
            }

            MindfulnessActivity activity;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    break;
                case 3:
                    activity = new ListingActivity();
                    break;
                default:
                    activity = null;
                    break;
            }

            if (activity != null)
            {
                activity.StartActivity();
            }
        }
    }
}
