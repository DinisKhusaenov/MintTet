using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [JsonProperty]
    public bool IsFavorite;
    [JsonProperty]
    private int id;
    [JsonProperty]
    private string first_name;
    [JsonProperty]
    private string last_name;
    [JsonProperty]
    private string email;
    [JsonProperty]
    private string gender;
    [JsonProperty]
    private string ip_address;
    [JsonProperty]
    private string _iconPath;

    public CardData(int id, string first_name, string last_name, string email, string gender, string ip_address, string iconPath)
    {
        this.id = id;
        this.first_name = first_name;
        this.last_name = last_name;
        this.gender = gender;
        this.email = email;
        this.ip_address = ip_address;
        this._iconPath = iconPath;
    }

    public string IconPath => _iconPath;

    public string FirstName => first_name;

    public string LastName => last_name;

    public string Gender => gender;

    public string Mail => email;

    public string Ip => ip_address;

    public Sprite GetIcon() => LoadSpriteFromDisk(_iconPath);

    public void SetIcon(string iconPath)
    {
        if (iconPath != null)
            _iconPath = iconPath;
    }

    private Sprite LoadSpriteFromDisk(string path)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}
