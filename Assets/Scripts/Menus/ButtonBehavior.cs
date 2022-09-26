
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    GameManager manager;


    private void OnEnable()
    {
        manager = GameManager.instance;

    }
    public void Retry()
    {
        manager.ReStart();
    }
    public void StartOver()
    {
        manager.StartOver();
    }
}
