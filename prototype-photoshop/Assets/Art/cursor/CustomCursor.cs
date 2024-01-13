using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public SpriteRenderer cursorSprite; // 将你的Sprite拖拽到这个变量上

    void Start()
    {
        // 隐藏默认的鼠标光标
        Cursor.visible = false;
    }

    void Update()
    {
        // 获取鼠标在世界空间中的位置
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 更新Sprite的位置，使其中心点跟随鼠标
        cursorSprite.transform.position = cursorPosition;
    }
}