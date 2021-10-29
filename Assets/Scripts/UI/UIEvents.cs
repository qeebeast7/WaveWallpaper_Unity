using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 挂载在总Setting上
/// </summary>
public class UIEvents : MonoBehaviour,IPointerExitHandler {

    private RectTransform rect;
    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition))
            {                
                foreach (Transform tran in transform)
                {
                    if (tran.gameObject.name != "List")
                    {
                        tran.gameObject.SetActive(false);
                    }
                    else
                    {
                        tran.gameObject.SetActive(true);
                    }
                }

                gameObject.SetActive(false);
                WallpaperManager.Instance.isSetting = false;
            }
        }
	}

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
