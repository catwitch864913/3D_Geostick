using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    public CanvasGroup transitionCanvasGroup;
    public float transitionDuration = 1.0f;

    private void Start()
    {
        // �b�}�l��Ĳ�o����ĪG
        StartTransition();
    }

    public void StartTransition()
    {
        // �H�J����ĪG
        transitionCanvasGroup.alpha = 0;
        transitionCanvasGroup.blocksRaycasts = true; // �i�H�����I���ƥ�

        // �ϥΨ�{��{���ܮĪG
        StartCoroutine(FadeInTransition());
    }

    private IEnumerator FadeInTransition()
    {
        float elapsedTime = 0;

        while (elapsedTime < transitionDuration)
        {
            // �p���e��Alpha��
            float normalizedTime = elapsedTime / transitionDuration;
            transitionCanvasGroup.alpha = Mathf.Lerp(1, 0, normalizedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ܫ�A��������ĪG
        transitionCanvasGroup.alpha = 0;
        transitionCanvasGroup.blocksRaycasts = false; // �������I���ƥ�
    }
    
}
