using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class GoalManager : MonoBehaviour
{
    private new Renderer renderer;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;

    public bool isGoal { get; set; }

    private void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();

        isGoal = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "TargetMarble")
        {
            FadeMaterial();
            source.PlayOneShot(clip);
            isGoal = true;
        }
    }

    private void FadeMaterial()
    {
        var color = renderer.material.color;
        var count = 0f;

        // Emissionの変更をONに
        renderer.material.EnableKeyword("_EMISSION");

        Observable.IntervalFrame(1)
            .TakeWhile(_ => count < 1f)
            .Subscribe(_ =>
            {
                count += Time.deltaTime * 4;
                color.r = Mathf.Lerp(color.r, Color.red.r, count);
                color.g = Mathf.Lerp(color.g, Color.red.g, count);
                color.b = Mathf.Lerp(color.b, Color.red.b, count);

                renderer.material.SetColor("_EmissionColor", color);
                renderer.material.color = color;
            }, () =>
            {
                var tmpA = color.a;
                color = Color.red;
                color.a = tmpA;

                renderer.material.color = color;
                renderer.material.SetColor("_EmissionColor", color);
            });
    }
}
