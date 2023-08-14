using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackScreenTransition : MonoBehaviour
{
    public CanvasGroup canvasGroup;  // 全螢幕UI元素的Canvas Group組件
    public float transitionDuration = 1.0f;  // 漸變持續時間
    public string sceneToLoad;  // 要加載的目標場景名稱

    private void Start()
    {
        // 開始時觸發黑屏漸變效果
        //StartCoroutine(TransitionToScene());
    }
    
    public void 按鈕按下換下個場景()
    {
        StartCoroutine(TransitionToScene());
    }

    private IEnumerator TransitionToScene()
    {
        float elapsedTime = 0;

        // 漸變黑屏效果
        while (elapsedTime < transitionDuration)
        {
            float normalizedTime = elapsedTime / transitionDuration;
            canvasGroup.alpha = normalizedTime;

            elapsedTime += Time.deltaTime/10;
            yield return null;
        }

        // 完全黑屏後切換場景
        canvasGroup.alpha = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}
