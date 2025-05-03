using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    [SerializeField] string url;

    public void Open()
    {
        Application.OpenURL(url);
    }
}
