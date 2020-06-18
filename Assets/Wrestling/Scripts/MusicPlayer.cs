using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer musicPlayer;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (musicPlayer == null) musicPlayer = this;
        else Destroy(this.gameObject);
    }
}
