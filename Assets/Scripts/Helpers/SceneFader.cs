using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public AnimationCurve curve;                // to control the fade curve

    bool fadeState;                             // state of the fade, true = faded in

    Image fadeImage;                            // reference to the fade image

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(rect.offsetMin.x, 0f);
        rect.offsetMax = new Vector2(rect.offsetMax.x, 0f);

        fadeImage = GetComponent<Image>();
        fadeState = false;

        DoTheFadeIn();
    }

    /// <summary>
    /// Handles requests to fade the screen in.
    /// </summary>

    public void DoTheFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// Handles requests to fade the screen out.
    /// </summary>

    public void DoTheFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        if (!fadeState)
        {
            float t = 1f;

            float rg = 247f / 255f;             // converts the bit colour value to a float
            float b = 239f / 255f;              // value of the colour channel / full value

            while (t > 0f)
            {
                t -= Time.deltaTime;

                float a = curve.Evaluate(t);
                fadeImage.color = new Color(rg, rg, b, a);

                yield return 0;
            }
        }

        fadeState = true;
    }

    IEnumerator FadeOut()
    {
        if (fadeState)
        {
            float t = 0f;

            float rg = 247f / 255f;             // converts the bit colour value to a float
            float b = 239f / 255f;              // value of the colour channel / full value

            while (t < 0f)
            {
                t += Time.deltaTime;

                float a = curve.Evaluate(t);
                fadeImage.color = new Color(rg, rg, b, a);

                yield return 0;
            }
        }

        fadeState = false;
    }
}
