using GameLib.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLib.Util
{
    public class RayCalibrater : Player
    {
        public bool doRayCalibration = true;
        public bool doGravityTest = false;
        public float tileHeight = 1.0f;

        public override void Start()
        {
        }

        // Update is called once per frame
        public override void LateUpdate()
        {
            if(doGravityTest)
            {
                base.LateUpdate();
            }

            if (doRayCalibration)
            {
                rayInformation.PosRayTopFront = new Vector2(0.0f, ((spriteRenderer.bounds.size.y / 2.0f) * 0.9f));
                rayInformation.PosRayMiddleFront = new Vector2(0.0f, -spriteRenderer.bounds.size.y * 0.1f);
                rayInformation.PosRayBelowFront = new Vector2(0.0f, -((spriteRenderer.bounds.size.y / 2.0f) * 0.9f));

                rayInformation.PosRayLeftTop = new Vector2(0.0f, (spriteRenderer.bounds.size.x / 2.0f) * 0.9f);
                rayInformation.PosRayMiddleTop = new Vector2(0.0f, (spriteRenderer.bounds.size.x / 2.0f) * 0.0f);
                rayInformation.PosRayRightTop = new Vector2(0.0f, -(spriteRenderer.bounds.size.x / 2.0f) * 0.9f);

                rayInformation.PosRayLeftBelow = new Vector2(0.0f, -(spriteRenderer.bounds.size.x / 2.0f) * 0.9f);
                rayInformation.PosRayMiddleBelow = new Vector2(0.0f, (spriteRenderer.bounds.size.x / 2.0f) * 0.0f);
                rayInformation.PosRayRightBelow = new Vector2(0.0f, (spriteRenderer.bounds.size.x / 2.0f) * 0.9f);

                rayInformation.MinimalSpaceBetweenTileFront = spriteRenderer.bounds.size.x * 0.5f;
                rayInformation.MinimalSpaceBetweenTileTop = spriteRenderer.bounds.size.y * 0.5f;
                rayInformation.MinimalSpaceBetweenTileBelow = spriteRenderer.bounds.size.y * 0.5f;

                rayInformation.BelowTolerance = tileHeight * 1.3f;

                doRayCalibration = false;
                EditorUtility.SetDirty(rayInformation);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}