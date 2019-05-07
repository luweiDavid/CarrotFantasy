using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    public float animationTime;
    public string resourcePath;

    private void OnEnable()
    {
        Invoke("DestroyEffect", animationTime);
    }

    private void DestroyEffect()
    {
        GameController.Instance.PushItem(resourcePath, gameObject);
    }
}
