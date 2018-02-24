using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// GameManagerにアタッチ
/// </summary>
public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    private Coroutine coroutine;
    private IEnumerator IEnum;
    private float elapsedTime;
    private float remainingTime;

    private float timeScale = 1;    // TimeManagerが管理するTimeScale
    private float tmpTimeScale;
    private IDisposable scaleStream;

    public float ElapsedTime { get { return Mathf.Floor(elapsedTime); } }
    public float LeftTime { get { return Mathf.Floor(remainingTime); } }

    private void Awake()
    {
        // シングルトンを確立
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        elapsedTime = 0;
        remainingTime = 0;
    }

    private void LateUpdate()
    {
        // TimeScaleを管理
        Time.timeScale = timeScale;
    }

    /// <summary>
    /// カウントを始めます。
    /// </summary>
    public void StartCount()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        IEnum = CoCount();
        coroutine = StartCoroutine(IEnum);
    }

    private IEnumerator CoCount()
    {
        var startTime = Time.timeSinceLevelLoad;
        
        while (true)
        {
            elapsedTime = Time.timeSinceLevelLoad - startTime;
            yield return null;
        }
    }

    /// <summary>
    /// 任意の時間でカウントダウンします。
    /// </summary>
    /// <param name="time">カウントする時間（秒）</param>
    public void StartCountDown(float time)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        IEnum = CoCountDown(time);
        coroutine = StartCoroutine(IEnum);
    }

    private IEnumerator CoCountDown(float time)
    {
        var startTime = Time.timeSinceLevelLoad;
        var limitTime = time;
        remainingTime = time;

        while (remainingTime > 0)
        {
            elapsedTime = Time.timeSinceLevelLoad - startTime;
            remainingTime = limitTime - elapsedTime;
            yield return null;
        }

        FinishCountDown();
    }

    /// <summary>
    /// カウントダウンを終了します
    /// </summary>
    public void FinishCountDown()
    {
        elapsedTime = 0;
        remainingTime = 0;
        StopCoroutine(coroutine);
        coroutine = null;
    }

    /// <summary>
    /// カウントを一時停止します
    /// </summary>
    public void PauseCount()
    {
        if (IEnum != null) StopCoroutine(IEnum);
        else Debug.Log("カウントが始まっていません");
    }

    /// <summary>
    /// カウントを再開します
    /// </summary>
    public void RestartCount()
    {
        if (IEnum != null) StartCoroutine(IEnum);
        else Debug.Log("カウントが始まっていません");
    }

    /// <summary>
    /// カウントをリセットします
    /// </summary>
    public void ResetCount()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        elapsedTime = 0;
        remainingTime = 0;
    }

    /// <summary>
    /// TimeScaleを変更します。
    /// </summary>
    /// <param name="scale">変更後のTimeScale</param>
    public void SetTimeScale(float scale)
    {
        timeScale = scale;
    }

    /// <summary>
    /// TimeScaleを任意の時間中だけ変更します
    /// </summary>
    /// <param name="scale">変更後のTimeScale</param>
    /// <param name="validTime">TimeScaleの変更が有効な時間（秒）</param>
    public void SetTimeScale(float scale, float validTime)
    {
        // ストリームをリセット
        if (scaleStream != null)
            scaleStream.Dispose();

        // 値をセット
        tmpTimeScale = timeScale;
        timeScale = scale;

        // 時間経過でTimeScaleを元に戻す
        scaleStream = Observable.Timer(TimeSpan.FromSeconds(validTime)).Subscribe(_ => timeScale = tmpTimeScale);
    }

    /// <summary>
    /// TimeScaleを元に戻します
    /// </summary>
    public void ResetTimeScale()
    {
        if (scaleStream != null)
            scaleStream.Dispose();

        timeScale = tmpTimeScale;
    }
}