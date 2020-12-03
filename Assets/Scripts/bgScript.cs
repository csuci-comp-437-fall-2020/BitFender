using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgScript : MonoBehaviour
{
    public static bgScript BgInstance;

    private void Awake()
    {
        if(BgInstance != null && BgInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        BgInstance = this;
        DontDestroyOnLoad(this);
    }
}
