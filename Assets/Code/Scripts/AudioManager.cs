using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    private Dictionary<string, List<AudioSource>> audios;

    private void Start()
    {
        audios = new Dictionary<string, List<AudioSource>>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            audios[childTransform.name] = new List<AudioSource>();
            int j = 0;
            for (; j < childTransform.childCount; j++)
                audios[childTransform.name].Add(childTransform.GetChild(j).GetComponent<AudioSource>());
            if (j == 0) audios[childTransform.name].Add(childTransform.GetComponent<AudioSource>());
        }
    }

    public void Play(String audioName)
    {
        audios[audioName][Random.Range(0, audios[audioName].Count)].Play();
    }
}
