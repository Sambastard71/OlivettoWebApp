using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OnVideoEnd : MonoBehaviour
{
    public string[] VideoFileName;

    public VideoPlayer player;
    public GameObject QuestionGui;
    public QuestionSetter QS;

    public RawImage Immage;
    public RenderTexture IntroRT; 

    public GameObject StartPanel;
    public GameObject RealizzatoText;
    public GameObject OliveLogo;


    public void OnClickStartBtn()
    {
        //Immage.texture = IntroRT;
        //player.targetTexture = IntroRT;

        player.url = Application.streamingAssetsPath + "/" + VideoFileName[0];
        player.Prepare();
        player.prepareCompleted += OnPrepareEnded;
        player.loopPointReached += OnEndVideoIntro;
        //StartPanel.SetActive(false);

    }

    void OnPrepareEnded(VideoPlayer player)
    {
        player.prepareCompleted -= OnPrepareEnded;
        player.Play();
    }

    void OnEndVideoIntro(VideoPlayer player)
    {
        StartPanel.SetActive(false);

        player.loopPointReached -= OnEndVideoIntro;

        player.gameObject.SetActive(false);
         
        QuestionGui.SetActive(true);
        QS.SetQuestion();
    }

    public void PlayLogos()
    {

        RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
       
        Immage.texture = renderTexture;
        player.targetTexture = renderTexture;
        
        StartPanel.SetActive(true);
        player.gameObject.SetActive(true);
        RealizzatoText.SetActive(true);
        QuestionGui.SetActive(false);
        
        player.url = Application.streamingAssetsPath + "/" + VideoFileName[1];
        
        player.Prepare();
        player.loopPointReached += OnEndVideoLogo;
        player.prepareCompleted += OnPrepareEnded;
    }

    void OnEndVideoLogo(VideoPlayer player)
    {
        player.loopPointReached -= OnEndVideoLogo;
        
        player.gameObject.SetActive(false);
        RealizzatoText.SetActive(false);
        OliveLogo.SetActive(true);
    }

}
