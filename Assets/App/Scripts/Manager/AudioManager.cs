using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Param { Master, UI, BGM, SE }
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;
    [SerializeField, Range(-80, 0)]
    private float masterVolume = 0, uiVolume = -10, bgmVolume = -10, seVolume = -10;

    public float MasterVolume { set { masterVolume = value; } }
    public float UIVolume { set { uiVolume = value; } }
    public float BGMVolume { set { bgmVolume = value; } }
    public float SEVolume { set { seVolume = value; } }

    private List<AudioSource> BGMTable;
    private AudioSource currentBGM;
    private string[] paramNames = new string[Enum.GetNames(typeof(Param)).Length];

    private void Awake()
    {
        // シングルトンを確立
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // パラメーターの名前を配列に格納
        for (int i = 0; i < paramNames.Length; i++)
        {
            Param param = (Param)i;
            paramNames[i] = param.ToString() + "Volume";
        }
    }

    private void Update()
    {
        mixer.SetFloat(paramNames[(int)Param.Master], masterVolume);
        mixer.SetFloat(paramNames[(int)Param.UI], uiVolume);
        mixer.SetFloat(paramNames[(int)Param.BGM], bgmVolume);
        mixer.SetFloat(paramNames[(int)Param.SE], seVolume);
    }

    public void SetBGM(AudioSource bgm)
    {
        var isListed = false;
        foreach (var bgmInTable in BGMTable)
        {
            isListed = (bgm == bgmInTable) ? true : false;
        }
        BGMTable.Add(bgm);
    }

    public void PlayBGM(AudioSource bgm)
    {
        currentBGM = bgm;
        currentBGM.Play();
    }
}
