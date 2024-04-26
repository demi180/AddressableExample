using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using TMPro;

public class LoadScene : MonoBehaviour
{
    public TextMeshProUGUI loadText;
    public TextMeshProUGUI loadedText;
    public Camera loaderCam;

    bool sceneLoaded;
    bool sceneLoading;
    AsyncOperationHandle<SceneInstance> sceneHandle;

    // Start is called before the first frame update
    void Start()
    {
        if (loadedText != null)
            loadedText.gameObject.SetActive(false);
        if (loadText != null)
            loadText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sceneLoaded)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                sceneLoaded = true;
				sceneHandle = Addressables.LoadSceneAsync("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Additive, false);
                if (sceneHandle.IsValid())
                    sceneLoading = true;
                if (loadText != null)
                    loadText.gameObject.SetActive(false);
                if (loadedText != null)
                    loadedText.gameObject.SetActive(true);
            }
        }
        if (sceneLoading)
        {
            if (sceneHandle.IsValid() && sceneHandle.Status == AsyncOperationStatus.Succeeded)
            {
                if (loadedText != null)
                {
                    loadedText.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    sceneLoading = false;
                    if (loadedText != null)
                        loadedText.gameObject.SetActive(false);
                    if (loaderCam != null)
                        loaderCam.gameObject.SetActive(false);
                    sceneHandle.Result.ActivateAsync();
                }
            }
        }
    }
}