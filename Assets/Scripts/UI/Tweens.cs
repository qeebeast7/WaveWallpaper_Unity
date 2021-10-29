using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tweens : MonoBehaviour {

    public static void Fade(GameObject obj,bool isAppear,float time=0.2f)
    {
        //Fade
        int value = isAppear ? 1 : 0;

        foreach (var item in obj.GetComponentsInChildren<Image>())
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, 1-value);
        }
        foreach (var item in obj.GetComponentsInChildren<Text>())
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, 1 - value);
        }
        foreach (var item in obj.GetComponentsInChildren<Image>())
        {
            item.DOFade(value, time);
        }
        foreach (var item in obj.GetComponentsInChildren<Text>())
        {
            item.DOFade(value, time);
        }
    }
}
