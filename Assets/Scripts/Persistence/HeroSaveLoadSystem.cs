using System;
using Systems.Persistence;
using UnityEngine;
using UnityUtils;

namespace Systems
{
    public class HeroSaveLoadSystem : MonoBehaviour, IBind<PlayerData>
    {
        [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
        [SerializeField] public PlayerData data;

        public void Bind(PlayerData data)
        {
            this.data = data;
            this.data.Id = Id;
            transform.position = data.position;
            transform.rotation = data.rotation;
        }

        void Update()
        {
            data.position = transform.position;
            data.rotation = transform.rotation;
        }
    }

    [Serializable]
    public class PlayerData : ISaveable
    {
        [field: SerializeField] public SerializableGuid Id { get; set; }
        public Vector3 position;
        public Quaternion rotation;
    }
}