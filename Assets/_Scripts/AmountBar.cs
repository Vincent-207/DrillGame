using UnityEngine;

public class AmountBar : MonoBehaviour
{
    [SerializeField]
    RectTransform behindTransform, colorTransform;
    [SerializeField]
    float amount, maxAmount;
    void Start()
    {
        SetAmount(maxAmount);
        
    }
    public void SetMaxAmount(float input)
    {
        maxAmount = input;
        SetAmount(input);
    }
    public void SetAmount(float input)
    {
        amount = input;
        float maxWidth = behindTransform.sizeDelta.x * 2;
        float amountPercent = amount / maxAmount;
        float colorWidth = maxWidth * amountPercent;
        SetColorWidth(colorWidth);

    }

    void SetColorWidth(float width)
    {
        Vector2 size = colorTransform.sizeDelta;
        size.x = width;
        colorTransform.sizeDelta = size;
    }

}
