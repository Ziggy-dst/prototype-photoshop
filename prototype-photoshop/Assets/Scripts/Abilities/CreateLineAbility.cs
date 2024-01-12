using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities
{
    public class CreateLineAbility : AbilityChangeCursor
    {
        private Vector2 origin;
        private LineRenderer _currentLineRenderer;
        
        [Header("Main")]
        [SerializeField] private float startWidth = 0.1f;
        [SerializeField] private float endWidth = 0.1f;
        [SerializeField] private Color startColor = Color.black;
        [SerializeField] private Color endColor = Color.black;
        [SerializeField] private float minDrawLength = 5f;
        [SerializeField] private float hitRange = 0.1f;

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
                // _currentLineRenderer.SetPosition(1, endPosition);
                
                //Effect
                RemoveLine();
                Enemy[] enemyArray = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                foreach (var enemy in enemyArray)
                {
                    if (DistancePointToLine(enemy.transform.position, origin, endPosition) <= hitRange)
                    {
                        enemy.Dead();
                    }
                }
                
                
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
        
        public float DistancePointToLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            Vector2 line = lineEnd - lineStart;
            Vector2 pointToStart = point - lineStart;

            float lineSquareLength = line.sqrMagnitude;
            float dotProduct = Vector2.Dot(pointToStart, line);
            float t = dotProduct / lineSquareLength;

            if (t < 0)
            {
                return Vector2.Distance(point, lineStart);
            }
            else if (t > 1)
            {
                return Vector2.Distance(point, lineEnd);
            }

            Vector2 projection = lineStart + t * line;
            return Vector2.Distance(point, projection);
        }
    }
}

