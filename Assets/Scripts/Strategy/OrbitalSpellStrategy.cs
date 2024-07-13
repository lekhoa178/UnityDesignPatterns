using DG.Tweening;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitalSpellStrategy", menuName = "Spells/OrbitalSpellStrategy")]
public class OrbitalSpellStrategy : SpellStrategy
{
    public GameObject orbPrefab;
    public int numberOfOrbs = 3;
    public float radius = 3f;
    public float rotationSpeed = 0.3f;
    public float duration = 5f;

    public override void CastSpell(Transform origin)
    {
        Transform orbParent = CreateOrbParent(origin);
        RotateOrbParent(orbParent);

        for (int i = 0; i < numberOfOrbs; ++i)
        {
            SpawnOrb(origin, orbParent, i);
        }
    }

    void SpawnOrb(Transform origin, Transform orbParent, int i)
    {
        Instantiate(orbPrefab, CalculateSpawnPosition(origin, i), Quaternion.identity, orbParent);
    }

    Vector3 CalculateSpawnPosition(Transform origin, int i)
    {
        float angle = i * 2f * Mathf.PI / (float)numberOfOrbs;
        return origin.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
    }

    void RotateOrbParent(Transform orbParent)
    {
        float rotationRate = 360f * rotationSpeed;
        DOTween.To(() => orbParent.rotation.eulerAngles, x => orbParent.rotation = Quaternion.Euler(x), new Vector3(0, rotationRate, 0), duration = 1f)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    Transform CreateOrbParent(Transform origin)
    {
        Transform orbParent = new GameObject("OrbParent").transform;
        orbParent.position = origin.position;
        orbParent.rotation = origin.rotation;
        orbParent.SetParent(origin);
        return orbParent;
    }
}