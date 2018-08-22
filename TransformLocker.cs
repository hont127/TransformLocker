using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformLocker : MonoBehaviour
{
    bool mIsInitialization;


    void OnValidate()
    {
#if UNITY_EDITOR
        if (!mIsInitialization)
        {
            UnityEditor.EditorApplication.update -= SceneUpdateCallback;
            UnityEditor.EditorApplication.update += SceneUpdateCallback;
            mIsInitialization = true;
        }
#endif
    }

    void Reset()
    {
#if UNITY_EDITOR
        if (!mIsInitialization)
        {
            UnityEditor.EditorApplication.update -= SceneUpdateCallback;
            UnityEditor.EditorApplication.update += SceneUpdateCallback;
            mIsInitialization = true;
        }
#endif
    }

#if UNITY_EDITOR
    void SceneUpdateCallback()
    {
        try
        {
            transform.GetHashCode();
        }
        catch
        {
            UnityEditor.EditorApplication.update -= SceneUpdateCallback;
            return;
        }

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
#endif
}
