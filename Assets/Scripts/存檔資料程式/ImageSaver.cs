using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageSaver : MonoBehaviour
{
    //public Texture2D image;
    #region 存單張圖片
    /*public Image sourceImage;

    public void SaveImage()
    {
        // 获取Source Image的纹理
        Texture2D texture = GetTextureFromImage(sourceImage);

        // 将Texture2D转换为字节数组
        byte[] bytes = texture.EncodeToPNG();

        // 设置保存路径和文件名
        string filePath = Path.Combine(Application.persistentDataPath, "savedImage.png");

        // 保存字节数组到文件
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Image saved to: " + filePath);
        print(bytes);
    }

    private Texture2D GetTextureFromImage(Image image)
    {
        // 创建一个新的RenderTexture，并将Image的纹理渲染到其中
        RenderTexture renderTexture = new RenderTexture(image.mainTexture.width, image.mainTexture.height, 32);
        Graphics.Blit(image.mainTexture, renderTexture);

        // 创建一个新的Texture2D，并将RenderTexture的像素数据拷贝到其中
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // 释放临时的RenderTexture资源
        RenderTexture.active = null;
        Destroy(renderTexture);

        return texture;
    }*/
    #endregion
    #region 存多張圖片_失敗
    public Image[] imageArray;

    public void SaveImageArray(string directoryPath)
    {
        for (int i = 0; i < imageArray.Length; i++)
        {
            Texture2D texture = GetTextureFromImage(imageArray[i]);
            byte[] bytes = texture.EncodeToPNG();
            string fileName = "image_" + i + "_" + GetUniqueIdentifier() + ".png";
            string filePath = Path.Combine(directoryPath, fileName);
            File.WriteAllBytes(filePath, bytes);
            Debug.Log("Image saved to file: " + filePath);
        }
    }
    private Texture2D GetTextureFromImage(Image image)
    {
        // 创建一个新的RenderTexture，并将Image的纹理渲染到其中
        RenderTexture renderTexture = new RenderTexture(image.mainTexture.width, image.mainTexture.height, 32);
        Graphics.Blit(image.mainTexture, renderTexture);

        // 创建一个新的Texture2D，并将RenderTexture的像素数据拷贝到其中
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // 释放临时的RenderTexture资源
        RenderTexture.active = null;
        Destroy(renderTexture);

        return texture;
    }

    private string GetUniqueIdentifier()
    {
        // 使用时间戳生成唯一标识符
        string timeStamp = System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
        return timeStamp;
    }
    #endregion
}
