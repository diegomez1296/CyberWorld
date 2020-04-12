using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectToSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private bool isVR;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnButtonClick());
    }

    private void OnButtonClick()
    {
        GameController.Instance.StartGame(isVR);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<ControlsPanelController>().SetFooterText(isVR ? 1 : 2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInParent<ControlsPanelController>().SetFooterText(0);
    }
}
