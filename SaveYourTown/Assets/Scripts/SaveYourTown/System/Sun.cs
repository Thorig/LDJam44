using SaveYourTown.Entity;
using SaveYourTown.Entity.NonPlayerCharacter;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SaveYourTown.System
{
    class Sun :MonoBehaviour
    {
        [SerializeField]
        protected List<GameObject> particles;

        [SerializeField]
        protected List<GameObject> dogs;

        [SerializeField]
        protected List<Vector2> spawnPoints;

        public bool playerIsUnderTheSun = false;
        public float damageTick;
        public float counter;
        public float damage;
        public Player player;
        public Image vitalityBar;
        public Vector3 scale = new Vector3();

        public bool canRest = false;
        public int amountOfBlood = 0;
        public float bloodBonusCounter = 0;

        private void Start()
        {
            respawnAllDogs();
        }

        public void addBloodAmount(int amount)
        {
            amountOfBlood += amount + ((bloodBonusCounter <= 0.0f) ? 0 : (amount));
            bloodBonusCounter = 10.0f;
        }

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
                    showBlood(player.transform.position);
                    canRest = true;
                }
                bloodBonusCounter -= Time.deltaTime; 
            }
        }
        
        public void reset()
        {
            canRest = false;
            respawnAllDogs();
            damageTick = 5;
            counter = 0;
            damage = 1;
            bloodBonusCounter = 0;
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

        private void respawnAllDogs()
        {
            foreach (GameObject dog in dogs)
            {
                respawn(dog);
            }
        }

        public void respawn(GameObject respawnee)
        {
            respawnee.SetActive(false);
            Vector2 pos = spawnPoints[Random.Range(0, spawnPoints.Count-1)];
            pos.x += Random.Range(-1.5f, 1.5f);
            pos.y += Random.Range(-1.5f, 1.5f);
            respawnee.transform.position = pos;
            respawnee.SetActive(true);
            respawnee.GetComponent<Dog>().reset();
        }

        public void showBlood(Vector2 pos)
        {
            foreach(GameObject blood in particles)
            {
                if(!blood.activeInHierarchy)
                {
                    blood.transform.position = pos;
                    blood.SetActive(true);
                    break;
                }
            }
        }
    }
}
