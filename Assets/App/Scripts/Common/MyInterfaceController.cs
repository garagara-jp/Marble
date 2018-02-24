using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インターフェース周りの自作クラス
/// </summary>
static class MyInterfaceController
{
    /// <summary>
    /// コンポーネントのうち、指定したインターフェイスを実装しているもの全てを返す
    /// </summary>
    /// <typeparam name="I">>取得するインターフェイス</typeparam>
    /// <returns>指定したインターフェイスを持つオブジェクトの列挙</returns>
    public static IEnumerable<I> FindObjectsOfInterface<I>() where I : class
    {
        MonoBehaviour[] monoBehaviours = Object.FindObjectsOfType<MonoBehaviour>();
        List<I> list = new List<I>();

        foreach (var behaviour in monoBehaviours)
        {
            I[] components = behaviour.GetComponents<I>();

            if (components != null)
            {
                foreach (var component in components)
                {
                    if (!list.Contains(component))
                        list.Add(component);
                }
            }
        }

        return list;
    }

    /// <summary>
    /// コンポーネントのうち、指定したインターフェイスを実装しているものを一つ返す
    /// </summary>
    /// <typeparam name="I">>取得するインターフェイス</typeparam>
    /// <returns>指定したインターフェイスを持つオブジェクト</returns>
    public static I FindObjectOfInterface<I>() where I : class
    {
        MonoBehaviour[] monoBehaviours = Object.FindObjectsOfType<MonoBehaviour>();
        I obj = null;

        foreach (var behaviour in monoBehaviours)
        {
            I component = behaviour.GetComponent<I>();

            if (component != null)
            {
                obj = component;
            }
        }

        return obj;
    }
}
