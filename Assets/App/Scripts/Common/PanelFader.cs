using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

public static class PanelFader
{
    public static void Fade(Image panel, float fadeTime, bool fadeIn)
    {
        panel.gameObject.SetActive(true);
        var color = panel.color;
        var count = 0f;

        Observable.IntervalFrame(1)
            .TakeWhile(_ => count < fadeTime)
            .Subscribe(_ =>
            {
                count += Time.deltaTime;
                if (fadeIn) color.a -= Time.deltaTime / fadeTime;
                else color.a += Time.deltaTime / fadeTime;

                color.a = Mathf.Clamp01(color.a);

                if (panel != null) panel.color = color;
            },
            () =>
            {
                if (panel != null) panel.gameObject.SetActive(false);
            });
    }
}