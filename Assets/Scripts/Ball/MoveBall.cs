using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    [SerializeField] private float dir = 150f;
    [SerializeField]private Rigidbody2D rb;
    public bool onlyOnce = false;
   


    private Transform myParent;
    private Vector3 initPosition;
    private GameManager gameManager;

    [SerializeField] private float paddleBlindSpot = 0.2f;

    [SerializeField] private float vForceMin = 0.6f;
    [SerializeField] private float vForceMinMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        gameManager = GameManager.instance;
        initPosition = transform.localPosition;
        myParent = transform.parent;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            gameManager.Death();
        }
        else if(collision.gameObject.tag == "Player")
        {
            float diffx = transform.position.x - collision.transform.position.x;
            if (diffx < -paddleBlindSpot)
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(-dir, dir));
            }
            else if(diffx > paddleBlindSpot)
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(dir, dir));
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(Mathf.Abs(rb.velocity.y) < vForceMin)
        {
            float vecX = rb.velocity.x;
            if (rb.velocity.y <0)
            {
                rb.velocity = new Vector2(vecX, -vForceMin * vForceMinMultiplier);
            }
            else
            {
                rb.velocity = new Vector2(vecX, vForceMin * vForceMinMultiplier);
            }
        }
    }

    public void Init()
    {
        transform.parent = myParent;
        transform.localPosition = initPosition;
        rb.simulated = false;
        rb.velocity = new Vector2(0, 0);
        onlyOnce = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Jump") && !onlyOnce)
        {
            onlyOnce = true;
            rb.simulated = true;
            rb.transform.parent = null;
            rb.AddForce(new Vector2(-dir, dir));
            
        }
    }
    public void ExtraBall()
    {
        transform.parent = myParent;
        transform.localPosition = initPosition;
        rb.simulated = false;
        rb.velocity = new Vector2(0, 0);
        onlyOnce = false;
    }
    

}
