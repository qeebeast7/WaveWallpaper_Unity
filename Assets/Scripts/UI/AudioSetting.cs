using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AudioSetting : MonoBehaviour,IPointerExitHandler {
    private static AudioSetting instance;
    private AudioSetting() { }

    public static AudioSetting Instance
    {
        get
        {
            if (instance == null) instance = new AudioSetting();
            return instance;
        }
    }


    private AudioSource audio;

    private Toggle mute;
    private Slider volume;
    private Text volumeTxt;

    private void Awake()
    {
        instance = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    // Use this for initialization
    void Start () {
        audio = GameObject.FindObjectOfType<AudioSource>();
        mute = GetComponentInChildren<Toggle>();
        volume = GetComponentInChildren<Slider>();
        volumeTxt = volume.transform.Find("Label").GetComponent<Text>();
        volumeTxt.text = ((int)(volume.value * 100)).ToString();

        mute.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn) { audio.volume = 0; print(GetVolume()); }
            else audio.volume = volume.value;
        });

        volume.onValueChanged.AddListener((float value)=>
        {
            if(!mute.isOn) audio.volume = volume.value;
            volumeTxt.text = ((int)(volume.value * 100)).ToString();
        });

        //Set value
        if (WallpaperManager.Instance.userData!=null)
        {
            mute.isOn = (WallpaperManager.Instance.userData.Mute == 1) ? true : false;
            volume.value = WallpaperManager.Instance.userData.Volume;
        }
    }

    public int GetMute()
    {
        return FindObjectOfType<AudioSource>().volume==0?1:0;
    }

    public float GetVolume()
    {
        return FindObjectOfType<AudioSource>().volume;
    }
}
