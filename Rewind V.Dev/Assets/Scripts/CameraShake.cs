using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Animator cameraAnim;
    private void Start()
    {
        cameraAnim = this.GetComponent<Animator>();
    }

    public void Shake()
    {
        int randomInt = Random.Range(1, 3);

        if(randomInt == 1)
        {
            cameraAnim.Play("shake1");
            randomInt = 0;
        }

        if (randomInt == 2)
        {
            cameraAnim.Play("shake2");
            randomInt = 0;
        }

        if (randomInt == 3)
        {
            cameraAnim.Play("shake3");
            randomInt = 0;
        }
    }
}
