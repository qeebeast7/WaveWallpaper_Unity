using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaveSimple : MonoBehaviour
{
    private static WaterWaveSimple instance;
    private WaterWaveSimple() { }
    public static WaterWaveSimple Instance
    {
        get
        {
            if (instance == null) instance = new WaterWaveSimple();
            return instance;
        }
    }

    //Inspector面板上直接拖入
    public Shader shader = null;
    private Material _material = null;

    //距离系数
    public float distanceFactor = 60.0f;
    //时间系数
    public float timeFactor = -30.0f;
    //sin函数结果系数
    public float totalFactor = 1.0f;

    //波纹宽度
    public float waveWidth = 0.3f;
    //波纹扩散的速度
    public float waveSpeed = 0.3f;

    private float waveStartTime;
    private Vector4 startPos = new Vector4(0.5f, 0.5f, 0, 0);

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (WallpaperManager.Instance.userData != null)
        {
            UserData ud = WallpaperManager.Instance.userData;
            distanceFactor = ud.Density;
            timeFactor = ud.Amp;
            totalFactor = ud.Strength;
            waveWidth = ud.WaveWidth;
            waveSpeed = ud.WaveSpeed;
        }
    }
    //根据Shader生成材质
    public Material _Material
    {
        get
        {
            if (_material == null)
                _material = GenerateMaterial(shader);
            return _material;
        }
    }

    //根据shader创建用于屏幕特效的材质
    protected Material GenerateMaterial(Shader shader)
    {
        if (shader == null)
            return null;
        //需要判断shader是否支持
        if (shader.isSupported == false)
            return null;
        Material material = new Material(shader);
        material.hideFlags = HideFlags.DontSave;
        if (material)
            return material;
        return null;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //计算波纹移动的距离，根据enable到目前的时间*速度求解
        float curWaveDistance = (Time.time - waveStartTime) * waveSpeed;
        //设置一系列参数
        _Material.SetFloat("_distanceFactor", distanceFactor);
        _Material.SetFloat("_timeFactor", timeFactor);
        _Material.SetFloat("_totalFactor", totalFactor);
        _Material.SetFloat("_waveWidth", waveWidth);
        _Material.SetFloat("_curWaveDis", curWaveDistance);
        _Material.SetVector("_startPos", startPos);
        Graphics.Blit(source, destination, _Material);
    }

    void Update()
    {
        if (!WallpaperManager.Instance.isSetting)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Input.mousePosition;
                //将mousePos转化为（0，1）区间
                startPos = new Vector4(mousePos.x / Screen.width, mousePos.y / Screen.height, 0, 0);
                waveStartTime = Time.time;
            }
        }
    }
    /// <summary>
    /// Set distancewFactor(传播距离),timeFactor(时间系数),totalFactor（波纹幅度）,waveWidth（波纹半径）,
    /// waveSpeed（波纹速度）
    /// </summary>
    /// <param name="_distancewFactor"></param>
    /// <param name="_timeFactor"></param>
    /// <param name="_totalFacor"></param>
    /// <param name="_waveWidth"></param>
    /// <param name="_waveSpeed"></param>
    public void SetAttrs(float _distancewFactor,float _timeFactor,float _totalFacor,float _waveWidth,
        float _waveSpeed)
    {
        distanceFactor = _distancewFactor;
        timeFactor = _timeFactor;
        totalFactor = _totalFacor;
        waveWidth = _waveWidth;
        waveSpeed = _waveSpeed;
    }

    public float[] GetAttrs()
    {
        float[] attrs=new float[5];
        attrs[0] = distanceFactor;
        attrs[1] = timeFactor;
        attrs[2] = totalFactor;
        attrs[3] = waveWidth;
        attrs[4] = waveSpeed;
        return attrs;
    }
}