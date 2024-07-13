using UnityEngine;
using UnityUtils;

[CreateAssetMenu(fileName = "RandomCircle", menuName = "Placements/RandomCircle")]
public class RandomCirclePlacer : PlacementStrategy
{
    public float minDistance;
    public float maxDistance = 10f;
    public override Vector3 SetPosition(Vector3 origin) => origin.RandomPointInAnnulus(minDistance, maxDistance);
}

[CreateAssetMenu(fileName = "DefaultPlacer", menuName = "Placements/DefaultPlacer")]
public class PlacementStrategy : ScriptableObject
{
    public virtual Vector3 SetPosition(Vector3 origin) => origin;
}