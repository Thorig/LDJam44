using SaveYourTown.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace SaveYourTown.System
{
    class Sun :MonoBehaviour
    {
        public bool playerIsUnderTheSun = false;
        public float damageTick;
        public float counter;
        public float damage;
        public Player player;
        public Image vitalityBar;
        public Vector3 scale = new Vector3();

        private void Update()
        {
            if(playerIsUnderTheSun)
            {
                counter += Time.deltaTime;
                if(counter >= damageTick)
                {
                    counter = 0.0f;
                    player.currentVitality -= damage;
                    damage *= 1.02f;
                    damageTick -= 0.05f;
                    scale.x = (player.currentVitality) / player.totalVitality;
                    scale.y = 1;
                    scale.z = 1;
                    vitalityBar.transform.localScale = scale;
                }
            }
        }

        public void reset()
        {
            damageTick = 5;
            counter = 0;
            damage = 1;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag.CompareTo("Player") == 0)
            {
                playerIsUnderTheSun = false;
                counter = 0;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag.CompareTo("Player") == 0)
            {
                playerIsUnderTheSun = true;
                counter = 0;
            }
        }

        public void respawn(GameObject respawnee)
        {
            respawnee.SetActive(false);
            respawnee.SetActive(true);
        }
    }
}
