using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRandom : MonoBehaviour
{
    private int randomMusic;
    void Start()
    {
        randomMusic = Random.Range(0, 10);

        if(randomMusic >= 5)
        {
            FindObjectOfType<AudioManager>().Play("Forest");
        }

        if(randomMusic < 5)
        {
            FindObjectOfType<AudioManager>().Play("MainTheme");
        }
    }
}
