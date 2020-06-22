using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreQuizScript : MonoBehaviour
{
    public AudioSource src;
    public AudioClip preIntro;

    public GameObject QuestioUI;

    void Start()
    {
        src.clip = preIntro;
        src.Play();
        Invoke("SetUIActive", src.clip.length);
    }

    void SetUIActive()
    {
        src.Stop();
        QuestioUI.SetActive(true);
        gameObject.SetActive(false);
    }

}
