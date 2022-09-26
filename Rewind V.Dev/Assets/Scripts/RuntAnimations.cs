using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntAnimations : MonoBehaviour
{
    private Animator runtAnim;

    private void Start()
    {
        runtAnim = this.GetComponent<Animator>();
    }

    public void PlayRuntAttackAnimation()
    {
        runtAnim.Play("Runt_Attack_Anim");
    }
}
