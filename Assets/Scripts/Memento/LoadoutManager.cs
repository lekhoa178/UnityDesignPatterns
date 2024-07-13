using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutManager : MonoBehaviour
{
    [SerializeField, Required] HotBar hotBar;
    [SerializeField] Button[] loadoutButtons;
    [SerializeField] Button saveButton;

    readonly HotBar.Memento[] loadoutMementos = new HotBar.Memento[3];
    int selectedLoadout = 0;

    private void Start()
    {
        for (int i = 0; i <= loadoutMementos.Length; ++i)
        {
            loadoutMementos[i] = hotBar.CreateMemento();
            int index = i;
            loadoutButtons[i].onClick.AddListener(() => SelectLoadout(index));
        }

        saveButton.onClick.AddListener(SaveLoadout);

        AdjustButtonColors();
    }

    private void SelectLoadout(int index)
    {
        SaveLoadout();
        selectedLoadout = index;
        hotBar.SetMemento(loadoutMementos[index]);

        AdjustButtonColors();
    }

    void SaveLoadout()
    {
        loadoutMementos[selectedLoadout] = hotBar.CreateMemento();
    }

    void AdjustButtonColors()
    {

    }
}