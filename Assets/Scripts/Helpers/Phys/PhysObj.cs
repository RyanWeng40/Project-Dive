﻿using System;
using System.Collections.Generic;
using DefaultNamespace;
using Mechanics;
using UnityEngine;

namespace Phys {
    [RequireComponent(typeof(Collider2D))]
    public abstract class PhysObj : MonoBehaviour {
        protected BoxCollider2D myCollider { get; private set; }
        protected Vector2 velocity = Vector2.zero;

        public Vector2 nextFrameOffset = Vector2.zero;

        protected float velocityY {
            get { return velocity.y; }
            set { velocity = new Vector2(velocity.x, value); }
        }
        
        protected float velocityX {
            get { return velocity.x; }
            set { velocity = new Vector2(value, velocity.y); }
        }

        protected void Start() {
            myCollider = GetComponent<BoxCollider2D>();
        }

        public void FixedUpdate() {
            nextFrameOffset = Vector2.zero;
            Move(velocity * Game.FixedDeltaTime);
        }

        /// <summary>
        /// Checks the interactable layer for any collisions. Will call OnCollide if it hits anything.
        /// </summary>
        /// <param name="direction"><b>MUST</b> be a cardinal direction with a <b>magnitude of one.</b></param>
        /// <param name="onCollide"></param>
        /// <returns></returns>
        public bool CheckCollisions(Vector2 direction, Func<PhysObj, Vector2, bool> onCollide) {
            Vector2 colliderSize = myCollider.size;
            Vector2 sizeMult = colliderSize*0.97f;
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = LayerMask.GetMask("Interactable");
            filter.useLayerMask = true;
            Physics2D.BoxCast(transform.position, sizeMult, 0, direction, filter, hits, 8f);
            foreach (var hit in hits) {
                var p = hit.transform.GetComponent<PhysObj>();

                bool proactiveCollision = ProactiveBoxCast(p.transform, p.nextFrameOffset, sizeMult, direction, filter);
                if (proactiveCollision) {
                    if (onCollide.Invoke(p, direction)){
                        return true;
                    }
                }
            }

            return false;
        }

        public bool ProactiveBoxCast(Transform checkAgainst, Vector3 nextFrameOffset, Vector2 sizeMult, Vector2 direction, ContactFilter2D filter) {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            int numHits = Physics2D.BoxCast(
                transform.position - nextFrameOffset,
                sizeMult, 0, direction, filter, hits, 0.3f);
            foreach (var hit in hits) {
                if (hit.transform == checkAgainst) {
                    return true;
                }
            }
            return false;
        }

        public void Move(Vector2 vel) {
            int moveX = (int) Math.Abs(vel.x);
            if (moveX != 0) {
                Vector2 xDir = new Vector2(vel.x / moveX, 0);
                MoveGeneral(xDir, moveX, OnCollide);
            }

            int moveY = (int) Math.Abs(vel.y);
            if (moveY != 0) {
                Vector2 yDir = new Vector2(0, vel.y / moveY);
                MoveGeneral(yDir, moveY, OnCollide);
            }
        }
        public abstract bool MoveGeneral(Vector2 direction, int magnitude, Func<PhysObj, Vector2, bool> OnCollide);

        public abstract bool OnCollide(PhysObj p, Vector2 direction);

        public abstract bool PlayerCollide(PlayerController p, Vector2 direction);

        public virtual bool IsGround(PhysObj whosAsking) {
            return true;
        }

        public static Actor[] GetActors() {
            return FindObjectsOfType<Actor>();
        }

        public abstract bool Squish(PhysObj p, Vector2 d);
    }
}