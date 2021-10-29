using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class WallpaperManager : MonoBehaviour {
    private static WallpaperManager instance;
    private WallpaperManager() { }

    public static WallpaperManager Instance
    {
        get
        {
            if (instance == null) instance = new WallpaperManager();
            return instance;
        }
    }

    private GameObject setting;
    private RectTransform canvasRect;
    private VideoPlayer player;
    private AudioSource audio;

    public float longPressTime = 2f;
    private float pressTimer = 0;
    private GameObject settings;
    [HideInInspector]
    public bool isSetting = false;

    [HideInInspector]
    public UserData userData;

    private void Awake()
    {
        instance = this;
        userData = JsonHelper.Read<UserData>("userData");
    }

    // Use this for initialization
    void Start () {
        settings = GameObject.Find("Settings");
        settings.SetActive(false);
        canvasRect = settings.transform.parent.GetComponent<RectTransform>();
        player = GameObject.FindObjectOfType<VideoPlayer>();
        audio = FindObjectOfType<AudioSource>();

        if (userData != null)
        {
            ListSetting.Instance.PlayPath(player, userData.VideoPath);
            audio.volume = userData.Volume;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && !isSetting)
        {
            pressTimer += Time.deltaTime;
            if (pressTimer >= longPressTime)
            {
                Vector2 outVec;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out outVec))
                {
                    settings.GetComponent<RectTransform>().anchoredPosition = outVec;
                }
                isSetting = true;
                Tweens.Fade(settings, true);
                settings.SetActive(true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            pressTimer = 0;
        }
	}

    private void OnApplicationQuit()
    {
        float[] attrs = WaterWaveSimple.Instance.GetAttrs();
        string videoPath = ListSetting.Instance.GetVideoPath();
        int mute = AudioSetting.Instance.GetMute();
        float volume = AudioSetting.Instance.GetVolume();
        UserData userData = new UserData(attrs[0],attrs[1], attrs[2], attrs[3], attrs[4],
            videoPath,mute,volume);
        JsonHelper.Write(userData, "userData");
    }
}
