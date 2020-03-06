using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollider : MonoBehaviour
{
    [Serializable]
    public struct Horizontal
    {
        public bool horiz;
        public bool right;
        public float bias;
    }

    [Serializable]
    public struct Vertical
    {
        public bool vert;
        public bool above;
        public float bias;
    }

    [Serializable]
    public struct Jump
    {
        public bool noJump;
        public bool wallJump;
        public int wallJumpDirection;
    }

    [Serializable]
    public struct Move
    {
        public bool move;
        public bool moveRight;
    }

    public Horizontal horizontal;
    public Vertical vertical;
    public bool either;
    public Jump jump;
    public Move move;
}
