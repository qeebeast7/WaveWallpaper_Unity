using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text txt;
    // Use this for initialization
    void Start () {
        txt = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        txt.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        txt.color = Color.black;
    }
}
