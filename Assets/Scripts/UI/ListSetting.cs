using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ListSetting : MonoBehaviour {
    private static ListSetting instance;
    private ListSetting() { }

    public static ListSetting Instance
    {
        get
        {
            if (instance == null) instance = new ListSetting();
            return instance;
        }
    }


    private Button waveBtn;
    private Button videoBtn;
    private Button audioBtn;

    private GameObject waveSet;
    private GameObject audioSet;

    private VideoPlayer player;
    private string videoPath;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindObjectOfType<VideoPlayer>();
    }
    // Use this for initialization
    void Start() {        
        waveBtn = transform.Find("waveBtn").GetComponent<Button>();
        videoBtn = transform.Find("videoBtn").GetComponent<Button>();
        audioBtn = transform.Find("audioBtn").GetComponent<Button>();
        waveSet = transform.parent.Find("waveSet").gameObject;
        waveSet.SetActive(false);
        audioSet = transform.parent.Find("audioSet").gameObject;
        audioSet.SetActive(false);

        waveBtn.onClick.AddListener(() => {
            //Appear
            if (!waveSet.activeInHierarchy)
            {
                waveSet.SetActive(!waveSet.activeInHierarchy);
                Tweens.Fade(waveSet, waveSet.activeInHierarchy);
            }
            //Disappear
            else
            {
                Tweens.Fade(waveSet, !waveSet.activeInHierarchy);
                waveSet.SetActive(!waveSet.activeInHierarchy);
            }
        });
        audioBtn.onClick.AddListener(() => {
            //Appear
            if (!audioSet.activeInHierarchy)
            {
                audioSet.SetActive(!audioSet.activeInHierarchy);
                Tweens.Fade(audioSet, audioSet.activeInHierarchy);
            }
            //Disappear
            else
            {
                Tweens.Fade(audioSet, !audioSet.activeInHierarchy);
                audioSet.SetActive(!audioSet.activeInHierarchy);
            }

        });
        videoBtn.onClick.AddListener(SelectVideo);
    }

    void SelectVideo()
    {
        videoBtn.enabled = false;
        FolderBrowserHelper.SelectFile(_ => PlayVideo(_), FolderBrowserHelper.VIDEOFILTER);
        videoBtn.enabled = true;
    }
    void PlayVideo(string filePath)
    {
        filePath = filePath.Replace("\\", "/");
        videoPath = filePath;
        PlayPath(videoPath);
    }

    void PlayPath(string filePath)
    {
        string url = filePath;
        if (!string.IsNullOrEmpty(url))
        {
            player.source = VideoSource.Url;
            player.url = url;
            player.Play();
        }
        //StartCoroutine(DownLoadMovie(url));
    }

    public void PlayPath(VideoPlayer player,string filePath)
    {
        string url = filePath;
        if (!string.IsNullOrEmpty(url))
        {
            player.source = VideoSource.Url;
            player.url = url;
            player.Play();
        }
        //StartCoroutine(DownLoadMovie(url));
    }
    private IEnumerator DownLoadMovie(string url)
    {
        WWW www = new WWW(url);
        Debug.Log(Time.time);

        yield return www;
    }

    public string GetVideoPath()
    {
        if (videoPath != null) return videoPath;
        else return player.url;
    }
}
