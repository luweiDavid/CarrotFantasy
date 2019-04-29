using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.Events;

/// <summary>
/// 实现Item的左右滑动页面效果，可以一次滑动一页，也可以一次滑动多页
/// </summary>
public class ScrollRectExtension : MonoBehaviour, IBeginDragHandler, IEndDragHandler
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
    public int CurItemIndex {
        get {
            return curItemIndex;
        }
    }
    private int itemCount;
    public int ItemCount
    {
        get {
            return itemCount;
        }
    }

    private float[] valueArray;

    [Tooltip("是否可以滑动多页")]
    public bool CanMoveMultiPages = true;
    [Tooltip("缓动速度")]
    public float Speed = 0.8f;
    public Ease EaseEffect = Ease.Linear;

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

    /// <summary>
    /// 
    /// </summary> 
    public void AddScrollValueChangedAction(UnityAction<Vector2> cb) {
        if (m_scrollRect == null) {
            m_scrollRect = transform.GetComponent<ScrollRect>(); 
        }
        m_scrollRect.onValueChanged.AddListener(cb);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseBeginX = Input.mousePosition.x;
        //Debug.Log("begin x: " + mouseBeginX); 
    }

    public void OnEndDrag(PointerEventData eventData)
    { 
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
                    if (curItemIndex > itemCount - 1)
                    {
                        return;
                    }
                    curItemIndex += moveCount;
                    curItemIndex = curItemIndex > (itemCount - 1) ? (itemCount - 1) : curItemIndex; 
                } 
            }
        }
        else {
            if (CanMoveMultiPages)
            {
                if ((Mathf.Abs(offset) - firstItemMoveDis) > 0)
                {
                    int moveCount = Mathf.FloorToInt((Mathf.Abs(offset) - firstItemMoveDis) / oneItemMoveDis) + 1;
                    if (curItemIndex < 0)
                    {
                        return;
                    }
                    curItemIndex -= moveCount;
                    curItemIndex = curItemIndex < 0 ? 0 : curItemIndex; 
                } 
            } 
        } 

        DOTween.To(()=>m_scrollRect.horizontalNormalizedPosition, lerpV => m_scrollRect.horizontalNormalizedPosition = lerpV,
            valueArray[curItemIndex], Speed).SetEase(EaseEffect);
    }

    public void MoveToNextPage() {
        curItemIndex += 1;
        curItemIndex = curItemIndex > (itemCount - 1) ? (itemCount - 1) : curItemIndex;

        DOTween.To(() => m_scrollRect.horizontalNormalizedPosition, lerpV => m_scrollRect.horizontalNormalizedPosition = lerpV,
            valueArray[curItemIndex], Speed).SetEase(EaseEffect);
    }

    public void MoveToPrePage() {
        curItemIndex -= 1;
        curItemIndex = curItemIndex < 0 ? 0 : curItemIndex;

        DOTween.To(() => m_scrollRect.horizontalNormalizedPosition, lerpV => m_scrollRect.horizontalNormalizedPosition = lerpV,
            valueArray[curItemIndex], Speed).SetEase(EaseEffect);
    } 

}
