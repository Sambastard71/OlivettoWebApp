using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class RectValue
{
    public RectTransform position;
    public RectTransform scale;
}

public class ChoosedReply : MonoBehaviour
{

    public RectTransform Right;
    public RectTransform Wrong;

    public RectValue[] Replys1;

    public void SetRight(int replyIndex, int questionIndex)
    {
        RectValue value = Replys1[replyIndex];
        Right.anchoredPosition = new Vector2(value.position.anchoredPosition.x + 15, value.position.anchoredPosition.y);
        Right.sizeDelta = new Vector2(value.scale.sizeDelta.x + 30, value.scale.sizeDelta.y);
    }

    public void SetWrong(int replyIndex, int questionIndex)
    {
        RectValue value = Replys1[replyIndex];
        Wrong.anchoredPosition = new Vector2(value.position.anchoredPosition.x + 15, value.position.anchoredPosition.y);
        Wrong.sizeDelta = new Vector2(value.scale.sizeDelta.x + 30, value.scale.sizeDelta.y);
    }

    public void ResetRectTransform()
    {
        Right.anchoredPosition = new Vector2(2000, 2000);
        Wrong.anchoredPosition = new Vector2(2000, 2000);
    }

}
