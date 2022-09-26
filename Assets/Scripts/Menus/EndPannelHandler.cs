using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPannelHandler : MonoBehaviour
{
    GameManager manager;
    [SerializeField] private string preTextHightScore = "HIGH SCORE: ";
    [SerializeField] private Text highScoreTxt;
    void OnEnable()
    {
        manager = GameManager.instance;
        highScoreTxt.text = preTextHightScore + manager.LoadHightScore().ToString("D6");
    }

}
