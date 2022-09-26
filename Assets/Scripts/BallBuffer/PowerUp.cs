using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   private  GameManager manager;

    private void Start()
    {
        manager = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            manager.ChangeStates();
            manager.BallBehaviours();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "death")
        {
            Destroy(gameObject);
        }
    }

}
