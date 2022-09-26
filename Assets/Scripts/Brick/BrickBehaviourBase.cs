using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BrickBehaviourBase : MonoBehaviour
{
    protected GameManager manager;
    [SerializeField]protected int hitTimes;
    protected void Start()
    {
        manager = GameManager.instance;
        manager.AddBrick();
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {

    }
    protected virtual void BallOnExit()
    {

    }
}
