using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CommandManager : SerializedMonoBehaviour
{
    public IEntity Entity;
    public List<ICommand> commands;

    readonly CommandInvoker commandInvoker = new();

    private void Start()
    {
        Entity = GetComponent<IEntity>();

        commands = new List<ICommand>
        {
            HeroCommand.Create<AttackCommand>(Entity),
            HeroCommand.Create<SpinCommand>(Entity),
            HeroCommand.Create<JumpCommand>(Entity)
        };
    }

    async Task ExecuteCommand(List<ICommand> commands)
    {
        await commandInvoker.ExecuteCommand(commands);
    }
}

public class CommandInvoker
{
    public async Task ExecuteCommand(List<ICommand> commands)
    {
        foreach (ICommand command in commands)
        {
            await command.Execute();
        }
    }
}