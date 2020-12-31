using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class stores a list of the codes selected by the player, which are compared with the code sequences.
/// - 'size' specifies the number of slots.
/// </summary>
public class Buffer : MonoBehaviour
{
    [SerializeField]
    private int size;

    [SerializeField]
    private TextItem bufferItem;

    [SerializeField]
    private StringEventChannelSO codeSelectedChannel;

    [SerializeField]
    private IntEventChannelSO bufferInitializedChannel;

    [SerializeField]
    private StringListEventChannelSO bufferChangedChannel;

    [SerializeField]
    private VoidEventChannelSO gameOverChannel;

    private Queue<Text> bufferTexts = new Queue<Text>();

    private List<string> codeBuffer = new List<string>(); 
    
    public int Size { get => size; set => size = value; }

    private void Awake()
    {
        codeSelectedChannel.OnEventRaised += OnCodeSelected;
    }

    private void Start()
    {
        for (int i = 0; i < size; i++)
        {
            TextItem item = Instantiate(bufferItem, transform);
            item.TextObject.text = "";
            bufferTexts.Enqueue(item.TextObject);
        }

        bufferInitializedChannel.RaiseEvent(gameObject, size);
    }

    private void OnDestroy()
    {
        codeSelectedChannel.OnEventRaised -= OnCodeSelected;
    }

    private void OnCodeSelected(GameObject sender, string code)
    {
        Text t = bufferTexts.Dequeue();
        t.text = code;

        codeBuffer.Add(code);
        bufferChangedChannel.RaiseEvent(gameObject, codeBuffer);

        if (codeBuffer.Count == size)
        {
            gameOverChannel.RaiseEvent(gameObject);
        }
    }
}
