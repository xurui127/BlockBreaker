using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicHadler : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM;
    [SerializeField] private string[] nameParam;
    // Start is called before the first frame update
    void Start()
    {
        GetParams();
    }

    private void GetParams()
    {
        for(int i = 0; i < nameParam.Length; i++)
        {
            float v = PlayerPrefs.GetFloat(nameParam[i]);
            audioM.SetFloat(nameParam[i], v);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
