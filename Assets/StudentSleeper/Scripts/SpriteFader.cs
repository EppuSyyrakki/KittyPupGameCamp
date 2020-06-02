using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    public bool fadingIn { get; set; }
    public bool fadingOut { get; set; }
    public float fadeSpeed = 5f;
    private float _t = 0;
    public SpriteRenderer sr;

    // Update is called once per frame
    void Update()
    {
        if (fadingIn)
        {
            if (_t <= 1) _t += Time.deltaTime * fadeSpeed;
            fadingIn = Fade(0, 1);
        }

        if (fadingOut)
        {
            if (_t <= 1) _t += Time.deltaTime * fadeSpeed;
            fadingOut = Fade(1, 0);

            if (!fadingOut)
            {
                Destroy(gameObject);    // add GameObject.Destroy if doesn't destroy
            }
        }
    }

    private bool Fade(int start, int stop)
    {
        Color whiteAlpha = new Color(1, 1, 1, Mathf.Lerp(start, stop, _t));
        sr.color = whiteAlpha;

        if (_t >= 1) {_t = 0; return false;}
        else return true;
    }
}
