using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopButtonManager : MonoBehaviour
{
    public TextMeshProUGUI NameObject;
    public TextMeshProUGUI ButtonValueObject;
    public GameObject PaymentObject;

    PlayerData playerData;
    string Name;
    float value;

    // Start is called before the first frame update
    void Start()
    {
        Name = NameObject.text;
        value = CarManager.GetCarByName(Name).Cost;
        ButtonValueObject.text = value.ToString();
        playerData = PlayerData.Instance;
    }

    public void OnBuy()
    {
        ButtonValueObject.transform.position = new Vector3(ButtonValueObject.transform.position.x, 0, -1);
        PaymentObject.transform.position = new Vector3(PaymentObject.transform.position.x, 0, -1);
    }
}
