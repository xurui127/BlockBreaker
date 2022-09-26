using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlashing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainHighScoreTxt;
    private GameManager manager;
    public void Start()
    {
        manager = GameManager.instance;
    }
    public void Update()
    {
        //if (manager.highScore > manager.points)
        //{
        //    mainHighScoreTxt.color= FlashText();
        //}
    }
    public Color FlashText()
    {
       
      return   Color.Lerp(new Color(mainHighScoreTxt.color.r, mainHighScoreTxt.color.g, mainHighScoreTxt.color.b, 1),
                          new Color(mainHighScoreTxt.color.r, mainHighScoreTxt.color.g, mainHighScoreTxt.color.b, 0), 
                          Mathf.Sin(Time.time*4f));
    }   
}
