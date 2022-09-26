using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimations : MonoBehaviour
{

    private Animator KnightAnim;
    // Start is called before the first frame update
    void Start()
    {
        KnightAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWalk()
    {
        KnightAnim.SetBool("attack", false);
        KnightAnim.SetBool("walk", true);
    }

    public void StopWalk()
    {
        
    }

    public void PlayAttack()
    {
        KnightAnim.Play("KnightAttack_Anim");
    }
}
