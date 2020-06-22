using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

[Serializable]
public class Question
{
    public string Quest;

    public int RightReplyIndex;
    public string[] Replys;

}

public class QuestionSetter : MonoBehaviour
{
    public Camera Camera;

    public Question[] Questions;
    public int ActualQuestion;

    public GameObject QuestionGui;

    public TextMeshProUGUI Question; 
    public TextMeshProUGUI Reply1;
    public TextMeshProUGUI Reply2;
    public TextMeshProUGUI Reply3;

    public Button[] Btns;
    
    public ChoosedReply choosedReply;

    AsyncOperation operation;

    public string[] FilesNameReply;
    public string[] FilesNameQuestions;
    public VideoPlayer player;

    private void Awake()
    {
        player.url = Application.streamingAssetsPath + "/" + FilesNameQuestions[ActualQuestion];
        player.Prepare();
    }

    public void Start()
    {
        SetQuestion();
        operation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        operation.allowSceneActivation = false;
    }

    public void SetQuestion()
    {
        Camera.depth = 0;

        if (ActualQuestion >= Questions.Length)
        {
            Debug.Log("Quitting");
            Application.Quit();
            return;
        }

        choosedReply.ResetRectTransform();

        EventSystem.current.SetSelectedGameObject(null);

        Question.text = Questions[ActualQuestion].Quest;
        Reply1.text = Questions[ActualQuestion].Replys[0];
        Reply2.text = Questions[ActualQuestion].Replys[1];
        Reply3.text = Questions[ActualQuestion].Replys[2];

        player.Play();

        for (int i = 0; i < 3; i++)
        {
            Btns[i].interactable = true;

            ColorBlock cb = Btns[i].colors;
            cb.normalColor = new Color(1, 1, 1, 0);
            cb.highlightedColor = new Color(1, 1, 1, 0);
            cb.pressedColor = new Color(1, 1, 1, 0);
            cb.selectedColor = new Color(1, 1, 1, 0);
            cb.disabledColor = new Color(1, 1, 1, 0);

            if (i == Questions[ActualQuestion].RightReplyIndex - 1)
            {
                Btns[i].colors = cb;
                Btns[i].onClick.AddListener(RightQuestion);
                Btns[i].gameObject.tag = "RightReply";
                continue;
            }

            Btns[i].colors = cb;
            
            Btns[i].onClick.AddListener(WrongQuestion);
            Btns[i].gameObject.tag = "WrongReply";
        }
    }

    public void PlayLogos()
    {
        operation.allowSceneActivation = true;
    }

    public void RightQuestion()
    {
        Camera.depth = 1;

        for (int i = 0; i < 3; i++)
        {
            Btns[i].onClick.RemoveAllListeners();
            Btns[i].interactable = false;
        }

        player.url = Application.streamingAssetsPath + "/" + FilesNameReply[ActualQuestion];

        player.loopPointReached += FinishReplyVideo;

        player.Play();


        //float timeToInvoke = 25;
        //ActualQuestion++;
        //if (ActualQuestion >= Questions.Length)
        //{
        //    Invoke("PlayLogos", timeToInvoke + 1);
        //    return;
        //}
        //Invoke("SetQuestion", timeToInvoke + 1);
    }

    public void WrongQuestion()
    {
        Camera.depth = 1;

        for (int i = 0; i < 3; i++)
        {
            Btns[i].onClick.RemoveAllListeners();
            Btns[i].interactable = false;
            if (Btns[i].gameObject.tag == "RightReply")
            {
                choosedReply.SetRight(i, ActualQuestion);
            }

        }

        player.url = Application.streamingAssetsPath + "/" + FilesNameReply[ActualQuestion];

        player.loopPointReached += FinishReplyVideo;

        player.Play();

        //float timeToInvoke = 25;
        //ActualQuestion++;
        //if (ActualQuestion >= Questions.Length)
        //{
        //    Invoke("PlayLogos", timeToInvoke + 1);
        //    return;
        //}
        //Invoke("SetQuestion", timeToInvoke + 1);
    }

    public void FinishReplyVideo(VideoPlayer player)
    {
        player.loopPointReached -= FinishReplyVideo;

        //float timeToInvoke = 25;
        ActualQuestion++;
        if (ActualQuestion >= Questions.Length)
        {
            PlayLogos();
            //Invoke("PlayLogos", timeToInvoke + 1);
            return;
        }
        player.url = Application.streamingAssetsPath + "/" + FilesNameQuestions[ActualQuestion];
        SetQuestion();
        //Invoke("SetQuestion", timeToInvoke + 1);

    }

    public void SetColor(Button btn)
    {
        if (btn.gameObject.tag == "RightReply")
        {
            string s = btn.gameObject.name[btn.gameObject.name.Length - 1].ToString();

            choosedReply.SetRight(int.Parse(s)-1, ActualQuestion);
        }
        else if (btn.gameObject.tag == "WrongReply")
        {
            string s = btn.gameObject.name[btn.gameObject.name.Length - 1].ToString();

            choosedReply.SetWrong( int.Parse(s) - 1, ActualQuestion);
        }
    }
}
