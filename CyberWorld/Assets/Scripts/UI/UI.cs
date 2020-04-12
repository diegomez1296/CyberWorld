using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public enum Panel
    {
        Control,
        Loading
    }

    public static UI Instance;

    public ControlsPanelController ControlsPanel { get; private set; }
    public LoadingPanel LoadingPanel { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ControlsPanel = GetComponentInChildren<ControlsPanelController>();
        LoadingPanel = GetComponentInChildren<LoadingPanel>();

        HidePannels();
        UI.Instance.PanelVisibility(UI.Panel.Control, true);
    }

    public void HidePannels()
    {
        PanelVisibility(Panel.Control, false);
        PanelVisibility(Panel.Loading, false);
    }

    public void PanelVisibility(Panel panel, bool value)
    {
        switch (panel)
        {
            case Panel.Control:
                ControlsPanel.gameObject.SetActive(value);
                break;
            case Panel.Loading:
                LoadingPanel.gameObject.SetActive(value);
                break;
            default:
                break;
        }
    }
}
