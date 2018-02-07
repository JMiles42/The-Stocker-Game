namespace JMiles42.Systems.MenuManaging
{
    public abstract class SimpleMenuDestroySubMenus<T>: SimpleMenu<T>
        where T: SimpleMenuDestroySubMenus<T>
    {
        public Menu[] CloseOtherMenus;

        public override void OnBeforeDestroy()
        {
            for(var i = 0; i < CloseOtherMenus.Length; i++)
                CloseOtherMenus[i].Destroy();
        }
    }
}