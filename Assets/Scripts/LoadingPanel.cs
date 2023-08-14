using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingPanel : MonoBehaviour
{
    public CanvasGroup canvasGroup;  // ���ù�UI������Canvas Group�ե�
    public float transitionDuration = 1.0f;  // ���ܫ���ɶ�
    public string sceneToLoad;  // �n�[�����ؼг����W��

    public Slider slider;
    public GameObject buttonStart;
    public float curProgress;

    public float loadingTime = 2;

    //public bool really = true;

    //AsyncOperation operation;
    void Start()
    {
        buttonStart.GetComponent<Button>().onClick.AddListener(OnButtonStart);
        buttonStart.SetActive(false);
        curProgress = 0;
        slider.value = curProgress;

        /*if (really)
        {
            operation = SceneManager.LoadSceneAsync("Menu");
            operation.allowSceneActivation = false;
        }*/
    }
    void OnButtonStart()
    {
        StartCoroutine(TransitionToScene());
        //SceneManager.LoadScene("Meun");
        /*if (!really)
        {
            SceneManager.LoadScene("Meun");
        }
        else
        {
            operation.allowSceneActivation= true;
        }*/
    }
    void OnSliderValueChange(float value)
    {
        slider.value = value;
        if (value >= 1.0)
        {
            buttonStart.SetActive(true);
        }
    }
    void Update()
    {
        //if (!really)
        //{
            curProgress += Time.deltaTime / loadingTime;
            if (curProgress > 1.0f)
            {
                curProgress = 1;
            }
            OnSliderValueChange(curProgress);
        //}
       // else
        //{
        //    curProgress = Mathf.Clamp01(operation.progress / 0.9f);
        //    OnSliderValueChange(curProgress);
        //}
    }
    private IEnumerator TransitionToScene()
    {
        float elapsedTime = 0;

        // ���ܶ«̮ĪG
        while (elapsedTime < transitionDuration)
        {
            float normalizedTime = elapsedTime / transitionDuration;
            canvasGroup.alpha = normalizedTime;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �����«̫��������
        canvasGroup.alpha = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}
