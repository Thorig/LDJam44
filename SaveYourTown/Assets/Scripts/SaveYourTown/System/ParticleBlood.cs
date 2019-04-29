using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBlood : MonoBehaviour
{
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
        main.loop = false;
    }

    void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
    }
}
