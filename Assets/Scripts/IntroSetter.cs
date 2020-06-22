using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class IntroSetter : MonoBehaviour
{
    public string FileToLoad;
    public VideoPlayer player;

    AsyncOperation operation;
    private void Awake()
    {
        operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        player.url = Application.streamingAssetsPath + "/" + FileToLoad;
        player.Prepare();
    }

    public void OnClickStartBtn()
    {
        player.prepareCompleted += OnPrepareEnded;
        player.loopPointReached += OnEndVideoIntro;
    }

    public void OnPrepareEnded(VideoPlayer player)
    {
        player.prepareCompleted -= OnPrepareEnded;
        player.Play();
    }

    void OnEndVideoIntro(VideoPlayer player)
    {
        player.loopPointReached -= OnEndVideoIntro;
        operation.allowSceneActivation = true;
    }
}
