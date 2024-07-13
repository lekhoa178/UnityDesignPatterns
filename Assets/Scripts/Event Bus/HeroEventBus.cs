using UnityEngine;
using UnityUtils;

public class HeroEventBus : MonoBehaviour
{
    HealthComponent healthComponent;
    ManaComponent manaComponent;

    EventBinding<TestEvent> testEventBinding;
    EventBinding<PlayerEvent> playerEventBinding;

    private void Awake()
    {
        healthComponent = gameObject.GetOrAdd<HealthComponent>();
        manaComponent = gameObject.GetOrAdd<ManaComponent>();
    }

    private void OnEnable()
    {
        testEventBinding = new EventBinding<TestEvent>(HandleTestEvent);
        EventBus<TestEvent>.Register(testEventBinding);

        playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerEvent);
        EventBus<PlayerEvent>.Register(playerEventBinding);
    }

    private void OnDisable()
    {
        EventBus<TestEvent>.Deregister(testEventBinding);
        EventBus<PlayerEvent>.Deregister(playerEventBinding);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventBus<TestEvent>.Raise(new TestEvent());
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            EventBus<PlayerEvent>.Raise(new PlayerEvent
            {
                health = healthComponent.Health,
                mana = manaComponent.Mana,
            });
        }
    }

    void HandleTestEvent(TestEvent testEvent)
    {
        Debug.Log("Test event received");
    }

    void HandlePlayerEvent(PlayerEvent playerEvent)
    {
        Debug.Log($"Health: {playerEvent.health} | Mana: {playerEvent.mana}");
    }
}