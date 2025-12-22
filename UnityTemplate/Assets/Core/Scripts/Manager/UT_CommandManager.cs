using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UT_CommandManager : UT_Manager
{
    public override string ManagerName => "Command Manager";

    private readonly UT_ICoroutineService _CoroutineSercive;
    private readonly ConcurrentQueue<UT_Command> _CommandQueue = new();

    public UT_CommandManager(UT_ICoroutineService InCoroutineService)
    {
        _CoroutineSercive = InCoroutineService;
    }

    public override Task Initialize()
    {
        _CommandQueue.Clear();

        return Task.CompletedTask;
    }

    public override void Destroy()
    {
        _CommandQueue.Clear();
    }

    public void PushCommand(UT_Command Command)
    {
        if (Command == null)
            return;

        _CommandQueue.Enqueue(Command);
    }

    public void Update()
    {
        while (_CommandQueue.TryDequeue(out UT_Command Command))
        {
            _CoroutineSercive.RunCoroutine(Command.Execute());
        }
    }
}