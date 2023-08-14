using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public Image targetImage;
    public string imagePath;
    //int a = Random.Range(0, 5);
    void Start()
    {
        LoadImage();
    }

    public void LoadImage()
    {
        // 检查图像文件是否存在
        if (!System.IO.File.Exists(imagePath))
        {
            Debug.LogError("Image file not found at: " + imagePath);
            return;
        }

        // 从文件读取字节数据
        byte[] bytes = System.IO.File.ReadAllBytes(imagePath);

        // 创建新的Texture2D
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        // 将Texture2D应用到UI Image
        targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }
}
