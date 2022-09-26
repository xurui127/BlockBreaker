using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    public static ScreenBoundaries instance;
    Vector3 screenBounds;
  
    
    

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
    // Start is called before the first frame update
    void Start()
    {
        screenBounds =Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
       
    }

    // Update is called once per frame
    void Update()
    {
    }
   public Vector3 MovePosition(Vector3 playerPosition , float playerWidth)
    {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Clamp(playerPosition.x, (screenBounds.x*-1) + playerWidth, screenBounds.x - playerWidth);
        position.y = playerPosition.y;
        position.z = playerPosition.z;
        return position;
    }
}
