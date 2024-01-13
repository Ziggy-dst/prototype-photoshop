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
        private BoxCollider2D _currentBoxCollider;
        
        [Header("Main")]
        [SerializeField] private float startWidth = 0.1f;
        [SerializeField] private float endWidth = 0.1f;
        [SerializeField] private Color startColor = Color.black;
        [SerializeField] private Color endColor = Color.black;
        [SerializeField] private float minDrawLength = 5f;
        

        protected override void OnKeyModifierReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyModifierReleased(abilityName);
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
            
            // 生成碰撞盒
            _currentBoxCollider = new GameObject("Line Collider").AddComponent<BoxCollider2D>();
            _currentBoxCollider.isTrigger = true;
            _currentBoxCollider.transform.parent = _currentLineRenderer.transform;
        }

        protected override void OnKeyTriggerHolding(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 设置线段的位置
            _currentLineRenderer.SetPosition(1, endPoint);
            
            //设置碰撞盒的位置
            _currentBoxCollider.size = new Vector2(Vector3.Distance(origin, endPoint),
                startWidth > endWidth ? startWidth : endWidth);
            _currentBoxCollider.transform.position = (origin + endPoint) / 2;
            float angle = Mathf.Atan2(endPoint.y - origin.y, endPoint.x - origin.x) * Mathf.Rad2Deg;
            _currentBoxCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
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
                List<Collider2D> selectedEnemies = new List<Collider2D>();
                _currentBoxCollider.GetContacts(selectedEnemies);
                foreach (var enemy in selectedEnemies)
                {
                    enemy.GetComponent<Enemy>().Dead();
                }
                
                RemoveLine();
                _currentLineRenderer = null;
                _currentBoxCollider = null;
            }
            else
            {
                RemoveLine();
            }
        }

        protected override void OnKeyModifierSwitched(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            RemoveLine();
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

