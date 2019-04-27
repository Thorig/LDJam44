using GameLib.System.GUI;
using UnityEngine;

namespace GameLib.System.Camera
{
    public class CamToTexture : MonoBehaviour
    {
        public static int STATE_RENDERING_TO_SCREEN = 0;
        public static int STATE_RENDERING_TO_TEXTURE = 1;

        [SerializeField]
        protected bool doRenderToTexture = false;

        [SerializeField]
        protected int stateOfRendering = 0;

        [SerializeField]
        protected RenderTexture renderTexture;

        [SerializeField]
        protected Texture2D myTexture2D;

        [SerializeField]
        protected UnityEngine.Camera cam;

        [SerializeField]
        protected IUIManager uiManager;

        public void OnEnable()
        {
            // register the callback when enabling object
            UnityEngine.Camera.onPostRender += MyPostRender;
        }

        public void OnDisable()
        {
            // remove the callback when disabling object
            UnityEngine.Camera.onPostRender -= MyPostRender;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (doRenderToTexture)
            {
                setRenderToTexture();
            }
        }

        private void setRenderToTexture()
        {
            cam.targetTexture = renderTexture;
            stateOfRendering = STATE_RENDERING_TO_TEXTURE;
            doRenderToTexture = false;
        }

        public void MyPostRender(UnityEngine.Camera cam)
        {
            if (stateOfRendering == STATE_RENDERING_TO_TEXTURE)
            {
                renderToTexture();
            }
        }

        private void renderToTexture()
        {
            stateOfRendering = STATE_RENDERING_TO_SCREEN;

            myTexture2D.ReadPixels(new Rect(0, 0, renderTexture.width,
                renderTexture.height), 0, 0);

            myTexture2D.Apply();

            cam.targetTexture = null;

            uiManager.doScreenTransition();
        }
    }
}