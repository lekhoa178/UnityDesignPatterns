using UnityEngine;

public class HeroObserver : MonoBehaviour
{
    public Observer<int> Health = new Observer<int>(100);

    private void Start()
    {
        Health.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Health.Value += 10;
        }
    }
}
