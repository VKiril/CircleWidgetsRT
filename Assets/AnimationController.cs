using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        IsPlayingSecondary = false;
        _animator.enabled = false;
    }
 
    public bool IsPlayingMain
    {
        get { return _animator.GetBool("IsPlayingMain"); }
        set { _animator.SetBool("IsPlayingMain", value); }
    }

    public bool IsPlayingSecondary
    {
        get { return _animator.GetBool("IsPlayingSecondary"); }
        set { _animator.SetBool("IsPlayingSecondary", value); }
    }

    public void Start_anim()
    {
        _animator.enabled = true;
    }
}
