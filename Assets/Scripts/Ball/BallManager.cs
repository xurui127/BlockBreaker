using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    public GameObject prabBall;
    public List<BallBehaviour> balls;
    [SerializeField] private GameObject parent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject copy = GameObject.Instantiate(prabBall);
            copy.transform.parent = this.transform;
            copy.SetActive(false);
            balls.Add(copy.GetComponent<BallBehaviour>());
        }



    }
    public void ReleaseBall(Transform parTransform)
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].gameObject.SetActive(true);
            balls[i].transform.parent = parent.transform;
            balls[i].transform.position = parTransform.position;
            //balls[i].ResetBall();
            balls[i].transform.localScale = prabBall.transform.localScale;
            balls[i].InitExtraBall();
        }
    }
    public void DestroyExtrlBall()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].gameObject.transform.parent = this.transform;
            balls[i].gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        Init();
    }



}
