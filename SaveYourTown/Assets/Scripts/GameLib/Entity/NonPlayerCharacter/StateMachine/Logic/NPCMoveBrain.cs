using GameLib.Entity.Animation;
using GameLib.Level;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using UnityEngine;

namespace GameLib.Entity.NonPlayerCharacter.StateMachine.Logic
{
    public class NPCMoveBrain : IBrain
    {
        protected int state = 0;
        protected int layermask;

        public override void init(AICharacter character)
        {
            LayersLookup layersLookup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LayersLookup>();
            layermask = 1 << layersLookup.giveLayerNumber("Tile");
        }

        public override void update(AICharacter character)
        {
            IGravityClient gravityClient = character.getEntity().getGravityClient();
            KeysPressed keysPressed = character.getEntity().getKeysPressed();
            RayInformation rayInformation = character.getEntity().getGravityClient().getRayInformation();
            RayHitboxes rayHitboxes = gravityClient.getRayHitboxes();

            gravityClient.getRayInformation().checkRaysFront(gravityClient, 0.0f, character.getEntity().getTransform().eulerAngles.z + 0.0f, layermask, false);
            gravityClient.getRayInformation().checkRaysBelow(gravityClient, 0.0f, character.getEntity().getTransform().eulerAngles.z + 270.0f, layermask, false);

            float minimalSpaceBetweenTileBelow = rayInformation.MinimalSpaceBetweenTileBelow;
            float topFrontDistance = (rayHitboxes.HitTopFront.distance * 0.9f);
            bool turned = false;

            gravityClient.getRayInformation().checkRaysFront(gravityClient, 0.0f, character.getEntity().getTransform().eulerAngles.z + 0.0f, layermask, true);
        
            keysPressed = turnIfWallIsNear(gravityClient, topFrontDistance, 
                rayInformation.MinimalSpaceBetweenTileFront, keysPressed, out turned);

            if (!turned && (rayHitboxes.HitMiddleBelow.collider.tag.CompareTo("Slope") != 0))
            {
                keysPressed = turnIfOnAnEdge(keysPressed, minimalSpaceBetweenTileBelow, character);
            }
            character.setKeysPressed(keysPressed);
        }

        protected virtual KeysPressed turnIfWallIsNear(IGravityClient gravityClient, float topFrontDistance,
            float minimalSpaceBetweenTileFront, KeysPressed keysPressed, out bool turned)
        {
            turned = false;

            if (!gravityClient.isFlipped() &&
                topFrontDistance <= minimalSpaceBetweenTileFront)
            {
                turned = true;
                keysPressed.left = true;
                keysPressed.right = false;
            }
            else if (!gravityClient.isFlipped() &&
                topFrontDistance > minimalSpaceBetweenTileFront)
            {
                keysPressed.left = false;
                keysPressed.right = true;
            }

            if (gravityClient.isFlipped() &&
                topFrontDistance <= minimalSpaceBetweenTileFront)
            {
                turned = true;
                keysPressed.left = false;
                keysPressed.right = true;
            }
            else if (gravityClient.isFlipped() &&
                topFrontDistance > minimalSpaceBetweenTileFront)
            {
                keysPressed.left = true;
                keysPressed.right = false;
            }

            return keysPressed;
        }

        private KeysPressed turnIfOnAnEdge(KeysPressed keysPressed, 
            float minimalSpaceBetweenTileBelow, AICharacter character)
        {
            IGravityClient gravityClient = character.getEntity().getGravityClient();
            RayHitboxes rayHitboxes = gravityClient.getRayHitboxes();
            
            if (gravityClient.isFlipped() && rayHitboxes.HitLeftBelow.distance > 1.0f)
            {
                keysPressed.left = false;
                keysPressed.right = true;
            }
            else if (gravityClient.isFlipped() &&
                rayHitboxes.HitLeftBelow.distance <= minimalSpaceBetweenTileBelow)
            {
                keysPressed.left = true;
                keysPressed.right = false;
            }

            if (!gravityClient.isFlipped() &&
                rayHitboxes.HitRightBelow.distance > 1.0f)
            {
                keysPressed.left = true;
                keysPressed.right = false;
            }
            else if (!gravityClient.isFlipped() &&
                rayHitboxes.HitRightBelow.distance <= minimalSpaceBetweenTileBelow)
            {
                keysPressed.left = false;
                keysPressed.right = true;
            }

            return keysPressed;
        }
    }
}