using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackScreenTransition : MonoBehaviour
{
    public CanvasGroup canvasGroup;  // ���ù�UI������Canvas Group�ե�
    public float transitionDuration = 1.0f;  // ���ܫ���ɶ�
    public string sceneToLoad;  // �n�[�����ؼг����W��

    private void Start()
    {
        // �}�l��Ĳ�o�«̺��ܮĪG
        //StartCoroutine(TransitionToScene());
    }
    
    public void ���s���U���U�ӳ���()
    {
        StartCoroutine(TransitionToScene());
    }

    private IEnumerator TransitionToScene()
    {
        float elapsedTime = 0;

        // ���ܶ«̮ĪG
        while (elapsedTime < transitionDuration)
        {
            float normalizedTime = elapsedTime / transitionDuration;
            canvasGroup.alpha = normalizedTime;

            elapsedTime += Time.deltaTime/10;
            yield return null;
        }

        // �����«̫��������
        canvasGroup.alpha = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}
