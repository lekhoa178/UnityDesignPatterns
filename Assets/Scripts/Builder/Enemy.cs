using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name { get; private set; }
    public float Health { get; private set; }
    public float Speed { get; private set; }
    public float Damage { get; private set; }
    public bool IsBoss { get; private set; }

    public class Builder
    {
        string name = "Knight";
        float health = 1f;
        float speed = 1f;
        float damage = 1f;
        bool isBoss = false;

        public Builder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public Builder WithHealth(float health)
        {
            this.health = health;
            return this;
        }

        public Builder WithSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }

        public Builder WithDamage(float damage)
        {
            this.damage = damage;
            return this;
        }

        public Builder WithIsBoss(bool isBoss)
        {
            this.isBoss = isBoss;
            return this;
        }

        public Enemy Build()
        {
            Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();
            enemy.Name = name;
            enemy.Health = health;
            enemy.Speed = speed;
            enemy.Damage = damage;
            enemy.IsBoss = isBoss;

            return enemy;
        }
    }
}