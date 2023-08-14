using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text dialogueText;
    public string[] dialogues;
    public float textSpeed = 0.05f;
    private int currentDialogueIndex = 0;

    private Coroutine typingCoroutine;
    private RectTransform dialogueTextRectTransform;
    private TextGenerator textGenerator;

    private void Start()
    {
        dialogueTextRectTransform = dialogueText.GetComponent<RectTransform>();
        textGenerator = new TextGenerator();
        ShowDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    public void ShowDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeDialogue());
    }

    IEnumerator TypeDialogue()
    {
        dialogueText.text = string.Empty;

        StringBuilder stringBuilder = new StringBuilder();
        string[] words = dialogues[currentDialogueIndex].Split(' ');

        for (int i = 0; i < dialogues[currentDialogueIndex].Length; i++)
        {
            stringBuilder.Append(dialogues[currentDialogueIndex][i]);
            dialogueText.text = stringBuilder.ToString();
            yield return new WaitForSeconds(textSpeed);
        }
        for (int i = 0; i < words.Length; i++)
        {
            stringBuilder.Append(words[i] + " ");
            dialogueText.text = stringBuilder.ToString();

            // 如果文本框宽度超出可见区域，换行
            if (IsTextOverflow())
            {
                stringBuilder.Length -= words[i].Length + 1;
                dialogueText.text = stringBuilder.ToString();
                dialogueText.text += "\n" + words[i] + " ";
                //stringBuilder.Clear();
                //stringBuilder.Append(words[i] + " ");
            }

            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogues[currentDialogueIndex];
        }

        if (currentDialogueIndex < dialogues.Length - 1)
        {
            currentDialogueIndex++;
            ShowDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        // 在这里可以执行对话结束后的任何操作
        Debug.Log("对话结束");
    }

    private bool IsTextOverflow()
    {
        TextGenerationSettings generationSettings = dialogueText.GetGenerationSettings(dialogueTextRectTransform.rect.size);
        float textWidth = textGenerator.GetPreferredWidth(dialogueText.text, generationSettings);

        return textWidth > dialogueTextRectTransform.rect.width;
    }
}
