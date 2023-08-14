using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControl : MonoBehaviour
{
    public Slider slider; // �NSlider������w��o���ܼ�

    public Animator animator; // �ʵe���

    private void Update()
    {
        // �ھ�Slider���Ȩӱ���ʵe����ɶ�
        float normalizedTime = Mathf.Clamp01(slider.value / slider.maxValue);
        animator.Play("ourAnimationName", -1, normalizedTime);
    }
}
