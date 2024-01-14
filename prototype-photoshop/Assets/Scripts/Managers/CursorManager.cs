using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class CursorManager : MonoBehaviour
    {
        private SpriteRenderer cursorRenderer;
        public static CursorManager instance;
        public Sprite defaultCursor;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            cursorRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Cursor.visible = false;
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorRenderer.transform.position = cursorPosition;
        }

        public void ChangeCursor(Sprite cursorSprite)
        {
            cursorRenderer.sprite = cursorSprite;
        }

        public void HideCursor()
        {
            cursorRenderer.enabled = false;
        }

        public void ShowCursor()
        {
            cursorRenderer.enabled = true;
        }

        public void ResumeCursor()
        {
            cursorRenderer.sprite = defaultCursor;
        }
    }
}
