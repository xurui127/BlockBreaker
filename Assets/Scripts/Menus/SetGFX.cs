using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SetGFX : MonoBehaviour
{
    [SerializeField] private Text txtGFX;

    private Slider slider;
    private string[] _GFXName;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        _GFXName = QualitySettings.names;
        float v = QualitySettings.GetQualityLevel();
        slider.maxValue = _GFXName.Length - 1;
        slider.value = v;
        txtGFX.text = _GFXName[(int)v];
    }
    public void SetGfx(float val)
    {
        int v = (int)Mathf.Floor(val);
        slider.value = val;
        QualitySettings.SetQualityLevel(v, true);
        txtGFX.text = _GFXName[v];
    }
}
