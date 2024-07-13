using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HeroObserver))]
public class HeroObserverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        HeroObserver hero = (HeroObserver)target;

        if (GUILayout.Button("Increase Health"))
        {
            hero.Health.Value += 10;
        }
        if (GUILayout.Button("Decrease Health"))
        {
            hero.Health.Value -= 10;
        }

        if (GUILayout.Button("Add Debugger"))
        {
            hero.Health.AddListener(Debug);
        }
        if (GUILayout.Button("Remove Debugger"))
        {
            hero.Health.RemoveListener(Debug);
        }
    }
    public void Debug(int value)
    {
        UnityEngine.Debug.Log($"Value changed to {value}");
    }
}
