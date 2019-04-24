using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 实现Item的左右滑动页面效果，可以一次滑动一页，也可以一次滑动多页
/// </summary>
public class ScrollRectExtension : MonoBehaviour, IBeginDragHandler,IEndDragHandler
{
    private ScrollRect m_scrollRect;
    private RectTransform m_contentRectTr;
    private GridLayoutGroup m_contentGridLG;

    private float leftPadding;
    private float rightPadding;
    private float topPadding;
    private float bottomPadding; 
    private Vector2 cellSize;
    private float xSpacing;
    private float ySpacing;

    //由于leftPadding和xSpacing可能是不一样的，所以需要知道第一个item需要移动的长度
    private float firstItemMoveDis;
    //从第二个item开始，移动到下一个item需要滑动的距离
    private float oneItemMoveDis;
    private float contentWidth;

    private float mouseBeginX;
    private float mouseEndX;

    private int curItemIndex;
    private int itemCount;
    private float[] valueArray;

    public bool CanMoveMultiPages = true;
    private bool IsDragEnd = false;

    private float speed = 5.5f;

    private void Awake()
    {
        m_scrollRect = transform.GetComponent<ScrollRect>();
        m_contentRectTr = m_scrollRect.content;
        m_contentGridLG = m_contentRectTr.GetComponent<GridLayoutGroup>();

        if (m_contentGridLG == null)
        {
            Debug.LogError("请添加GridLayoutGroup组件");
            return;
        }

        leftPadding = m_contentGridLG.padding.left;
        xSpacing = m_contentGridLG.spacing.x;
        cellSize = m_contentGridLG.cellSize;

        firstItemMoveDis = leftPadding + (cellSize.x / 2);
        oneItemMoveDis = xSpacing + (cellSize.x / 2);
        contentWidth = m_contentRectTr.rect.xMax;

        //下表从零开始
        curItemIndex = 0;
        itemCount = m_contentRectTr.childCount;

        valueArray = new float[itemCount]; 
        float intervalValue = 1.0f / (itemCount - 1);
        for (int i = 0; i < itemCount; i++)
        {
            valueArray[i] = intervalValue * i; 
        } 
    } 

    void Update() {
        if (IsDragEnd == true)
        {
            m_scrollRect.horizontalNormalizedPosition = Mathf.Lerp(m_scrollRect.horizontalNormalizedPosition, valueArray[curItemIndex], speed);
            if ((m_scrollRect.horizontalNormalizedPosition - valueArray[curItemIndex]) < 0.005f) {
                m_scrollRect.horizontalNormalizedPosition = valueArray[curItemIndex];
                IsDragEnd = false;
            } 
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseBeginX = Input.mousePosition.x;
        //Debug.Log("begin x: " + mouseBeginX); 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsDragEnd) {
            return;
        }
        mouseEndX = Input.mousePosition.x;
        //offset小于0：左移，  offset大于0： 右移
        float offset = mouseEndX - mouseBeginX;
        //Debug.Log("end x: " + mouseEndX + "   差值：" + (mouseEndX - mouseBeginX));  
        if (offset <= 0)
        { 
            if (CanMoveMultiPages) {
                if ((Mathf.Abs(offset) - firstItemMoveDis) > 0)
                {
                    int moveCount = Mathf.FloorToInt((Mathf.Abs(offset) - firstItemMoveDis) / oneItemMoveDis) + 1;
                    if (curItemIndex >= itemCount - 1)
                    {
                        return;
                    }
                    curItemIndex += moveCount;

                    IsDragEnd = true;
                }
                else {
                    IsDragEnd = false;
                    m_scrollRect.horizontalNormalizedPosition = valueArray[curItemIndex];
                }
            }
        }
        else {
            if (CanMoveMultiPages)
            {
                if ((Mathf.Abs(offset) - firstItemMoveDis) > 0)
                {
                    int moveCount = Mathf.FloorToInt((Mathf.Abs(offset) - firstItemMoveDis) / oneItemMoveDis) + 1;
                    if (curItemIndex <= 0)
                    {
                        return;
                    }
                    curItemIndex -= moveCount;

                    IsDragEnd = true;
                }
                else
                {
                    IsDragEnd = false;
                    m_scrollRect.horizontalNormalizedPosition = valueArray[curItemIndex];
                }
            }

        }
    }

    
}
