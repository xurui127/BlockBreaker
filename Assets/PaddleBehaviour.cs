using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{
    // [SerializeField] [Range(1f,4f)]private float positionLimit = 2f;
    // [SerializeField] Slider slider;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector2 paddlesize;
    [SerializeField] private Vector2 megaPaddle;
    [SerializeField] private Vector2 tinyPaddle;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private TransationEffect transationEffect;
    [SerializeField] private float duration;

    public Vector3 mousePosition;
    private float playerWidth;
    private GameManager gameManager;



    private void Start()
    {
        gameManager = GameManager.instance;
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        PaddleMovement();

    }
    public void PaddleMovement()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
        transform.position = ScreenBoundaries.instance.MovePosition(transform.position, playerWidth);
    }

    public void MegaPaddle()
    {
        //gameManager.isBuffing = true;
        //transform.localScale = new Vector3(0.5f, 0.3f, 0.3f);
        //sprite.size = megaPaddle;
        StartCoroutine(nameof(ChangeToMegaPaddle));
    }

    private IEnumerator ChangeToMegaPaddle()
    {
        while (sprite.size.magnitude < megaPaddle.magnitude)
        {
            sprite.size += new Vector2(2f, 0f) * Time.deltaTime;
            yield return StartCoroutine(transationEffect.TransformPause(duration));
            yield return null;
        }
        StopCoroutine(nameof(ChangeToMegaPaddle));
    }
    private IEnumerator ChangeToTinyPaddle()
    {
        while (sprite.size.magnitude > tinyPaddle.magnitude)
        {
            sprite.size -= new Vector2(2f, 0f) * Time.deltaTime;
            yield return StartCoroutine(transationEffect.TransformPause(duration));
            yield return null;
        }
        StopCoroutine(nameof(ChangeToTinyPaddle));
    }
    public void TinyPaddle()
    {
        //gameManager.isBuffing = true;
        // transform.localScale = new Vector3(0.2f, 0.3f, 0.3f);
        // sprite.size = tinyPaddle;
        StartCoroutine(nameof(ChangeToTinyPaddle));

    }
    public void NormalPaddle()
    {
        StartCoroutine(nameof(ChangeToNormalPaddle));
    }
    private IEnumerator ChangeToNormalPaddle()
    {
        if (sprite.size.magnitude > paddlesize.magnitude)
        {
            while (sprite.size.magnitude > paddlesize.magnitude)
            {
                sprite.size -= new Vector2(2f, 0f) * Time.deltaTime;
                yield return StartCoroutine(transationEffect.TransformPause(duration));
                yield return null;

            }
            StopCoroutine(nameof(ChangeToNormalPaddle));
        }
        else
        {
            while (sprite.size.magnitude < paddlesize.magnitude)
            {
                sprite.size += new Vector2(2f, 0f) * Time.deltaTime;
                yield return StartCoroutine(transationEffect.TransformPause(duration));
                yield return null;

            }
            StopCoroutine(nameof(ChangeToNormalPaddle));
        }

    }
    public void ResetPaddle()
    {
        //gameManager.isBuffing = true;
        //transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        StopAllCoroutines();
        sprite.size = paddlesize;
    }

}
