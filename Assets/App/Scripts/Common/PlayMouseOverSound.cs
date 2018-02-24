using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayMouseOverSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip clip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (clip != null)
            source.PlayOneShot(clip);
        else source.Play();
    }
}
