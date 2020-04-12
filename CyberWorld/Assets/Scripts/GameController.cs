using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum ControllerSet
    {
        VR,
        PC,
        NF //not found
    }

    public static GameController Instance;

    [SerializeField]
    private ControllerSet controllerSet = ControllerSet.NF;

    [SerializeField] private GameObject FPS_Controller;
    [SerializeField] private GameObject VR_Controller;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void StartGame(bool isVR)
    {
        controllerSet = isVR ? ControllerSet.VR : ControllerSet.PC;

        StartCoroutine(LoadWorld());
    }

    private IEnumerator LoadWorld()
    {
        UI.Instance.PanelVisibility(UI.Panel.Loading, true);
        UI.Instance.PanelVisibility(UI.Panel.Control, false);
        UI.Instance.LoadingPanel.SetSliderValue(0);

        //yield return new WaitForSeconds(0.3f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            UI.Instance.LoadingPanel.SetSliderValue(operation.progress);
            yield return new WaitForEndOfFrame();
        }

        yield return CreatePlayer(controllerSet);

        UI.Instance.LoadingPanel.SetSliderValue(1);
        yield return new WaitForSeconds(0.3f);


        UI.Instance.PanelVisibility(UI.Panel.Loading, false);
    }

    private IEnumerator CreatePlayer(ControllerSet controllerSet)
    {
        yield return null;

        switch (controllerSet)
        {
            case ControllerSet.VR:
                if(VR_Controller != null) Instantiate(VR_Controller);
                break;
            case ControllerSet.PC:
                if (FPS_Controller != null) Instantiate(FPS_Controller);
                break;
            case ControllerSet.NF:
                break;
            default:
                break;
        }
    }
}
