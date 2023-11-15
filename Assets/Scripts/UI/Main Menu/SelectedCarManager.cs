using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCarManager : MonoBehaviour
{
    PlayerData playerData;
    SpriteRenderer spriteRenderer;
    int rotationSpeed;
    Vector3 rotate;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 30;
        rotate = new Vector3(0, 0, -1);
        playerData = PlayerData.Instance;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        SetSelectedCar(spriteRenderer, playerData.GetCarList()[0].Texture);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = playerData.GetCar().Texture;
        gameObject.transform.Rotate(rotate * (rotationSpeed * Time.deltaTime));
    }

    public static void SetSelectedCar(SpriteRenderer spriteRenderer, Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
