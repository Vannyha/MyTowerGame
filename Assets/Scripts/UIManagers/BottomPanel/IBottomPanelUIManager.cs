using Context;

namespace UIManagers.BottomPanel
{
    public interface IBottomPanelUIManager : IPanelManager, IBean
    {
        public void OpenMainPreset();
    }
}