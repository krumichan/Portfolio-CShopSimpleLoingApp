using System.Windows;
using System.Windows.Input;

namespace LoginApp.ViewModel
{
    class LobbyViewModel : Core.BaseViewModel, Core.IPageViewModel
    {
        private ICommand logoutCommand_;
        private ICommand ModifyAccountCommand_;
        private ICommand deleteAccountCommend_;

        private readonly Service.MemberService memberService_;

        private Model.Member currentUser;

        public LobbyViewModel()
        {
            memberService_ = new Service.MemberService();
        }

        public void LoadPageData(object _data)
        {
            currentUser = _data as Model.Member;
        }

        // 로그아웃 명령
        public ICommand LogoutCommand
        {
            get => logoutCommand_ ?? (logoutCommand_ = new Command.ReplyCommand(Logout));
        }

        // 회원삭제 명령
        public ICommand DeleteAccountCommand
        {
            get => deleteAccountCommend_ ?? (deleteAccountCommend_ = new Command.ReplyCommand(DeleteAccount));
        }

        // 회원수정화면 호출 명령
        public ICommand ModifyAccountCommand
        {
            get => ModifyAccountCommand_ ?? (ModifyAccountCommand_ = new Command.ReplyCommand(ModifyAccount));
        }

        public string CurrentUserName
        {
            get => currentUser.name;
        }

        /// <summary>
        /// 로그아웃 이벤트
        /// </summary>
        private void Logout()
        {
            currentUser = null;
            Core.CallbackMediator.Notify("ToLogin");
        }

        /// <summary>
        /// 회원삭제 이벤트
        /// </summary>
        private void DeleteAccount()
        {
            MessageBoxResult result = MessageBox.Show("Delete your account now?", "Checking", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                bool deleted = memberService_.Delete(currentUser);
                ViewModel.Core.CallbackMediator.Notify("ToLogin", null);
            }
        }

        /// <summary>
        /// 회원수정 이벤트
        /// </summary>
        private void ModifyAccount()
        {
            Core.CallbackMediator.Notify("ToModifyAccount", currentUser);
        }
    }
}
