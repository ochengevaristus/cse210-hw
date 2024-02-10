using System;
using System.Collections.Generic;
using System.IO;


abstract class Goal
{
    protected string _name;
    protected bool _isCompleted;
    protected int _points;

    public string Name 
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Points
    {
        get { return _points; }
        protected set { _points = value; }
    }

    public Goal(string name)
    {
        _name = name;
        _isCompleted = false;
        _points = 0;
    }

    public virtual void MarkCompleted()
    {
        _isCompleted = true;
    }

    public abstract void RecordEvent();

    public virtual string Display()
    {
        return $"{_name} - {(_isCompleted ? "[X]" : "[ ]")}";
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name)
    {
        _points = points;
    }

    public override void RecordEvent()
    {
        if (!_isCompleted)
        {
            _points += _points;
            _isCompleted = true;
        }
        else
        {
            Console.WriteLine("Goal already completed!");
        }
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name)
    {
        _points = points;
    }

    public override void RecordEvent()
    {
        _points += _points;
    }

    public override string Display()
    {
        return $"{_name} - Eternal";
    }
}

class ChecklistGoal : Goal
{
    private int _requiredCount;
    private int _completedCount;

    public ChecklistGoal(string name, int points, int requiredCount) : base(name)
    {
        _points = points;
        _requiredCount = requiredCount;
        _completedCount = 0;
    }

    public override void RecordEvent()
    {
        if (!_isCompleted)
        {
            _completedCount++;
            if (_completedCount == _requiredCount)
            {
                _points += 500;
                MarkCompleted();
            }
            else
            {
                _points += _points;
            }
        }
        else
        {
            Console.WriteLine("Goal already completed!");
        }
    }

    public override string Display()
    {
        return $"{_name} - Completed {_completedCount}/{_requiredCount} times";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Goal> goals = new List<Goal>();
        goals.Add(new SimpleGoal("Run a marathon", 1000));
        goals.Add(new EternalGoal("Read scriptures", 100));
        goals.Add(new ChecklistGoal("Attend the temple", 50, 10));

        goals[0].RecordEvent(); 
        goals[1].RecordEvent(); 
        goals[1].RecordEvent(); 
        goals[2].RecordEvent(); 

        foreach (var goal in goals)
        {
            Console.WriteLine(goal.Display());
            Console.WriteLine($"Points: {goal.Points}");
            Console.WriteLine();
        }
    }
}
