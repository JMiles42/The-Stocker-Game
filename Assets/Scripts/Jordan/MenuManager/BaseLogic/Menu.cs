using ForestOfChaosLib.AdvVar;
using UnityEngine;

namespace ForestOfChaosLib.MenuManaging
{
    public abstract class Menu<T>: Menu
        where T: Menu<T>
    {
        protected static Menu<T> instance;

        protected static T instanceType;

        public static bool instanceNull => instance == null;

        public static Menu<T> Instance
        {
            get
            {
                if(instance)
                    return instance;
                instance = (T)FindObjectOfType(typeof(T));
                if(!instance)
                    Debug.LogWarning(typeof(T) + " is needed in the scene."); //Print error
                return instance;
            }
        }

        public static bool InstanceNull => Instance == null;

        public static bool instanceTypeNull => instanceType == null;

        public static T InstanceType
        {
            get
            {
                if(instanceType)
                    return instanceType;
                instanceType = (T)FindObjectOfType(typeof(T));
                if(!instanceType)
                    Debug.LogWarning(typeof(T) + " is needed in the scene."); //Print error
                return instanceType;
            }
        }

        public static bool InstanceTypeNull => InstanceType == null;

        protected virtual void Awake()
        {
            instance = this;
        }

        protected virtual void OnDestroy()
        {
            instance = null;
        }

        public static void Open()
        {
            if(Instance == null)
                MenuManager.Instance.CreateInstance<T>();

            MenuManager.Instance.OpenMenu(Instance);
        }

        public override void OpenByRef()
        {
            Open();
        }

        public override void CloseByRef()
        {
            Close();
        }

        public virtual void OnEnable()
        {
            Open();
        }

        public virtual void OnDisable()
        { }

        protected static void Close()
        {
            if(Instance == null)
            {
                Debug.LogErrorFormat("Trying to close menu {0} but Instance is null", typeof(T));
                return;
            }

            MenuManager.Instance.CloseMenu(Instance);
        }

        public override void OnBackPressed()
        {
            Close();
        }

        public static void DestroyInstance()
        {
            if(Instance)
            {
                Instance.OnBeforeDestroy();
                Destroy(Instance.gameObject);
            }
            else if(InstanceType)
            {
                InstanceType.OnBeforeDestroy();
                Destroy(InstanceType);
            }
        }

        public virtual void OnBeforeDestroy()
        { }

        public override void Destroy()
        {
            DestroyInstance();
        }
    }

    public abstract class Menu: FoCsBehavior
    {
        [Tooltip("Destroy the Game Object when menu is closed (reduces memory usage)")] public BoolVariable DestroyWhenClosed = false;

        [Tooltip("Disable menus that are under this one in the stack")] public BoolVariable DisableMenusUnderneath = true;

        public static bool HasInstance { get; protected set; }

        public abstract void OnBackPressed();

        public static void MenuManagerGoBack()
        {
            MenuManager.OnBackPressed();
        }

        public void GoBack()
        {
            MenuManager.OnBackPressed();
        }



        public abstract void OpenByRef();
        public abstract void CloseByRef();
        public abstract void Destroy();
    }
}