using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomNamespace
{
    public class LineEnemy : MonoBehaviour
    {
        public float lifeTime;
        public float moveSpeed;

        private Rigidbody2D[] rb2DArray;
        private Vector2 direction;
        void Start()
        {
            Vector3 targetPos = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            direction = (targetPos - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Invoke("SelfDestroy", lifeTime);
        }
        
        void Update()
        {
            rb2DArray = GetComponentsInChildren<Rigidbody2D>();
            foreach (var rb2D in rb2DArray)
            {
                rb2D.velocity = direction * moveSpeed;
            }
            
            if(transform.childCount == 0) Destroy(gameObject);
        }

        void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}