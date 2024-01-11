using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Pipeline;
using UnityEngine;

public class MascotAnimation : MonoBehaviour
{

    public RectTransform backImage;
    public RectTransform eyeImage;
    public GameObject tutorialUI;

    public float rotationSpeed;
    public float floatSpeed;
    public float floatHeight;


    public float minAnimationWaitTime;
    public float maxAnimationWaitTime;

    public Animator eyeAnimator;
    public AnimationClip eyeAnimation;

    bool canAnimate = true;
    bool wasTimeSelected = false;
    bool wasActivated = false;
    bool animationStarted = false;
    [Header("TweenAnimation")]
    public float transitionTime;

    public float yDerivation;

    
    Vector2 backPositionUp;
    Vector2 backPositionDown;

    
    Vector2 eyePositionUp;
    Vector2 eyePositionDown;
    public Ease firstEase;
    public Ease animationEase;
    private void Start()
    {
        
        
    }
    // Update is called once per frame
    void Update()
    {
        OutAnimation();
        
        HandleAnimation();
        CheckForEnable();
        StartAnimation();



    }

    public void CheckForEnable()
    {
        if (!wasActivated) return;
        if (!tutorialUI.activeSelf) return;
        wasActivated = true;
    }

    public void StartAnimation()
    {
        if (animationStarted) return;
        backPositionUp = new Vector2(backImage.position.x, backImage.position.y + yDerivation);
        backPositionDown = new Vector2(backImage.position.x, backImage.position.y - yDerivation);


        eyePositionUp = new Vector2(eyeImage.position.x, eyeImage.position.y + yDerivation);
        eyePositionDown = new Vector2(eyeImage.position.x, eyeImage.position.y - yDerivation);
        
        animationStarted = true;

        FloatUp(1, firstEase);
    }

    //tween

    public void FloatUp(int multy, Ease ease)
    {
        backImage.DOMove(backPositionUp, transitionTime * multy).SetEase(ease);
        eyeImage.DOMove(eyePositionUp, transitionTime * multy).SetEase(ease).OnComplete(() => FloatDown(2, animationEase));

        Debug.Log(transitionTime * multy);
    }

    public void FloatDown(int multy, Ease ease)
    {
        backImage.DOMove(backPositionDown, transitionTime * multy).SetEase(ease);
        eyeImage.DOMove(eyePositionDown, transitionTime * multy).SetEase(ease).OnComplete(() => FloatUp(2, animationEase));

        Debug.Log(transitionTime * multy);
    }


    public void OutAnimation()
    {
        backImage.Rotate(Vector3.forward, -(rotationSpeed * Time.deltaTime));
        
    }


    public void HandleAnimation()
    {
        if (!canAnimate) return;
        if (wasTimeSelected) return;
        
        float TimeBewteenAnimation = UnityEngine.Random.Range(minAnimationWaitTime, maxAnimationWaitTime);
        wasTimeSelected = true;
        {
            StartCoroutine(Animation(TimeBewteenAnimation));
        }

    }

    IEnumerator Animation(float time)
    {
        canAnimate = false;

        yield return new WaitForSecondsRealtime(time);

        eyeAnimator.SetBool("canAnimate", true);

        yield return new WaitForSecondsRealtime(eyeAnimation.length);

        eyeAnimator.SetBool("canAnimate", false);
        canAnimate = true;
        wasTimeSelected = false;
    }


}
