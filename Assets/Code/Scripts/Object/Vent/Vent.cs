using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField] private AudioManager audio;
    [SerializeField] private GameObject otherVent;
    [SerializeField] private float time;
    private bool vented;
    private float progress;
    private Collision2D player;

    private void Update()
    {
        if (!vented) return;
        progress += Time.deltaTime;
        if (progress < time)return; 
        
        float posOffset = 0;
        if (otherVent.transform.position.x > gameObject.transform.position.x)
        {
            posOffset += 1;
        }
        else
        {
            posOffset -= 1;
        }
        player.transform.position = otherVent.transform.position + new Vector3(posOffset, 1);
        audio.Play("VentOut");
        vented = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision;
            collision.gameObject.GetComponent<Animator>().SetBool("IsJumping", true);
            collision.gameObject.GetComponent<Animator>().SetTrigger("OnWall");
            vented = true;
            audio.Play("VentIn");
        }
    }
}
