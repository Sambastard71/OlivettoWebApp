using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LogoSetter : MonoBehaviour
{
    public string FileName;
    public VideoPlayer player;

    void Start()
    {
        player.url = Application.streamingAssetsPath + "/" + FileName;
    }
}
