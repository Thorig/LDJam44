using UnityEngine;

namespace SaveYourTown.Entity
{
    class StoreNPC : MonoBehaviour
    {
        public GameObject welcomeMessage;
        public GameObject helpMessage;
        private float counter = 0;
        public bool playerIsNear = false;
        private bool messagesSwitched = false;

        public void Update()
        {
            if (playerIsNear)
            {
                counter += Time.deltaTime;
                if(counter >= 1.20f && !messagesSwitched)
                {
                    messagesSwitched = true;
                    welcomeMessage.SetActive(false);
                    helpMessage.SetActive(true);
                }
                if (counter >= 2.4f && messagesSwitched)
                {
                    messagesSwitched = true;
                    welcomeMessage.SetActive(false);
                    helpMessage.SetActive(false);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.CompareTo("Player") == 0)
            {
                counter = 0;
                messagesSwitched = false;
                welcomeMessage.SetActive(true);
                helpMessage.SetActive(false);

                playerIsNear = true;
                Player player = collision.gameObject.GetComponent<Player>();
                player.IsNearShop = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag.CompareTo("Player") == 0)
            {
                counter = 0;
                messagesSwitched = false;
                welcomeMessage.SetActive(false);
                helpMessage.SetActive(false);

                playerIsNear = false;
                Player player = collision.gameObject.GetComponent<Player>();
                player.IsNearShop = false;
            }
        }

    }
}
