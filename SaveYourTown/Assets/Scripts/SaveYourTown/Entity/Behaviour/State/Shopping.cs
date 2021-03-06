﻿using GameLib.Entity.Animation;
using GameLib.Entity.Behaviour;
using GameLib.Level;
using GameLib.System.Controller;
using SaveYourTown.System;
using UnityEngine;

namespace SaveYourTown.Entity.Behaviour.State
{
    class Shopping : AbstractState
    {
        private static int REST = 0;
        private static int BUY_VIT = 1;
        private static int BUY_PWR = 2;
        private static int EXIT = 3;
        private int cursorState;
        private bool cursorMoved = false;
        private Vector2 tmpPositionCursor;

        public override void animationMessage(int messageId, IEntity entity)
        {
        }

        public override void init(IEntity entity)
        {
            cursorState = REST;
            entity.setMoving(false);
            switchAnimation(AnimationAttributes.ANIMATION_IDLE, entity);
            LayersLookup layersLookup = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LayersLookup>();
            layermask = (1 << layersLookup.giveLayerNumber("Tile"));

            Player player = entity.getTransform().gameObject.GetComponent<Player>();
            player.backGroundShop.SetActive(true);

            GameObject shopCursor = entity.getTransform().gameObject.GetComponent<Player>().shopCursor;
            setCursor(shopCursor, entity.getTransform().position);

            Sun sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Sun>();
            entity.getTransform().gameObject.GetComponent<Player>().notForSale[0].SetActive((!sun.canRest));

            entity.getTransform().gameObject.GetComponent<Player>().notForSale[1].SetActive(!(sun.amountOfBlood >= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraVitality * 2))));
            entity.getTransform().gameObject.GetComponent<Player>().notForSale[2].SetActive(!(sun.amountOfBlood >= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraPower * 2))));
        }

        private void setCursor(GameObject shopCursor, Vector2 positionPlayer)
        {
            tmpPositionCursor = positionPlayer;
            if (cursorState == REST)
            {
                tmpPositionCursor.x += -2;
                tmpPositionCursor.y += 0.7f;
            }
            if (cursorState == BUY_VIT)
            {
                tmpPositionCursor.x += -2;
                tmpPositionCursor.y += 0.2f;
            }
            if (cursorState == BUY_PWR)
            {
                tmpPositionCursor.x += -2;
                tmpPositionCursor.y += -0.3f;
            }
            if (cursorState == EXIT)
            {
                tmpPositionCursor.x += -2;
                tmpPositionCursor.y += -0.8f;
            }
            shopCursor.transform.position = tmpPositionCursor;
        }

        public override void update(IEntity entity)
        {
            KeysPressed keysPressed = entity.getKeysPressed();
            GameObject shopCursor = entity.getTransform().gameObject.GetComponent<Player>().shopCursor;

            Sun sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Sun>();
            entity.getTransform().gameObject.GetComponent<Player>().notForSale[0].SetActive((!sun.canRest));

            if (!keysPressed.down && !keysPressed.up && !keysPressed.actionButtonOne && !keysPressed.attack)
            {
                cursorMoved = false;
                setCursor(shopCursor, entity.getTransform().position);
            }
            if(!cursorMoved && keysPressed.down && cursorState < EXIT)
            {
                ++cursorState;
                cursorMoved = true;
                setCursor(shopCursor, entity.getTransform().position);
            }
            else if (!cursorMoved && keysPressed.up && cursorState > REST)
            {
                --cursorState;
                cursorMoved = true;
            }
            switch (cursorState)
            {
                case 0:
                    if (//keysPressed.actionButtonOne || 
                        keysPressed.attack)
                    {
                        entity.getTransform().gameObject.GetComponent<Player>().resetVitality();
                        sun.reset();
                    }

                    break;
                case 1:
                    if ((sun.amountOfBlood >= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraVitality * 2))) && 
                        (//keysPressed.actionButtonOne || 
                        keysPressed.attack))
                    {
                        sun.amountOfBlood -= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraVitality * 2));
                        entity.getTransform().gameObject.GetComponent<Player>().extraVitality += 1;
                        entity.getTransform().gameObject.GetComponent<Player>().notForSale[1].SetActive(!(sun.amountOfBlood >= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraVitality * 2))));
                        entity.getTransform().gameObject.GetComponent<Player>().notForSale[2].SetActive(!(sun.amountOfBlood >= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraPower * 2))));
                    }
                    break;
                case 2:
                    if ((sun.amountOfBlood >= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraPower * 2))) &&
                        (//keysPressed.actionButtonOne || 
                        keysPressed.attack))
                    {
                        sun.amountOfBlood -= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraPower * 2));
                        entity.getTransform().gameObject.GetComponent<Player>().extraPower += 1;
                        entity.getTransform().gameObject.GetComponent<Player>().notForSale[1].SetActive(!(sun.amountOfBlood >= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraVitality * 2))));
                        entity.getTransform().gameObject.GetComponent<Player>().notForSale[2].SetActive(!(sun.amountOfBlood >= (10 + (entity.getTransform().gameObject.GetComponent<Player>().extraPower * 2))));
                    }

                    break;
                case 3:
                    if (//keysPressed.actionButtonOne || 
                        keysPressed.attack)
                    {
                        IBehaviourStateFactory behaviourStateFactory = entity.getBehaviourStateFactory();
                        entity.setState(((BehaviourStateFactory)behaviourStateFactory).getIdleState(entity));

                        Player player = entity.getTransform().gameObject.GetComponent<Player>();
                        player.backGroundShop.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
