using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    public CanvasGroup transitionCanvasGroup;
    public float transitionDuration = 1.0f;

    private void Start()
    {
        // 在開始時觸發轉場效果
        StartTransition();
    }

    public void StartTransition()
    {
        // 淡入轉場效果
        transitionCanvasGroup.alpha = 0;
        transitionCanvasGroup.blocksRaycasts = true; // 可以接收點擊事件

        // 使用協程實現漸變效果
        StartCoroutine(FadeInTransition());
    }

    private IEnumerator FadeInTransition()
    {
        float elapsedTime = 0;

        while (elapsedTime < transitionDuration)
        {
            // 計算當前的Alpha值
            float normalizedTime = elapsedTime / transitionDuration;
            transitionCanvasGroup.alpha = Mathf.Lerp(1, 0, normalizedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 完全顯示後，隱藏轉場效果
        transitionCanvasGroup.alpha = 0;
        transitionCanvasGroup.blocksRaycasts = false; // 不接收點擊事件
    }
    
}
