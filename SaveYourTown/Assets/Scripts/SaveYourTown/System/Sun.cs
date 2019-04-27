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
                    scale.x = (player.currentVitality) / player.totalVitality;
                    scale.y = 1;
                    scale.z = 1;
                    vitalityBar.transform.localScale = scale;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag.CompareTo("Player") == 0)
            {
                playerIsUnderTheSun = false;
                counter = 0.0f;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag.CompareTo("Player") == 0)
            {
                playerIsUnderTheSun = true;
                counter = 0.0f;
            }
        }
    }
}
