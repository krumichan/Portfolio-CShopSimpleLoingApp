namespace LoginApp.ViewModel.Core
{
    /// <summary>
    /// 모든 View Model에서 제공하는 인터페이스
    /// </summary>
    public interface IPageViewModel
    {
        void LoadPageData(object _data);
    }
}
