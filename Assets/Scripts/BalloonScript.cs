using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BalloonScript : MonoBehaviour
{
    // The balloon button
    private Button Button;

    // Floating variables
    public float amplitude = 4;
    public float speed = 2.5f;
    private TMP_Text prizeText;

    // Start is called before the first frame update
    void Start()
    {
        prizeText = GameManager.Instance._prizeText;

        // Object definitions
        Button = gameObject.GetComponent<Button>();
        Button.onClick.AddListener(roll);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.position;
        p.y = amplitude * Mathf.Cos(Time.time * speed);
        transform.position = p;
    }

    public async void roll()
    {
        SetColor();
        GameManager.Instance.hasClicked = true;
        // Unrender Button
        Button.image.enabled = false;

        // Get price
        PriceInfo price = await SupabaseClient.GetPriceInfo();

        // Update text
        prizeText.text = price.name + " - " + price.message;
        // bottom text = price.message
    }

    void SetColor()
    {
        if (gameObject.CompareTag("BlueBalloon"))
        {
            // Set text color
            // Convert the hex color code to a Color object
            Color desiredColorBlue = HexToColor("#004cd3");
            prizeText.color = desiredColorBlue;
        }
        else if (gameObject.CompareTag("RedBalloon"))
        {
            Color desiredColorRed = HexToColor("#ff0310");
            prizeText.color = desiredColorRed;
        }
    }

    // HexToColor function converts a hexadecimal color code into a Unity Color object.
    // The ColorUtility.TryParseHtmlString method is used to perform this conversion.
    // If the conversion is successful, the color is set as the text color for the TextMeshPro component.
    Color HexToColor(string hex)
    {
        Color color = Color.black; // Default color if parsing fails

        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }

        return color;
    }
}
