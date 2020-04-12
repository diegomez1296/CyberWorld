using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsPanelController : MonoBehaviour
{
    private Text footerText;

    [SerializeField]
    private string[] footerTextInfos =
    {
        "",
        "VR headset",
        "Computer set",

    };

    void Start()
    {
        footerText = GetComponentsInChildren<Text>()[1];
        SetFooterText(0);
    }

    public void SetFooterText(int textParam)
    {
        footerText.text = footerTextInfos[textParam];
    }
}
