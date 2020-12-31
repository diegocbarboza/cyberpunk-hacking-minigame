using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to define a code sequence that the player is trying to find on the code matrix.
/// </summary>
public class CodeSequenceItem : TextItem
{
    [SerializeField]
    private RectTransform root;
        
    [SerializeField]
    private TextItem codeSequenceTextItem;

    [SerializeField]
    private Image resultImage;

    [SerializeField]
    private Text resultText;

    [SerializeField]
    private Color successColor;

    [SerializeField]
    private Color failColor;

    [SerializeField]
    private StringListEventChannelSO bufferChangedChannel;

    [SerializeField]
    private VoidEventChannelSO timeOutChannel;

    [SerializeField]
    private BoolEventChannelSO codeSequenceResultChannel;

    private List<TextItem> textItems = new List<TextItem>();

    private bool hasValidCode = false;

    private bool hasFailed = false;

    private int bufferSize;

    private void Awake()
    {
        bufferChangedChannel.OnEventRaised += OnBufferChanged;
        timeOutChannel.OnEventRaised += OnTimeOut;
    }

    private void OnDestroy()
    {
        bufferChangedChannel.OnEventRaised -= OnBufferChanged;
        timeOutChannel.OnEventRaised -= OnTimeOut;
    }

    private void OnBufferChanged(GameObject sender, List<string> codeBuffer)
    {
        if (!hasValidCode)
        {
            int correctItems = ValidateCode(codeBuffer);
            hasValidCode = correctItems == textItems.Count;
            hasFailed = !hasValidCode && (textItems.Count - correctItems > bufferSize - codeBuffer.Count); // if 'code size - correct items > remaing buffer slots', fail

            for (int i = 0; i < textItems.Count; i++)
            {
                textItems[i].TextObject.color = i < correctItems ? Color.cyan : Color.white;
            }

            if (hasValidCode)
            {
                ShowSuccess();
            }
            else if (hasFailed)
            {
                ShowFailure();
            }
        }
    }

    private void OnTimeOut(GameObject sender)
    {
        if (!hasValidCode)
        {
            ShowFailure();
        }
    }

    private int ValidateCode(List<string> codeBuffer)
    {
        int correctItems = textItems.Count;
        bool validCode;

        do
        {
            validCode = true;

            for (int i = codeBuffer.Count - correctItems, j = 0; i < codeBuffer.Count; i++, j++)
            {
                if (i < 0 || codeBuffer[i] != textItems[j].TextObject.text)
                {
                    validCode = false;
                    correctItems--;
                    break;
                }
            }
        } while (!validCode && correctItems > 0);

        return correctItems;
    }

    private void ShowSuccess()
    {
        resultImage.color = successColor;
        resultText.text = "SUCCESS";
        resultImage.gameObject.SetActive(true);

        codeSequenceResultChannel.RaiseEvent(gameObject, true);
    }

    private void ShowFailure()
    {
        resultImage.color = failColor;
        resultText.text = "FAILED";
        resultImage.gameObject.SetActive(true);

        codeSequenceResultChannel.RaiseEvent(gameObject, false);
    }

    public void SetCode(List<string> code, int bufferSize)
    {
        code.ForEach(c =>
        {
            TextItem item = Instantiate(codeSequenceTextItem, root);
            item.TextObject.text = c;
            textItems.Add(item);
        });

        this.bufferSize = bufferSize;
    }
}
