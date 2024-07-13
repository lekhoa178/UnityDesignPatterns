
using System.Threading.Tasks;

public abstract class HeroCommand : ICommand
{
    protected readonly IEntity hero;

    protected HeroCommand(IEntity hero)
    {
        this.hero = hero;
    }

    public abstract Task Execute();

    public static T Create<T>(IEntity hero) where T : HeroCommand
    {
        return (T)System.Activator.CreateInstance(typeof(T), hero);
    }
}

public class AttackCommand : HeroCommand
{
    public AttackCommand(IEntity hero) : base(hero) { }

    public override async Task Execute()
    {
        hero.Attack();
        //await Awaitable.WaitForSecondsAsync(time);
        //hero.Animations.Idle();
    }
}

public class SpinCommand : HeroCommand
{
    public SpinCommand(IEntity hero) : base(hero) { }

    public override async Task Execute()
    {
        hero.Spin();
        //await Awaitable.WaitForSecondsAsync(time);
        //hero.Animations.Spin();
    }
}

public class JumpCommand : HeroCommand
{
    public JumpCommand(IEntity hero) : base(hero) { }

    public override async Task Execute()
    {
        hero.Jump();
        //await Awaitable.WaitForSecondsAsync(time);
        //hero.Animations.Jump();
    }
}