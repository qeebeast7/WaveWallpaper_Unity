using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    private static UserData instance;
    public static UserData Instance
    {
        get
        {
            if (instance == null) instance = new UserData();
            return instance;
        }
    }

    public float Density;//波纹密度
    public float Amp;//幅度
    public float Strength;//力度
    public float WaveWidth;//波纹宽度
    public float WaveSpeed;//速度
    public string VideoPath;//视频路径
    public int Mute;//是否静音 1:True
    public float Volume;//音量

    public UserData()
    {

    }

    public UserData(float density, float amp, float strength, float waveWidth, float waveSpeed, string videoPath, int mute, float volume)
    {
        Density = density;
        Amp = amp;
        Strength = strength;
        WaveWidth = waveWidth;
        WaveSpeed = waveSpeed;
        VideoPath = videoPath;
        Mute = mute;
        Volume = volume;
    }
}
