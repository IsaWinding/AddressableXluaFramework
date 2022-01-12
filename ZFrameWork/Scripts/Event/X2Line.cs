using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redmoon.Animator{
    public class X2LineVec3 {
        public Vector3 startPos;
        public Vector3 endPos;
        public float height;

        private X2Line x2Line;
        private float length;
        public void Init() {
            length = (endPos - startPos).sqrMagnitude;
            x2Line = new X2Line();
            x2Line.InitByTowPosAndMidHight(Vector2.zero, new Vector2(length,0), height);
        }
        public Vector3 GetPos(float pProgress) {
            var pos = startPos + (endPos - startPos) * pProgress;
            pos.y = x2Line.GetY(pProgress* length);
            return pos;
        }
    }
    public class X2Line
    {
        private float a;
        private float b;
        private float c;

        private const float GravityEffect = 9.8f;

        public void InitPhysical(Vector2 pStartPos, float pSpeedX, float pSpeedY)
        {
            float maxHightTime = pSpeedY / GravityEffect;
            float maxHightDistance = pSpeedY * maxHightTime / 2;
            Vector2 sameahightEndPos = new Vector2(pStartPos.x + pSpeedX * maxHightTime * 2, pStartPos.y);
            InitByTowPosAndMidHight(pStartPos, sameahightEndPos, maxHightDistance);
        }

        public void InitByTowPosAndMidHight(Vector2 pPos1, Vector2 pPos2, float pHeight)
        {
            InitByThreePos(pPos1, pPos2, new Vector2((pPos1.x + pPos2.x) / 2, pPos1.y + pHeight));
        }

        public void InitByThreePos(Vector2 pPos1, Vector2 pPos2, Vector2 pPos3)
        {
            float effect1 = pPos1.y / ((pPos1.x - pPos2.x) * (pPos1.x - pPos3.x));
            float effect2 = pPos2.y / ((pPos2.x - pPos1.x) * (pPos2.x - pPos3.x));
            float effect3 = pPos3.y / ((pPos3.x - pPos1.x) * (pPos3.x - pPos2.x));

            a = effect1 + effect2 + effect3;
            b = -(pPos1.x + pPos2.x) * effect3 - (pPos1.x + pPos3.x) * effect2 - (pPos2.x + pPos3.x) * effect1;
            c = (pPos1.x * pPos2.x) * effect3 + (pPos1.x * pPos3.x) * effect2 + (pPos2.x * pPos3.x) * effect1;
        }

        public float GetY(float pX)
        {
            return a * pX * pX + b * pX + c;
        }
    }
}

