using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace CustomNamespace
{
    public class DragBoxAbility : AbilityBase
    {
        private Vector2 origin;
        private GameObject selectionBoxInstance;
        private float currentDiagonalLength;
        private LineRenderer rangeArcRenderer;
        private GameObject rangeArcHolder;
        
        public float maxDiagonalLength;
        public float damage; //not necessary
        public GameObject selectionBoxPrefab;

        [Header("Range Arc")]
        public int segments;
        public Material lineMaterial;
        public float lineWidth;
        public int sortingOrder;

        [Header("Feedbacks")]
        public AudioClip soundFX;
        
        
        public override void OnKeyModifierPressed()
        {
            base.OnKeyModifierPressed();
            //TODO: Change Cursor
        }

        public override void OnKeyModifierReleased()
        {
            base.OnKeyModifierReleased();
            //TODO: Change Cursor Back
        }

        public override void OnKeyTriggerPressed()
        {
            base.OnKeyTriggerPressed();
            
            origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectionBoxInstance = Instantiate(selectionBoxPrefab, origin, Quaternion.Euler(Vector3.zero));
            selectionBoxInstance.transform.localScale = Vector2.zero;
            currentDiagonalLength = 0;

            ShowRange();
            // AudioManager.instance.PlaySound(soundFX);
        }

        public override void OnKeyTriggerHolding()
        {
            base.OnKeyTriggerHolding();
            
            Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float boxWidth = endPos.x - origin.x;
            float boxHeight = origin.y - endPos.y;

            currentDiagonalLength = Mathf.Sqrt(boxWidth * boxWidth + boxHeight * boxHeight);
            if (currentDiagonalLength <= maxDiagonalLength)
            {
                selectionBoxInstance.transform.localScale = new Vector2(boxWidth, boxHeight);
            }
            else
            {
                selectionBoxInstance.transform.localScale =
                    new Vector2(boxWidth, boxHeight) * (maxDiagonalLength / currentDiagonalLength);
            }

            FlipRange(boxWidth, boxHeight);
        }

        public override void OnKeyTriggerReleased()
        {
            base.OnKeyTriggerReleased();
            
            Destroy(rangeArcHolder);
            List<Collider2D> selectedEnemies = new List<Collider2D>();
            selectionBoxInstance.GetComponent<BoxCollider2D>().GetContacts(selectedEnemies);
            foreach (var enemy in selectedEnemies)
            {
                enemy.GetComponent<Enemy>().Dead();
            }
            
            Destroy(selectionBoxInstance);
        }

        private void ShowRange()
        {
            rangeArcHolder = new GameObject("Range Arc Holder");
            rangeArcHolder.transform.position = origin;
            
            rangeArcRenderer = rangeArcHolder.AddComponent<LineRenderer>();
            rangeArcRenderer.useWorldSpace = false;
            rangeArcRenderer.startWidth = lineWidth;
            rangeArcRenderer.material = lineMaterial;
            rangeArcRenderer.sortingOrder = sortingOrder;
            rangeArcRenderer.positionCount = segments + 1;
            

            float angle = 95;
            float arcLength = 175 - 95;
            for (int i = 0; i <= segments; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * maxDiagonalLength;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * maxDiagonalLength;
                
                rangeArcRenderer.SetPosition(i, new Vector2(x, y));

                angle += (arcLength / segments);
            }
        }

        private void FlipRange(float boxWidth, float boxHeight)
        {
            float localScaleX = 1;
            float localScaleY = 1;

            localScaleX = boxWidth >= 0 ? 1 : -1;
            localScaleY = boxHeight >= 0 ? 1 : -1;

            rangeArcHolder.transform.localScale = new Vector2(localScaleX, localScaleY);
        }
    }
}
