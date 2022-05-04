using Context;

namespace UIManagers.BottomPanel
{
    public interface IBottomPanelUIManager : IPanelManager, IInitResolve
    {
        public void OpenMainPreset();
    }
}