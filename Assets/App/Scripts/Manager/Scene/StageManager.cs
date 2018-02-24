using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;

public enum Rank { S, A, B, C, D }
public class StageManager : MonoBehaviour
{
    private SceneModel model;
    private SceneChangeManager sceneChange;

    [SerializeField]
    private Image fadePanel;


    [SerializeField]
    private GoalManager[] goalManagers;

    [SerializeField]
    private GameObject clearUI;
    [SerializeField]
    private Text desireCountText;
    [SerializeField]
    private Text clickCountText;
    [SerializeField]
    private Text rankText;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;

    private void Awake()
    {
        model = GetComponent<SceneModel>();
        sceneChange = GetComponent<SceneChangeManager>();

        GameManager.Instance.SetCurrentState(GameState.Prepare);
    }

    private void Start()
    {
        StartScene();

        var stream = this.UpdateAsObservable().Publish().RefCount();

        stream.Where(_ => GameManager.Instance.CurrentGameState == GameState.Stage)
            .Subscribe(_ => CheckGoal());
        stream.Where(_ => GameManager.Instance.CurrentGameState == GameState.Result)
            .First()
            .Subscribe(_ => ShowResult());
    }

    private void StartScene()
    {
        clearUI.SetActive(false);
        PanelFader.Fade(fadePanel, 1f, true);
        GameManager.Instance.SetCurrentState(GameState.Stage);
    }

    private void CheckGoal()
    {
        Debug.Log("ゴールをチェックしています");

        if (goalManagers.All(x => x.isGoal))
        {
            Debug.Log("全てのゴール地点が達成されました");
            GameManager.Instance.SetCurrentState(GameState.Result);
            return;
        }
    }

    private void ShowResult()
    {
        Subject<int> process = new Subject<int>();

        process.Where(x => x == 0)
            .Subscribe(_ =>
            {
                clearUI.SetActive(true);

                var param = model.GetStageParam();

                desireCountText.text = param.DesireCount.ToString();
                clickCountText.text = param.ClickCount.ToString();
                rankText.text = GetRank(param.ClickCount, param.DesireCount).ToString();
            });

        source.PlayOneShot(clip);
        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ => process.OnNext(0));
    }

    private Rank GetRank(int clickCount, int desireCount)
    {
        Rank rank;
        float score = (float)clickCount / desireCount;

        if (score < 0.5f) rank = Rank.S;
        else if (score <= 1) rank = Rank.A;
        else if (score <= 2) rank = Rank.B;
        else if (score <= 3) rank = Rank.C;
        else rank = Rank.D;

        return rank;
    }
}
