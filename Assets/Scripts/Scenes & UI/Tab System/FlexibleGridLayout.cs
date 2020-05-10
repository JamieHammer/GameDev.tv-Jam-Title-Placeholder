using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Improvement to the grid layout, making it more flexible.
/// </summary>

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform, Width, Height, FixedRows, FixedColoumns
    }

    public FitType fitType;

    public int rows;
    public int coloumns;

    public Vector2 cellSize;

    public Vector2 spacing;

    public bool fitX;
    public bool fitY;

    /// <summary>
    /// Builds upon the layout group to make grid layout more flexible.
    /// </summary>

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;

            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            coloumns = Mathf.CeilToInt(sqrRt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColoumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)coloumns);
        }

        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            coloumns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)coloumns - ((spacing.x / (float)coloumns) * 2) - (padding.left / (float)coloumns) - (padding.right / (float)coloumns);
        float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int coloumnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / coloumns;
            coloumnCount = i % coloumns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * coloumnCount) + (spacing.x * coloumnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
        // needs to be overriden, just leave it be
    }

    public override void SetLayoutHorizontal()
    {
        // needs to be overriden, just leave it be
    }

    public override void SetLayoutVertical()
    {
        // needs to be overriden, just leave it be
    }
}
