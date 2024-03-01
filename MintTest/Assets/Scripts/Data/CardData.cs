using System;
using UnityEngine.UI;

[Serializable]
public class CardData
{
    private Image _icon;
    private string _firstName;
    private string _lastName;
    private string _gender;
    private string _mail;
    private string _ip;

    public CardData(string firstName, string lastName, string gender, string mail, string ip, Image icon)
    {
        _firstName = firstName;
        _lastName = lastName;
        _gender = gender;
        _mail = mail;
        _ip = ip;
        _icon = icon;
    }

    public Image Icon => _icon;
    public string FirstName => _firstName;
    public string LastName => _lastName;
    public string Gender => _gender;
    public string Mail => _mail;
    public string Ip => _ip;
}
