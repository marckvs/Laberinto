﻿using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour
{

    private float lifeTime = 5;

    public Rigidbody rigidbody;
    public Material mat;
    private Color originalCol;
    private float fadePercent;
    private float deathTime;
    private bool fading;

    void Start()
    {
        deathTime = Time.time + lifeTime;
        originalCol = mat.color;
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);

            if (fading)
            {
                fadePercent += Time.deltaTime;
                mat.color = Color.Lerp(originalCol, Color.clear, fadePercent);

                if (fadePercent >= 1)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (Time.time > deathTime)
                {
                    fading = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Ground")
        {
            rigidbody.Sleep();
        }
    }
}