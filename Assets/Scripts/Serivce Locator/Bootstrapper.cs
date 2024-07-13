using UnityEngine;
using UnityUtils;

namespace UnityServiceLocator
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ServiceLocator))]
    public abstract class Bootstrapper : MonoBehaviour
    {
        ServiceLocator container;
        internal ServiceLocator Container => container.OrNull() ?? (container = GetComponent<ServiceLocator>());

        bool hasBeenBoostrapped;

        private void Awake() => BootstrapOnDemand();

        public void BootstrapOnDemand()
        {
            if (hasBeenBoostrapped) return;

            hasBeenBoostrapped = true;
            Bootstrap();
        }

        protected abstract void Bootstrap();
    }
}