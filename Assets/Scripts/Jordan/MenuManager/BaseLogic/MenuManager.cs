using System.Collections.Generic;
using System.Reflection;
using JMiles42.Generics;
using UnityEngine;

namespace JMiles42.Systems.MenuManaging
{
    public partial class MenuManager: Singleton<MenuManager>
    {
        public Stack<Menu> menuStack = new Stack<Menu>();

        public static Menu TopMenu
        {
            get
            {
                if(Instance.menuStack.Count == 0)
                    return null;

                return Instance.menuStack.Peek();
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(Transform);
            var others = FindObjectsOfType<MenuManager>();
            foreach(var other in others)
            {
                if(other != Instance)
                    Destroy(other.gameObject);
            }
        }

        private void Reset()
        {
            FindAndFillPrefabs();
        }

        private void FindAndFillPrefabs()
        {
            var fields = GetFields();
            foreach(var field in fields)
            {
                if(field.FieldType.IsGenericType)
                    continue;
                var objs = Resources.FindObjectsOfTypeAll(field.FieldType);
                if(objs.Length > 0)
                    field.SetValue(this, objs[0]);
            }
        }

        private FieldInfo[] GetFields() => GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        public void CreateInstance<T>()
            where T: Menu
        {
            var prefab = GetPrefab<T>();

            Instantiate(prefab, transform);
        }

        public void OpenMenu(Menu menuInstance)
        {
            // De-activate top menu
            if(menuStack.Count > 0)
            {
                if(menuInstance.DisableMenusUnderneath)
                {
                    foreach(var menu in menuStack)
                    {
                        if(menu != menuInstance)
                            menu.gameObject.SetActive(false);

                        if(menu.DisableMenusUnderneath)
                            break;
                    }
                }

                var topCanvas = menuInstance.GetComponent<Canvas>();
                var previousCanvas = menuStack.Peek().GetComponent<Canvas>();
                topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;
            }
            menuInstance.gameObject.SetActive(true);

            if((menuStack.Count == 0) || (menuStack.Peek() != menuInstance))
                menuStack.Push(menuInstance);
        }

        private T GetPrefab<T>()
            where T: Menu
        {
            // Get prefab dynamically, based on public fields set from Unity
            // You can use private fields with SerializeField attribute too
            var fields = GetFields();
            foreach(var field in fields)
            {
                var prefab = field.GetValue(this) as T;
                if(prefab != null)
                    return prefab;
            }

            throw new MissingReferenceException("Prefab not found for type " + typeof(T));
        }

        public void CloseMenu(Menu menu)
        {
            if(menuStack.Count == 0)
            {
                Debug.LogErrorFormat(menu, "{0} cannot be closed because menu stack is empty", menu.GetType());
                return;
            }

            if(menuStack.Peek() != menu)
            {
                Debug.LogErrorFormat(menu, "{0} cannot be closed because it is not on top of stack", menu.GetType());
                return;
            }

            CloseTopMenu();
        }

        public void CloseTopMenu()
        {
            var menuInstance = menuStack.Pop();

            if(menuInstance.DestroyWhenClosed)
                Destroy(menuInstance.gameObject);
            else
                menuInstance.gameObject.SetActive(false);

            // Re-activate top menu
            // If a re-activated menu is an overlay we need to activate the menu under it
            foreach(var menu in menuStack)
            {
                if(menu != menuInstance)
                    menu.gameObject.SetActive(true);

                if(menu.DisableMenusUnderneath)
                    break;
            }
        }

        private void Update()
        {
            // On Android the back button is sent as Esc
            if(Input.GetKeyDown(KeyCode.Escape))
                OnBackPressed();
        }

        public static void OnBackPressed()
        {
            if(Instance.menuStack.Count > 0)
                Instance.menuStack.Peek().OnBackPressed();
        }
    }
}