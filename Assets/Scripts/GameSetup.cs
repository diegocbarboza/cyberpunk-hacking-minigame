using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to initialize the minigame with random values. It should be replaced if the minigame is used on an actual game.
/// </summary>
public class GameSetup : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private CodeMatrix codeMatrix;

    [SerializeField]
    private Buffer buffer;

    [SerializeField]
    private CodeSequence codeSequence;

    [SerializeField]
    [Range(25, 30)]
    private float minTime = 25;

    [SerializeField]
    [Range(50, 99)]
    private float maxTime = 99;

    [SerializeField]
    [Range(5, 6)]
    private int codeMatrixMinSize = 5;

    [SerializeField]
    [Range(6, 8)]
    private int codeMatrixMaxSize = 8;

    [SerializeField]
    [Range(3, 4)]
    private int bufferMinSize = 3;

    [SerializeField]
    [Range(5, 8)]
    private int bufferMaxSize = 8;

    [SerializeField]
    [Range(2, 3)]
    private int codeSequenceMinItemCount = 2;

    [SerializeField]
    [Range(4, 6)]
    private int codeSequenceMaxItemCount = 6;

    [SerializeField]
    [Range(3, 4)]
    private int codeSequenceMinCodeSize = 3;

    [SerializeField]
    [Range(4, 5)]
    private int codeSequenceMaxCodeSize = 5;

    private void Awake()
    {
        timer.Time = Random.Range(minTime, maxTime + 1);
        codeMatrix.Size = Random.Range(codeMatrixMinSize, codeMatrixMaxSize + 1);
        buffer.Size = Random.Range(bufferMinSize, bufferMaxSize + 1);
        codeSequence.ItemCount = Random.Range(codeSequenceMinItemCount, codeSequenceMaxItemCount + 1);
        codeSequence.MinCodeSize = codeSequenceMinCodeSize;
        codeSequence.MaxCodeSize = Mathf.Min(buffer.Size, codeSequenceMaxCodeSize);
    }
}
