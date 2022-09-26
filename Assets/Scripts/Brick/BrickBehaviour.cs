using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrickBehaviour : BrickBehaviourBase
{

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        BallOnExit();
    }

    protected override void BallOnExit()
    {
        manager.ReduceBrick();
        manager.AddPoints();
        manager.GoNextLevel();
        manager.InitBuffer(transform);
        Destroy(gameObject);
    }
}
