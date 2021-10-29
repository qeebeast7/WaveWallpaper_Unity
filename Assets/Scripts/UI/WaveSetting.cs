using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSetting : MonoBehaviour {
    private static WaveSetting instance;
    private WaveSetting() { }

    public static WaveSetting Instance
    {
        get
        {
            if (instance == null) instance = new WaveSetting();
            return instance;
        }
    }


    private Slider[] bars;
    private Button okBtn;
    private Button resetBtn;
    private float[] oris;
    private float[] updates;

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        bars = GetComponentsInChildren<Slider>();
        foreach (Slider bar in bars)
        {
            bar.onValueChanged.AddListener((float value) =>
            {
                UpdateLabel(bar,value);
            });
            UpdateLabel(bar,bar.value);
        }
        oris = new float[bars.Length];
        updates= new float[bars.Length];
        GetValues(ref oris,false);

        okBtn = transform.Find("okBtn").GetComponent<Button>();
        resetBtn= transform.Find("resetBtn").GetComponent<Button>();
        okBtn.onClick.AddListener(()=> {
            GetValues(ref updates,true);
            WaterWaveSimple.Instance.SetAttrs(updates[0], updates[1],updates[2], updates[3], updates[4]
            );
            gameObject.SetActive(false);
        });
        resetBtn.onClick.AddListener(() => {
            for (int i = 0; i < bars.Length; i++)
            {
                bars[i].value=oris[i];
            }
        });
    }

    void UpdateLabel(Slider bar,float value)
    {
        Text label = bar.transform.Find("Label").GetComponent<Text>();
        if(bar.wholeNumbers) label.text = bar.value.ToString();
        else label.text = bar.value.ToString("f2");
    }

    void GetValues(ref float[] values,bool isUpdate)
    {
        for (int i = 0; i < bars.Length; i++)
        {
            values[i] = bars[i].value;
        }
        if (isUpdate)
        {
            values[0] *= 100;
            values[1] *= 10;
            values[2] *= 5;
            values[3] /= 10f;
        }
    }
}
