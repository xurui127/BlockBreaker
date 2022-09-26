using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] private float dir = 150f;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private BallManager ballManager;
    [SerializeField] private SpriteRenderer sprite;
    private TransationEffect transationEffect; 
    public bool onlyOnce = false;


    [SerializeField] private float duration;

    [SerializeField] private Transform myParent;
    [SerializeField] private Vector3 initPosition;
    private GameManager gameManager;
    [SerializeField] private float paddleBlindSpot = 0.2f;
    [SerializeField] private float vForceMin = 0.6f;
    [SerializeField] private float vForceMinMultiplier = 4f;



    // Control ball scale  when player get scale buffe 
    [SerializeField] private Vector2 normalScale;
    [SerializeField] private Vector2 megaScale;
    [SerializeField] private Vector2 tinyScale;
    private Vector2 curSpeed;


    // Control ball speed when player get speed buffe 
    public float ballSpeed = 1f;


    // Audio Source  
    [SerializeField] private AudioSource[] audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        //rb.simulated = false;
        gameManager = GameManager.instance;
        ballManager = BallManager.instance;
        transationEffect = TransationEffect.instance;
        initPosition = transform.localPosition;
    }
    private void OnEnable()
    {
        rb.simulated = true;
    }
    private void Awake()
    {
        //rb.velocity = new Vector2(2f, 10f);
        // rb.AddForce(new Vector2(-dir, dir));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            if (gameManager.sumBall > 1)
            {
                gameManager.sumBall--;
                transform.parent = myParent;
                this.gameObject.SetActive(false);
            }
            else
            {
                gameManager.Death();
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            float diffx = transform.position.x - collision.transform.position.x;
            if (diffx < -paddleBlindSpot)
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(-dir * ballSpeed, dir * ballSpeed));


            }
            else if (diffx > paddleBlindSpot)
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(dir * ballSpeed, dir * ballSpeed));
            }


            //gameManager.timeLeft += gameManager.timeLeft * 0.5f;
            gameManager.TimerBoundary(0.5f);
            gameManager.IsShakeCamera();
            audioSource[1].Play();

        }
        else if (collision.gameObject.tag == "bricks" || collision.gameObject.tag == "board" || collision.gameObject.tag == "Player")
        {
            gameManager.IsShakeCamera();
           audioSource[0].Play();
        }
       

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Mathf.Abs(rb.velocity.y) < vForceMin)
        {
            float vecX = rb.velocity.x;
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(vecX * ballSpeed, -vForceMin * vForceMinMultiplier * ballSpeed);

            }
            else
            {
                rb.velocity = new Vector2(vecX * ballSpeed, vForceMin * vForceMinMultiplier * ballSpeed);

            }
        }

    }

    public void Init()
    {
        gameObject.SetActive(true);
        transform.parent = myParent;
        transform.localPosition = initPosition;
        rb.simulated = false;
        rb.velocity = new Vector2(0, 0);
        onlyOnce = false;
    }
    public void ActiveBall()
    {
        gameObject.SetActive(true);
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
            curSpeed = rb.velocity;
            gameManager.timerOn = true;
        }
        
    }
    public void InitExtraBall()
    {
        onlyOnce = true;
        rb.simulated = true;
        float randomX = Random.Range(2f, 5f);
        float randomY = Random.Range(2f, 5f);
        rb.velocity = new Vector2(randomX, randomY);
    }
    public void MegaBall()
    {
        StartCoroutine(nameof(ChangeToMegaBall));
        //sprite.size = megaScale;
  
    }
    public void TinyBall()
    {
        //sprite.size = tinyScale;
        StartCoroutine(nameof(ChangeToTinyBall));
    }

    public void NormalBall()
    {
        StartCoroutine(nameof(ChangeToNormalBall));
    }
    private IEnumerator ChangeToMegaBall()
    {
        while(sprite.size.magnitude < megaScale.magnitude)
        {
            sprite.size += new Vector2(2f, 2f) * Time.deltaTime;
            yield return StartCoroutine(transationEffect.TransformPause(duration));
            
            yield return null;
            //sprite.color = Color.Lerp(new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1),
            //                          new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f),
            //                          Mathf.Sin(Time.time * 5));
            //yield return new WaitForEndOfFrame();
            //sprite.color = new Color(255f, 255, 0, 255);
        }
        StopCoroutine(nameof(ChangeToMegaBall));
    }
    private IEnumerator ChangeToTinyBall()
    {
        while (sprite.size.magnitude > tinyScale.magnitude)
        {
            sprite.size -= new Vector2(2f, 2f) * Time.deltaTime;
            yield return StartCoroutine(transationEffect.TransformPause(duration));
            yield return null;

        }
        StopCoroutine(nameof(ChangeToTinyBall));

    }
    private IEnumerator ChangeToNormalBall()
    {
        if (sprite.size.magnitude > normalScale.magnitude)
        {
            while (sprite.size.magnitude > normalScale.magnitude)
            {
                sprite.size -= new Vector2(2f, 2f) * Time.deltaTime;
                yield return StartCoroutine(transationEffect.TransformPause(duration));
                yield return null;

            }
            StopCoroutine(nameof(ChangeToNormalBall));
        }
        else
        {
            while (sprite.size.magnitude < normalScale.magnitude)
            {
                sprite.size += new Vector2(2f, 2f) * Time.deltaTime;
                yield return StartCoroutine(transationEffect.TransformPause(duration));
                yield return null;

            }
            StopCoroutine(nameof(ChangeToNormalBall));
        }
        
    }

    public void SpeedBall()
    {
        ballSpeed = 2f;
        SetBallSpeed(ballSpeed);
    }
    public void ResetBall()
    {
        StopAllCoroutines();
        sprite.size = normalScale;
        ballSpeed = 1f;
        SetBallSpeed(ballSpeed);
    }


    public void MultiBalls()
    {
        //gameManager.isBuffing = true;
        ballManager.ReleaseBall(this.transform);
    }
    private void SetBallSpeed(float ballspeed)
    {
        float curX = rb.velocity.x;
        float cury = rb.velocity.y;
        rb.velocity = new Vector2(curX * ballspeed, cury * ballspeed);
    }
}
