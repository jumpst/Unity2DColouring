using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Paint : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    /// <summary>
    /// 钢笔半径
    /// </summary>
    public int PenRadius;
    public Color ColorValue;
    public Transform FixPosPoint;
    public RawImage FishImage;
    Color[] originColors;
    Color[] setColors;
    int width;
    int height;

    Texture2D m_Texture;
    RectTransform m_RectTransform;

    Color[] selectColors;
    Texture2D slectColor;

    HashSet<int> pixetPos = new HashSet<int>();
    private void Awake()
    {
        m_Texture = GetComponent<RawImage>().texture as Texture2D;
        m_RectTransform = GetComponent<RectTransform>();
        originColors = m_Texture.GetPixels();
        setColors = m_Texture.GetPixels();
        width = m_Texture.width;
        height = m_Texture.height;
        FillPos();

    }

    private void OnApplicationQuit()
    {
        Reset();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Brush(eventData.position);

    }


    private void OnDestroy()
    {
        Reset();
    }

    int counter = 0;
    void FillPos()
    {
        var texture = FishImage.texture as Texture2D;
        var pixels = texture.GetPixels();
        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                var index = i * width + j;
                if (pixels[index].a != 0)
                {
                    counter++;
                }
            }
        }
    }

    public void Reset()
    {
        m_Texture.SetPixels(originColors);
        m_Texture.Apply();

        pixetPos.Clear();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Brush(eventData.position);
    }



    public void Brush(Vector2 pos)
    {
        FixPosPoint.position = Camera.main.ScreenToWorldPoint(pos);
        var localPos = (FixPosPoint as RectTransform).anchoredPosition;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(m_RectTransform, pos, null, out var localPos);
        if (m_RectTransform.rect.Contains(localPos))
        {

            //那就开始绘制
            var initX = (int)(localPos.x * (width / m_RectTransform.rect.width));
            var initY = (int)(localPos.y * (height / m_RectTransform.rect.height));

            for (int i = initX - PenRadius; i < initX + PenRadius; i++)
            {
                for (int j = initY - PenRadius; j < initY + PenRadius; j++)
                {
                    var currPoint = new Vector2(i - initX, j - initY);
                    if (currPoint.sqrMagnitude < PenRadius * PenRadius)
                    {
                        var index = i + j * width;
                        if (index < setColors.Length && index >= 0)
                        {
                            setColors[index] = ColorValue;
                            pixetPos.Add(index);

                            //if (Mathf.Abs(counter - pixetPos.Count) < 200)
                            {
                               // MakeLampPanel.Instance.ShowContinue();
                            }
                        }

                    }
                }
            }
        }
        m_Texture.SetPixels(setColors);
        m_Texture.Apply();

    }

}
