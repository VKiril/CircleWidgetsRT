using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    protected AnimationController _controller;

    public void Start()
    {
        _controller = GetComponent<AnimationController>();
    }

    public void MainButtonClick()
    {
        if (_controller.IsPlayingSecondary == true)
        {
            _controller.IsPlayingSecondary = false;
            Debug.Log("close secondary animation");
        }
        else
        {
            _controller.IsPlayingSecondary = true;
            Debug.Log("open secondary animation");
        }
    }
}
