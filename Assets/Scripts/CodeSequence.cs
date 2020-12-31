using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to generate the code sequences that the player tries to find on the code matrix.
/// - 'itemCount' defines the number of codes the player will have to hack.
/// - 'minCodeSize' and 'maxCodeSize' defines the size for each code.
/// </summary>
public class CodeSequence : MonoBehaviour
{
    [SerializeField]
    private int itemCount;

    [SerializeField]
    private int minCodeSize;

    [SerializeField]
    private int maxCodeSize;

    [SerializeField]
    private CodeSequenceItem codeSequenceItem;

    [SerializeField]
    private StringMatrixEventChannelSO codeMatrixGeneratedChannel;

    [SerializeField]
    private IntEventChannelSO bufferInitializedChannel;

    [SerializeField]
    private BoolEventChannelSO codeSequenceResultChannel;

    [SerializeField]
    private VoidEventChannelSO gameOverChannel;

    private int bufferSize;

    private List<GameObject> items = new List<GameObject>();

    public int ItemCount { get => itemCount; set => itemCount = value; }

    public int MinCodeSize { get => minCodeSize; set => minCodeSize = value; }

    public int MaxCodeSize { get => maxCodeSize; set => maxCodeSize = value; }

    private void Start()
    {
        codeMatrixGeneratedChannel.OnEventRaised += OnCodeMatrixGenerated;
        bufferInitializedChannel.OnEventRaised += OnBufferInitialized;
        codeSequenceResultChannel.OnEventRaised += OnCodeSequenceResultReceived;
    }

    private void OnDestroy()
    {
        codeMatrixGeneratedChannel.OnEventRaised -= OnCodeMatrixGenerated;
        bufferInitializedChannel.OnEventRaised -= OnBufferInitialized;
        codeSequenceResultChannel.OnEventRaised -= OnCodeSequenceResultReceived;
    }

    private void OnCodeMatrixGenerated(GameObject sender, string[,] value)
    {
        for (int i = 0; i < itemCount; i++)
        {
            CodeSequenceItem item = Instantiate(codeSequenceItem, transform);

            // TODO: right now, this is just a random code. Add a way to always create a valid code.
            List<string> code = new List<string>();
            int codeSize = Random.Range(MinCodeSize, maxCodeSize + 1);
            for (int j = 0; j < codeSize; j++)
            {
                code.Add(value[Random.Range(0, value.GetLength(0)), Random.Range(0, value.GetLength(0))]);
            }

            item.SetCode(code, bufferSize);
        }
    }

    private void OnBufferInitialized(GameObject sender, int size)
    {
        bufferSize = size;
    }

    private void OnCodeSequenceResultReceived(GameObject sender, bool result)
    {
        if (!items.Contains(sender))
        {
            items.Add(sender);
        }

        if (items.Count == itemCount)
        {
            gameOverChannel.RaiseEvent(gameObject);
        }
    }
}
