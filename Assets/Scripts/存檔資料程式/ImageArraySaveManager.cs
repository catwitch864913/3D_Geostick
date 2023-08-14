using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageArraySaveManager : MonoBehaviour
{
    public Image[] imageArray;
    public RawImage mergedImage;
    public string directoryPath;

    private List<Vector2> imagePositions;

    public void SaveImageArray()
    {
        Texture2D mergedTexture = MergeImages(imageArray);
        byte[] bytes = mergedTexture.EncodeToPNG();
        string fileName = "merged_image_" + GetUniqueIdentifier() + ".png";
        string filePath = Path.Combine(directoryPath, fileName);
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Merged Image saved to file: " + filePath);
        imagePositions = new List<Vector2>();

        // Record the position of each image in the merged image
        foreach (Image image in imageArray)
        {
            Rect imageRect = image.rectTransform.rect;
            Vector2 imagePosition = new Vector2(imageRect.x, imageRect.y);
            imagePositions.Add(imagePosition);
        }
    }

    public void LoadImageArray()
    {
        string filePath = GetLatestMergedImagePath();
        Texture2D mergedTexture = LoadTextureFromFile(filePath);
        mergedImage.texture = mergedTexture;

        for (int i = 0; i < imageArray.Length; i++)
        {
            Vector2 imagePosition = imagePositions[i];
            Rect imageRect = new Rect(imagePosition.x, imagePosition.y, imageArray[i].rectTransform.rect.width, imageArray[i].rectTransform.rect.height);
            Sprite sprite = Sprite.Create(mergedTexture, imageRect, new Vector2(0.5f, 0.5f));
            imageArray[i].sprite = sprite;
        }
    }

    private Texture2D MergeImages(Image[] images)
    {
        int width = 0;
        int height = 0;

        foreach (Image image in images)
        {
            Rect imageRect = image.rectTransform.rect;
            width = Mathf.Max(width, Mathf.RoundToInt(imageRect.x + imageRect.width));
            height = Mathf.Max(height, Mathf.RoundToInt(imageRect.y + imageRect.height));
        }

        Texture2D mergedTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        foreach (Image image in images)
        {
            Rect imageRect = image.rectTransform.rect;
            Vector2 imagePosition = new Vector2(imageRect.x, imageRect.y);
            Color[] pixels = image.sprite.texture.GetPixels(Mathf.RoundToInt(imageRect.x), Mathf.RoundToInt(imageRect.y), Mathf.RoundToInt(imageRect.width), Mathf.RoundToInt(imageRect.height));
            mergedTexture.SetPixels(Mathf.RoundToInt(imagePosition.x), Mathf.RoundToInt(imagePosition.y), Mathf.RoundToInt(imageRect.width), Mathf.RoundToInt(imageRect.height), pixels);
        }

        mergedTexture.Apply();

        return mergedTexture;
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] bytes = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes);
        return texture;
    }

    private string GetLatestMergedImagePath()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
        FileInfo[] files = directoryInfo.GetFiles("merged_image_*.png");
        FileInfo latestFile = null;

        foreach (FileInfo file in files)
        {
            if (latestFile == null || file.LastWriteTime > latestFile.LastWriteTime)
            {
                latestFile = file;
            }
        }

        return latestFile != null ? latestFile.FullName : string.Empty;
    }

    private string GetUniqueIdentifier()
    {
        return System.DateTime.Now.Ticks.ToString();
    }
}
