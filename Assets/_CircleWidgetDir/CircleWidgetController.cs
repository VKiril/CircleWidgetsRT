using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CircleWidgetController : MonoBehaviour {

    protected Animator _animator;
    public Camera cam;

    public GameObject circleWidget;
    public GameObject cameraHolder;
    public float speed = 5;

    void Start()
    {
        _animator = GetComponent<Animator>();
        circleWidget = GameObject.Find("Canvas/CircleWidget");
        cameraHolder = GameObject.Find("MainCamera");
        cam = cameraHolder.GetComponent<Camera>();
    }

    public void Update()
    {
        Vector3 targetDir = transform.position - cam.transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public bool IsPlayingSecondary
    {
        get { return _animator.GetBool("IsPlayingSecondary"); }
        set { _animator.SetBool("IsPlayingSecondary", value); }
    }

    public bool IsWidgetShown
    {
        get { return _animator.GetBool("IsWidgetShown"); }
        set { _animator.SetBool("IsWidgetShown", value); }
    }

    public void SetIsWidgetShown(bool value)
    {
        IsWidgetShown = value;
    }

    public void SetIsPlayingSecondary(bool value)
    {
        IsPlayingSecondary = value;
    }

    public void TestFunction()
    {
        Debug.Log("test button was clicked.");
    }

    public void MainButtonClick()
    {
        Debug.Log(" main button pressed ");

        if (IsPlayingSecondary == true)
        {
            IsPlayingSecondary = false;
            _animator.speed = 1.9f;
        }
        else
        {
            IsPlayingSecondary = true;
            _animator.speed = 1.5f;
        }
    }

    public void showWidget(bool visibility)
    {
        circleWidget.SetActive(visibility);
    }





    public void ButtonUpLeftClick()
    {
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }

    public void ButtonBottomLeftClick()
    {
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }

    public void ButtonUpRightClick()
    {
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }

    public void ButtonBottomRightClick()
    {
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }
}
