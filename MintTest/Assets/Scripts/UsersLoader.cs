using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UsersLoader: IDisposable
{
    private const string Url = "https://drive.google.com/uc?id=1YvE6Y5-vxVWXrrYyb83ssqMfVUUkndLw";

    public event Action DataLoaded;

    private MonoBehaviour _context;
    private IPersistentData _persistentData;
    private ImageLoader _imageLoader;

    public UsersLoader(MonoBehaviour context, IPersistentData persistentData, ImageLoader imageLoader)
    {
        _context = context;
        _persistentData = persistentData;
        _imageLoader = imageLoader;

        _imageLoader.Imagesloaded += OnImagesLoaded;
    }

    public void Dispose()
    {
        _imageLoader.Imagesloaded -= OnImagesLoaded;
    }

    private void OnImagesLoaded()
    {
        _context.StartCoroutine(DownloadAndParseJson());
    }

    private IEnumerator DownloadAndParseJson()
    {
        UnityWebRequest www = UnityWebRequest.Get(Url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonString = www.downloadHandler.text;

            JsonContainer container = JsonConvert.DeserializeObject<JsonContainer>(jsonString);

            if (container != null && container.data != null && container.data.Count > 0)
            {
                CardData previousCard = container.data[0];
                previousCard.SetIcon(_persistentData.PlayerData.GetRandomSpritePath());

                foreach (CardData card in container.data)
                {
                    string randomSpritePath = _persistentData.PlayerData.GetRandomSpritePath();

                    while (randomSpritePath == null || randomSpritePath == previousCard.IconPath)
                    {
                        randomSpritePath = _persistentData.PlayerData.GetRandomSpritePath();
                    }

                    card.SetIcon(randomSpritePath);
                    previousCard = card;
                    _persistentData.PlayerData.AddCard(card);
                }
            }
            else
            {
                Debug.LogError("Failed to parse JSON data.");
            }
            DataLoaded?.Invoke();
        }
        else
        {
            Debug.LogError("Failed to download JSON file: " + www.error);
        }

        DataLoaded?.Invoke();
    }
}

[Serializable]
public class JsonContainer
{
    public List<CardData> data;
}
