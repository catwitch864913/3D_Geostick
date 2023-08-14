using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControl : MonoBehaviour
{
    public Slider slider; // 將Slider元件指定到這個變數

    public Animator animator; // 動畫控制器

    private void Update()
    {
        // 根據Slider的值來控制動畫播放時間
        float normalizedTime = Mathf.Clamp01(slider.value / slider.maxValue);
        animator.Play("ourAnimationName", -1, normalizedTime);
    }
}
