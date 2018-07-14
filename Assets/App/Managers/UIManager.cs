using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{

    public Text EventText;
    public Image Healthbar;

    internal void SetEventText(string v)
    {
        EventText.CrossFadeAlpha(1, .3f, true);
        EventText.text = v;

        //EventText.CrossFadeAlpha(0, .3f, true);
    }

    public void AdjustHealthBar(float n)
    {
        Healthbar.fillAmount = n;
    }
}
