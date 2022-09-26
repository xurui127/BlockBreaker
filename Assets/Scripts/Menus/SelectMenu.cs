using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private Selectable[] defaultBtn;
    [SerializeField] private GameObject[] pannel;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.01f);

    }
    public void PanelToggle(int pos)
    {
        Input.ResetInputAxes();
        for (int i = 0; i < pannel.Length; i++)
        {
            pannel[i].SetActive(pos == i);
            if (pos == i)
            {
                defaultBtn[i].Select();
            }
        }
    }
    public void OpenPannle(int pos)
    {
        pannel[pos].SetActive(true);
    }
    public void ClossPanel(int pos)
    {
        pannel[pos].SetActive(false);
    }

    public void GamePause()
    {
        Time.timeScale = 0;
    }
    public void GameResume()
    {
        Time.timeScale = 1;
    }
    public void SavePrefs()
    {
        PlayerPrefs.Save();

    }
        
}

