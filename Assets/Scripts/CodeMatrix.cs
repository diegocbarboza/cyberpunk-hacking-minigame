using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This class is used to store the codes that the player can select to send to the buffer.
/// - 'codeOptions' contais a list with the possible code strings.
/// - 'size' specifies the size of the code matrix.
/// </summary>
public class CodeMatrix : MonoBehaviour
{
    [SerializeField]
    private CodeOptions codeOptions;

    [SerializeField]
    private int size;

    [SerializeField]
    private RectTransform codeMatrixGrid;

    [SerializeField]
    private CodeMatrixItem codeMatrixItem;

    [SerializeField]
    private Image horizontalBar;

    [SerializeField]
    private Image verticalBar;

    [SerializeField]
    private Color currentAxisColor;

    [SerializeField]
    private Color otherAxisColor;

    [SerializeField]
    private StringEventChannelSO codeSelectedChannel;

    [SerializeField]
    private VoidEventChannelSO gameOverChannel;

    [SerializeField]
    private StringMatrixEventChannelSO codeMatrixGeneratedChannel;

    private Axis currentAxis = Axis.Horizontal;

    private Vector2Int currentLocation = Vector2Int.zero;

    public int Size { get => size; set => size = value; }

    private void Awake()
    {
        codeSelectedChannel.OnEventRaised += OnCodeSelected;
        gameOverChannel.OnEventRaised += OnGameOver;
    }

    private void Start()
    {
        codeMatrixGrid.GetComponent<GridLayoutGroup>().constraintCount = size;

        string[,] codeMatrix = new string[size, size];

        for (int line = 0; line < size; line++)
        {
            for (int column = 0; column < size; column++)
            {
                CodeMatrixItem item = Instantiate(codeMatrixItem, codeMatrixGrid);
                item.Location = new Vector2Int(column, line);
                string code = codeOptions.codes[Random.Range(0, codeOptions.codes.Count)];
                item.TextObject.text = code;
                
                item.ButtonObject.onClick.AddListener(() => OnItemClicked(item));

                EventTrigger.Entry entry = item.EventTrigger.triggers.Find(e => e.eventID == EventTriggerType.PointerEnter);
                entry.callback.AddListener((eventData) => UpdateBars(item.GetComponent<RectTransform>()));

                codeMatrix[line, column] = code;
            }
        }

        codeMatrixGeneratedChannel.RaiseEvent(gameObject, codeMatrix);
    }

    private void OnDestroy()
    {
        codeSelectedChannel.OnEventRaised -= OnCodeSelected;
        gameOverChannel.OnEventRaised -= OnGameOver;
    }

    private void OnItemClicked(CodeMatrixItem item)
    {
        if (enabled)
        {
            if ((currentAxis == Axis.Horizontal && item.Location.y == currentLocation.y) || (currentAxis == Axis.Vertical && item.Location.x == currentLocation.x))
            {
                codeSelectedChannel.RaiseEvent(item.gameObject, item.TextObject.text);
                item.MarkAsUsed();
            }
        }
    }

    private void OnCodeSelected(GameObject sender, string code)
    {
        CodeMatrixItem item = sender.GetComponent<CodeMatrixItem>();

        currentLocation = item.Location;

        currentAxis = currentAxis == Axis.Horizontal ? currentAxis = Axis.Vertical : Axis.Horizontal;
        UpdateBars(sender.GetComponent<RectTransform>());
    }
    private void OnGameOver(GameObject sender)
    {
        enabled = false;
    }

    private void UpdateBars(RectTransform rectTransform)
    {
        verticalBar.gameObject.SetActive(true);
        verticalBar.color = currentAxis == Axis.Vertical ? currentAxisColor : otherAxisColor;
        horizontalBar.color = currentAxis == Axis.Horizontal ? currentAxisColor : otherAxisColor;

        switch (currentAxis)
        {
            case Axis.Vertical:
                {
                    RectTransform horizontalRectTranform = horizontalBar.GetComponent<RectTransform>();
                    Vector2 horizontalBarPosition = horizontalRectTranform.anchoredPosition;
                    horizontalBarPosition.y = rectTransform.anchoredPosition.y;
                    horizontalRectTranform.anchoredPosition = horizontalBarPosition;
                }
                break;
            case Axis.Horizontal:
                {
                    RectTransform verticalRectTransform = verticalBar.GetComponent<RectTransform>();
                    Vector2 verticalBarPosition = verticalRectTransform.anchoredPosition;
                    verticalBarPosition.x = rectTransform.anchoredPosition.x;
                    verticalRectTransform.anchoredPosition = verticalBarPosition;
                }
                break;
            default:
                break;
        }
    }
}
