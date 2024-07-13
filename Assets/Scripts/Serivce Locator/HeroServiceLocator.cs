using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityServiceLocator;
using UnityUtils;

public class HeroServiceLocator : MonoBehaviour
{
    [SerializeField] GameObject powerOrbPrefab;
    [SerializeField] int powerOrbHeight = 1;

    // Consider making a 'Service Installer' component
    // that could be used at GameObject level, or scene level, or global level
    [Title("Registerd Serivces")]
    [SerializeField] List<Object> services;

    private void Awake()
    {
        ServiceLocator sl = ServiceLocator.For(this);
        foreach (Object service in services)
        {
            sl.Register(service.GetType(), service);
        }
    }

    private void Start()
    {
        GameObject powerOrb = Instantiate(powerOrbPrefab, transform.position.With(y: powerOrbHeight), Quaternion.identity);
        powerOrb.transform.SetParent(transform);
    }
}