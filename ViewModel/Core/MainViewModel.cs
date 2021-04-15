namespace LoginApp.ViewModel.Core
{
    /// <summary>
    /// View 전환을 위한 Main View Model 클래스
    /// </summary>
    class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            lobbyViewModel_ = new LobbyViewModel();
            loginViewModel_ = new LoginViewModel();
            createAccountModel_ = new CreateAccountViewModel();
            modifyAccountViewModel_ = new ModifyAccountViewModel();

            CallbackMediator.Subscribe("ToLobby", ToLobby);
            CallbackMediator.Subscribe("ToLogin", ToLogin);
            CallbackMediator.Subscribe("ToCreateAccount", ToCreateAccount);
            CallbackMediator.Subscribe("ToModifyAccount", ToModifyAccount);

            CurrentViewModel = loginViewModel_;
        }

        // 현재의 View 정보
        private object currentViewModel_;
        public object CurrentViewModel
        {
            get => currentViewModel_;
            set
            {
                currentViewModel_ = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        // 모든 View( 및 View Model ) 정보
        private readonly IPageViewModel lobbyViewModel_;
        private readonly IPageViewModel loginViewModel_;
        private readonly IPageViewModel createAccountModel_;
        private readonly IPageViewModel modifyAccountViewModel_;

        /// <summary>
        /// Lobby 화면으로 이동
        /// </summary>
        /// <param name="args">Lobby 화면에 전해줄 인수</param>
        private void ToLobby(object args)
        {
            lobbyViewModel_.LoadPageData(args);
            CurrentViewModel = lobbyViewModel_;
        }

        /// <summary>
        /// Login 화면으로 이동
        /// </summary>
        /// <param name="args">Login 화면에 전해줄 인수</param>
        private void ToLogin(object args)
        {
            loginViewModel_.LoadPageData(args);
            CurrentViewModel = loginViewModel_;
        }

        /// <summary>
        /// Create Account 화면으로 이동
        /// </summary>
        /// <param name="args">Create Account 화면에 전해줄 인수</param>
        private void ToCreateAccount(object args)
        {
            createAccountModel_.LoadPageData(args);
            CurrentViewModel = createAccountModel_;
        }

        /// <summary>
        /// Modify Account 화면으로 이동
        /// </summary>
        /// <param name="args">Modift Account 화면에 전해줄 인수</param>
        private void ToModifyAccount(object args)
        {
            modifyAccountViewModel_.LoadPageData(args);
            CurrentViewModel = modifyAccountViewModel_;
        }
    }
}
