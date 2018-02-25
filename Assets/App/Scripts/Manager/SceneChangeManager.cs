using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;
using System;

public class SceneChangeManager : MonoBehaviour
{
    private Coroutine coroutine;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip sound;

    [SerializeField]
    float changeTime = 3f;

    [SerializeField]
    private Image fadePanel;

    private bool inChanging = false;

    /// <summary>
    /// シーンを遷移します
    /// </summary>
    /// <param name="name">遷移先のシーン名</param>
    public void ChangeScene(string name)
    {
        Change(name, changeTime, sound);
    }

    /// <summary>
    /// シーンを遷移します
    /// </summary>
    /// <param name="name">遷移先のシーン名</param>
    /// <param name="sound">遷移する際に鳴らす音声</param>
    public void ChangeScene(string name, AudioClip _sound)
    {
        Change(name, changeTime, _sound);
    }

    /// <summary>
    /// シーンを遷移します
    /// </summary>
    /// <param name="name">遷移先のシーン名</param>
    /// <param name="time">遷移するまでの時間</param>
    public void ChangeScene(string name, float time)
    {
        Change(name, time, sound);
    }

    private void Change(string name, float time, AudioClip sound)
    {
        if (!inChanging)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            inChanging = true;
            audioSource.PlayOneShot(sound);
            coroutine = StartCoroutine(CoChange(name, time));
            PanelFader.Fade(fadePanel, time, false);
            Debug.Log($"シーン遷移開始します：{name}");
        }
        else Debug.Log("シーン遷移処理中です");
    }

    private IEnumerator CoChange(string name, float time)
    {
        for (float endTime = Time.timeSinceLevelLoad + time; endTime > Time.timeSinceLevelLoad;)
        {
            yield return null;
        }

        inChanging = false;
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// 現在のシーンをリロードします
    /// </summary>
    public void ReloadCurrentScene()
    {
        Change(SceneManager.GetActiveScene().name, 1, sound);
    }
}
