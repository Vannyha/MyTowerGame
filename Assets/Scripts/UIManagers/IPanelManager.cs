using Context;

namespace UIManagers
{
    public interface IPanelManager : IBean
    {
        void OpenPanel();
        void ClosePanel();
    }
}