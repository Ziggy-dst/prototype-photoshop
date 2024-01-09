using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class CreateLineAbility : AbilityBase
    {
        private Vector2 origin;

        private LineRenderer _lineRenderer;

        [SerializeField] private float startWidth = 0.1f;
        [SerializeField] private float endWidth = 0.1f;
        [SerializeField] private Color startColor = Color.green;
        [SerializeField] private Color endColor = Color.red;



        public override void OnKeyPressed()
        {
            print("press 2");
            // origin = Input.mousePosition;
            // _lineRenderer = Instantiate(new LineRenderer());
            // _lineRenderer.gameObject.AddComponent<LineRenderer>();
            //
            // // 设置线的参数
            // _lineRenderer.startWidth = startWidth;
            // _lineRenderer.endWidth = endWidth;
            //
            // // 设置线的颜色
            // _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            // _lineRenderer.startColor = startColor;
            // _lineRenderer.endColor = endColor;
            //
            // _lineRenderer.SetPosition(0, origin);

            // 定义线的起点和终点
            // Vector3 start = new Vector3(0, 0, 0);
            // Vector3 end = new Vector3(1, 1, 0);
        }

        public override void OnKeyHolding()
        {
            print("holding 2");
            // 设置线段的位置
            // _lineRenderer.SetPosition(1, Input.mousePosition);
        }

        public override void OnKeyReleased()
        {
            print("release 2");
            // _lineRenderer.SetPosition(1, Input.mousePosition);
        }

        private void CreateNewLine()
        {

        }
    }
}

