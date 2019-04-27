using System.Collections.Generic;
using UnityEngine;

namespace GameLib.System.Gravity2D
{
    public class RayInformationAttributes : ScriptableObject
    {
        #region attributes and props
        [SerializeField]
        protected Vector2 posRayTopFront;

        public Vector2 PosRayTopFront { get { return posRayTopFront; } set { posRayTopFront = value; } }

        [SerializeField]
        protected Vector2 posRayMiddleFront;

        public Vector2 PosRayMiddleFront { get { return posRayMiddleFront; } set { posRayMiddleFront = value; } }

        [SerializeField]
        protected Vector2 posRayBelowFront;

        public Vector2 PosRayBelowFront { get { return posRayBelowFront; } set { posRayBelowFront = value; } }


        [SerializeField]
        protected Vector2 posRayLeftTop;

        public Vector2 PosRayLeftTop { get { return posRayLeftTop; } set { posRayLeftTop = value; } }

        [SerializeField]
        protected Vector2 posRayMiddleTop;

        public Vector2 PosRayMiddleTop { get { return posRayMiddleTop; } set { posRayMiddleTop = value; } }

        [SerializeField]
        protected Vector2 posRayRightTop;

        public Vector2 PosRayRightTop { get { return posRayRightTop; } set { posRayRightTop = value; } }


        [SerializeField]
        protected Vector2 posRayLeftBelow;

        public Vector2 PosRayLeftBelow { get { return posRayLeftBelow; } set { posRayLeftBelow = value; } }

        [SerializeField]
        protected Vector2 posRayMiddleBelow;

        public Vector2 PosRayMiddleBelow { get { return posRayMiddleBelow; } set { posRayMiddleBelow = value; } }

        [SerializeField]
        protected Vector2 posRayRightBelow;

        public Vector2 PosRayRightBelow { get { return posRayRightBelow; } set { posRayRightBelow = value; } }
              
        [SerializeField]
        protected float minimalSpaceBetweenTileFront;

        public float MinimalSpaceBetweenTileFront { get { return minimalSpaceBetweenTileFront; } set { minimalSpaceBetweenTileFront = value; } }

        [SerializeField]
        protected float minimalSpaceBetweenTileTop;

        public float MinimalSpaceBetweenTileTop { get { return minimalSpaceBetweenTileTop; } set { minimalSpaceBetweenTileTop = value; } }

        [SerializeField]
        protected float minimalSpaceBetweenTileBelow;

        public float MinimalSpaceBetweenTileBelow { get { return minimalSpaceBetweenTileBelow; } set { minimalSpaceBetweenTileBelow = value; } }


        [SerializeField]
        protected bool slopesOnly;

        public bool SlopesOnly { get { return slopesOnly; } set { slopesOnly = value; } }
        
        [SerializeField]
        protected int slopeLayerNumber;

        public int SlopeLayerNumber { get { return slopeLayerNumber; } set { slopeLayerNumber = value; } }

        [SerializeField]
        protected float belowTolerance;

        public float BelowTolerance { get { return belowTolerance; } set { belowTolerance = value; } }
        
        #endregion
    }
}
