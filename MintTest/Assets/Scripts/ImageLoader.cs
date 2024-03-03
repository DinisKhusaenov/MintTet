using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ImageLoader
{
    private const int NumberOfImages = 5;
    private const string SavedImages = "Assets/Sprites/SavedSprites/sprite_";
    private const string BaseUrl = "https://loremflickr.com/600/450";

    public event Action Imagesloaded;

    private IPersistentData _data;
    private MonoBehaviour _context;

    public ImageLoader(MonoBehaviour context, IPersistentData data)
    {
        _context = context;
        _data = data;

        if (_data.PlayerData.IsStartDataLoaded) return;

        for (int i = 0; i < NumberOfImages; i++)
        {
            _context.StartCoroutine(DownloadAndSaveImage(i));
        }
    }

    private IEnumerator DownloadAndSaveImage(int count)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(BaseUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            _data.PlayerData.AddImagePath(SaveSpriteToDisk(sprite));
        }
        else
        {
            Debug.LogError("Failed to download image: " + www.error);
        }

        if(count == NumberOfImages - 1)
            Imagesloaded?.Invoke();
    }

    private string SaveSpriteToDisk(Sprite sprite)
    {
        byte[] bytes = sprite.texture.EncodeToPNG();
        string path = SavedImages + Guid.NewGuid().ToString() + ".png";
        System.IO.File.WriteAllBytes(path, bytes);
        return path;
    }
}
