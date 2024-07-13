using UnityEngine;
using UnityUtils;

[CreateAssetMenu(fileName = "ProjectileSpellStrategy", menuName = "Spells/ProjectileSpellStrategy")]
public class ProjectileSpellStrategy : SpellStrategy
{
    public GameObject projectilePrefab;
    public float speed = 10f;
    public float duration = 10f;

    public override void CastSpell(Transform origin)
    {
        new ProjectileBuilder()
            .WithProjectilePrefab(projectilePrefab)
            .WithSpeed(speed)
            .WithDuration(duration)
            .Build(origin);
    }

    public class ProjectileBuilder
    {
        private GameObject _projectilePrefab;
        private float _speed;
        private float _duration;

        public ProjectileBuilder WithProjectilePrefab(GameObject projectilePrefab)
        {
            _projectilePrefab = projectilePrefab;
            return this;
        }

        public ProjectileBuilder WithSpeed(float speed)
        {
            _speed = speed;
            return this;
        }

        public ProjectileBuilder WithDuration(float duration)
        {
            _duration = duration;
            return this;
        }

        public GameObject Build(Transform origin)
        {
            Vector3 instantiatePosition = origin.position + origin.forward * 2;

            GameObject fireball = Instantiate(_projectilePrefab, instantiatePosition.With(y: 1), Quaternion.identity, origin);
            //ParticleMover mover = fireball.GetOrAdd<ParticleMover>();
            //SelfDestruct selfDestruct = fireball.GetOrAdd<SelfDestruct>();

            //mover.Initialize(speed);
            //selfDestruct.Initialize(duration: 5f);

            return fireball;
        }
    }
}