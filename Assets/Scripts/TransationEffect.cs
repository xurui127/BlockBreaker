using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransationEffect : MonoBehaviour
{
    public static TransationEffect instance;
    private bool isShaking;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    public IEnumerator TransformPause(float duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

    public void ShakeCaemera(float duration, float strength)
    {
        if (!isShaking)
        {
            StartCoroutine(CameraShake(duration, strength));
        }
    }
    private IEnumerator CameraShake(float duration, float strength)
    {
        isShaking = true;
        Transform camera = Camera.main.transform;
        Vector3 startPosition = camera.position;
        while (duration > 0)
        {
            camera.position = Random.insideUnitSphere * strength + startPosition;
            duration -= Time.deltaTime;
            yield return null;
        }
        isShaking = false;
    }
}
