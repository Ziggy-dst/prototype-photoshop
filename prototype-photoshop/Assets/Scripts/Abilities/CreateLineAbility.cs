using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities
{
    public class CreateLineAbility : AbilityBase
    {
        private Vector2 origin;
        private LineRenderer _currentLineRenderer;

        [SerializeField] private float startWidth = 0.1f;
        [SerializeField] private float endWidth = 0.1f;
        [SerializeField] private Color startColor = Color.black;
        [SerializeField] private Color endColor = Color.black;
        [SerializeField] private float minDrawLength = 5f;

        protected override void OnKeyModifierReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            RemoveLine();
        }

        protected override void OnKeyTriggerPressed(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _currentLineRenderer = new GameObject("Line").AddComponent<LineRenderer>();

            // 设置线的参数
            _currentLineRenderer.startWidth = startWidth;
            _currentLineRenderer.endWidth = endWidth;

            _currentLineRenderer.numCapVertices = 10;

            // 设置线的颜色
            _currentLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _currentLineRenderer.startColor = startColor;
            _currentLineRenderer.endColor = endColor;

            _currentLineRenderer.SetPosition(0, origin);
        }

        protected override void OnKeyTriggerHolding(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            // 设置线段的位置
            _currentLineRenderer.SetPosition(1, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        protected override void OnKeyTriggerReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            Vector2 endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // check if the line is long enough
            if (Vector2.Distance(origin, endPosition) >= minDrawLength)
            {
                _currentLineRenderer.SetPosition(1, endPosition);
                _currentLineRenderer = null;
            }
            else
            {
                RemoveLine();
            }
        }

        private void RemoveLine()
        {
            if (_currentLineRenderer != null)
            {
                Destroy(_currentLineRenderer.gameObject);
                _currentLineRenderer = null;
            }
        }
    }
}

