using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using Managers;
using UnityEngine;

namespace Abilities
{
    public class LineSlashAbility : AbilityChangeCursor
    {
        private Vector2 origin;
        private LineRenderer _currentLineRenderer;
        private LineRenderer _drawingLineRenderer;
        private BoxCollider2D _currentBoxCollider;
        
        [Header("Main")]
        [SerializeField] private float startWidth = 0.1f;
        [SerializeField] private float endWidth = 0.1f;
        [SerializeField] private Color startColor = Color.black;
        [SerializeField] private Color endColor = Color.black;
        [SerializeField] private float minDrawLength = 5f;
        [SerializeField] private int sortingOrder = 100;

        private bool canDrawNewLine = true;

        [Header("Direction")]
        private Vector2 totalDirection;
        private int trackPointCount;
        private Vector2 previousPosition;
        [SerializeField] private float trackTimeInterval = 0.01f;
        private float timer = 0f;
        [SerializeField] private float trackingDistanceThreshold = 1f;

        [Header("Draw Line")]
        [SerializeField] private float drawDuration = 0.1f;
        [SerializeField] private float lineLengthMagnifier = 2;
        
        [Header("Feedbacks")]
        public AudioClip soundFX;

        protected override void OnKeyModifierReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyModifierReleased(abilityName);
            RemoveLine();
        }

        protected override void OnKeyTriggerPressed(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            if (!canDrawNewLine) return;

            origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            previousPosition = origin;

            _currentLineRenderer = new GameObject("Line").AddComponent<LineRenderer>();

            // 设置线的参数
            _currentLineRenderer.startWidth = startWidth;
            _currentLineRenderer.endWidth = endWidth;

            // _currentLineRenderer.numCapVertices = 10;

            // 设置线的颜色
            _currentLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _currentLineRenderer.startColor = startColor;
            _currentLineRenderer.endColor = endColor;
            _currentLineRenderer.sortingOrder = sortingOrder;
        }

        protected override void OnKeyTriggerHolding(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            if (!canDrawNewLine) return;

            timer += Time.deltaTime;

            if (timer >= trackTimeInterval)
            {
                TrackMouse();
                timer = 0f;
            }
        }

        protected override void OnKeyTriggerReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            if (!canDrawNewLine || _currentLineRenderer == null) return;

            Vector2 endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // check if the line is long enough
            if (Vector2.Distance(origin, endPosition) >= minDrawLength)
            {
                Vector2 averageDirection = GetAverageDirection();
                Vector2 calculatedEndPosition = averageDirection + origin;

                _drawingLineRenderer = _currentLineRenderer;
                
                // set collision box
                _currentBoxCollider = new GameObject("Line Collider").AddComponent<BoxCollider2D>();
                _currentBoxCollider.isTrigger = true;
                _currentBoxCollider.transform.parent = _currentLineRenderer.transform;
                _currentBoxCollider.size = new Vector2(Vector3.Distance(origin, calculatedEndPosition),
                    startWidth > endWidth ? startWidth : endWidth);
                _currentBoxCollider.transform.position = (origin + calculatedEndPosition) / 2;
                float angle = Mathf.Atan2(calculatedEndPosition.y - origin.y, calculatedEndPosition.x - origin.x) * Mathf.Rad2Deg;
                _currentBoxCollider.transform.rotation = Quaternion.Euler(0, 0, angle);

                StartCoroutine(DrawLine(origin, calculatedEndPosition));

                // _currentLineRenderer.SetPosition(0, origin);
                // _currentLineRenderer.SetPosition(1, calculatedEndPosition);

                // ResetLine();
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

        IEnumerator DrawLine(Vector2 startPoint, Vector2 endPoint)
        {
            canDrawNewLine = false;

            float startTime = Time.time;
            while (Time.time < startTime + drawDuration)
            {
                float t = (Time.time - startTime) / drawDuration;
                Vector3 currentPoint = Vector3.Lerp(startPoint, endPoint, t);
                _drawingLineRenderer.SetPosition(0, startPoint);
                _drawingLineRenderer.SetPosition(1, currentPoint);
                yield return null;
            }
            _drawingLineRenderer.SetPosition(1, endPoint);

            //Effect
            List<Collider2D> selectedEnemies = new List<Collider2D>();
            _currentBoxCollider.GetContacts(selectedEnemies);
            foreach (var enemy in selectedEnemies)
            {
                enemy.GetComponent<Enemy>().Dead();
            }

            yield return new WaitForSeconds(0.1f);

            RemoveLine();
            ResetLine();

            canDrawNewLine = true;
            
            AudioManager.instance.PlaySound(soundFX);
        }

        private void TrackMouse()
        {
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // if longer than the tracking threshold
            if (Vector2.Distance(currentPosition, previousPosition) >= trackingDistanceThreshold)
            {
                // print("current pos " + currentPosition);
                Vector2 direction = currentPosition - previousPosition;
                totalDirection += direction;
                trackPointCount++;
            }
        }

        private Vector2 GetAverageDirection()
        {
            if (trackPointCount > 0)
                return (totalDirection / trackPointCount) * lineLengthMagnifier;
            return Vector2.zero;
        }

        private void ResetLine()
        {
            _currentLineRenderer = null;
            totalDirection = Vector2.zero;
            trackPointCount = 0;
            timer = 0;
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
