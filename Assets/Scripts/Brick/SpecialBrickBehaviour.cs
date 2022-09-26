using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBrickBehaviour : BrickBehaviourBase
{
    [SerializeField]private SpriteRenderer spriteRenderer;
    protected override void OnCollisionExit2D(Collision2D collision)
    {

        BallOnExit();

    }
    /// <summary>
    /// TODO :  FIX brick counter bug 
    /// </summary>
    protected override void BallOnExit()
    {
        hitTimes--;
        manager.AddPoints();
        //manager.ReduceBrick();
        if (hitTimes == 1)
        {
            spriteRenderer.color = Color.yellow;
        }
       
        else if (hitTimes == 0)
        {
            
            manager.ReduceBrick();
            manager.GoNextLevel();
            manager.InitBuffer(transform);
            Destroy(gameObject);
        }
    }

}
