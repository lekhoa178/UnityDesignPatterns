using Unity.VisualScripting;
using UnityEngine;

public class EnemyDirector
{
    EnemyBuilder builder;

    public EnemyDirector(EnemyBuilder builder)
    {
        this.builder = builder;
    }

    //public Enemy Construct(EnemyBuilder builder, EnemyData data)
    //{
        
    //}
}

public class EnemyBuilder
{
    Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();

    //public void AddWeaponStrategy(WeaponStategy strategy)
    //{
    //    enemy.AddComponent<WeaponComponent>().SetWeaponStrategy(strategy);
    //}

    public void AddHealthComponent()
    {
        //enemy.AddComponent<Health>();
    }

    public Enemy Build()
    {
        Enemy builtEnemy = enemy;
        enemy = new GameObject("Enemy").AddComponent<Enemy>();

        return builtEnemy;
    }
}