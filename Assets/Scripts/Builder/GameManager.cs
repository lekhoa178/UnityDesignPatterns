using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Start()
    {
        Enemy enemy = new Enemy.Builder()
                        .WithName("Goblin")
                        .WithHealth(10f)
                        .WithSpeed(1f)
                        .WithDamage(1f)
                        .Build();

        //Instantiate(player);
    }
}