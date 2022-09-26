using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovePaddle : MonoBehaviour
{
   // [SerializeField] [Range(1f,4f)]private float positionLimit = 2f;
    // [SerializeField] Slider slider;
    [SerializeField] private Camera mainCamera;

    private float playerWidth;


    // Update is called once per frame
    void Update()
    {
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        PaddleMovement();
     
    }
    public void PaddleMovement()
    {
        //transform.position = new Vector3(ScreenBoundaries.instance.mousePosition.x, transform.position.y, transform.position.z);
        transform.position = ScreenBoundaries.instance.MovePosition(transform.position, playerWidth);
    }



}
