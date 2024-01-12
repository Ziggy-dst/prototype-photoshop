using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomNamespace
{
    public class LineEnemy : Enemy
    {
        public float lifeTime;
        public float moveSpeed;

        private Rigidbody2D[] rb2DArray;
        private Vector2 direction;
        void Start()
        {
            rb2DArray = GetComponentsInChildren<Rigidbody2D>();
            Vector3 targetPos = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            direction = (targetPos - transform.position).normalized;

            Invoke("SelfDestroy", lifeTime);
        }
        
        void Update()
        {
            foreach (var rb2D in rb2DArray)
            {
                rb2D.velocity = direction * moveSpeed;
            }
        }

        void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}
