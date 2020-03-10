using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class InputManager
    {
        // Data: ----------------------------------------------
        private static InputManager pInstance = null;
        private static InputManager pActiveMan = null;

        private bool pSpaceKeyPrev;
        private bool pCKeyPrev;

        private InputSubject pSubjectArrowRight;
        private InputSubject pSubjectArrowLeft;
        private InputSubject pSubjectSpace;
        private InputSubject pSubjectCKey;
        private InputSubject pSubjectPKey;

        public InputManager()
        {
            this.pSubjectArrowLeft = new InputSubject();
            this.pSubjectArrowRight = new InputSubject();
            this.pSubjectSpace = new InputSubject();
            this.pSubjectCKey = new InputSubject();
            this.pSubjectPKey = new InputSubject();

            this.pSpaceKeyPrev = false;
            this.pCKeyPrev = false;

            
        }

        public static void Create() {
            if (pInstance == null)
            {
                pInstance = new InputManager();
            }
            Debug.Assert(pInstance != null);

        }

        public static void SetActive(InputManager pIMan)
        {
            InputManager pMan = InputManager.GetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pIMan != null);
            InputManager.pActiveMan = pIMan;
        }

        private static InputManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static InputSubject GetArrowRightSubject()
        {
            InputManager pMan = InputManager.pActiveMan;
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowRight;
        }

        public static InputSubject GetArrowLeftSubject()
        {
            InputManager pMan = InputManager.pActiveMan;
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowLeft;
        }

        public static InputSubject GetSpaceSubject()
        {
            InputManager pMan = InputManager.pActiveMan;
            Debug.Assert(pMan != null);

            return pMan.pSubjectSpace;
        }
        public static InputSubject GetCKeySubject()
        {
            InputManager pMan = InputManager.pActiveMan;
            Debug.Assert(pMan != null);

            return pMan.pSubjectCKey;
        }

        public static InputSubject GetPKeySubject()
        {
            InputManager pMan = InputManager.pActiveMan;
            Debug.Assert(pMan != null);

            return pMan.pSubjectPKey;
        }

        public static void Update()
        {
            InputManager pMan = InputManager.pActiveMan;
            Debug.Assert(pMan != null);

            // LeftKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                pMan.pSubjectArrowLeft.Notify();
            }

            // RightKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                pMan.pSubjectArrowRight.Notify();
            }

            // P Key: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_P) == true)
            {
                pMan.pSubjectPKey.Notify();
            }

            // SpaceKey: (with key history) -----------------------------------------------------------
            bool spaceKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);
            if (spaceKeyCurr == true && pMan.pSpaceKeyPrev == false)
            {
                pMan.pSubjectSpace.Notify();
            }

            pMan.pSpaceKeyPrev = spaceKeyCurr;

            // C Key: (with key history) -----------------------------------------------------------
            bool CKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_C);
            if (CKeyCurr == true && pMan.pCKeyPrev == false)
            {
                pMan.pSubjectCKey.Notify();
                
            }

            pMan.pCKeyPrev = CKeyCurr;

        }
    }
}
