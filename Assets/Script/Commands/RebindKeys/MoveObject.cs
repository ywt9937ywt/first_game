using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveObject : MonoBehaviour
    {
        private const float MOVE_SPEED = 1f;

        public void Move(Vector3 dir)
        {
            transform.Translate(dir * MOVE_SPEED);
        }
    }
}

