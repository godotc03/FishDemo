using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBGMusic : MonoBehaviour
{
    public AudioClip[] BG_Audios;
    private AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = transform.GetComponent<AudioSource>();
        if(audioSrc)
            audioSrc.clip = BG_Audios[Random.Range(0, BG_Audios.Length)];
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
