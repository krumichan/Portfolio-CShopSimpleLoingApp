using System.Windows;
using System.Windows.Input;

namespace LoginApp.ViewModel
{
    class LoginViewModel : Core.BaseViewModel, Core.IPageViewModel
    {
        private ICommand createAccountCommand_;
        private ICommand loginCommand_;

        private readonly Service.MemberService memberService_;

        private string password_;

        public LoginViewModel()
        {
            memberService_ = new Service.MemberService();
        }

        public void LoadPageData(object _data)
        {

        }

        // 회원생성화면 호출 명령
        public ICommand CreateAccountCommand
        {
            get => createAccountCommand_ ?? (createAccountCommand_ = new Command.ReplyCommand(CreateAccount));
        }

        // 로그인 명령
        public ICommand LoginCommand
        {
            get => loginCommand_ ?? (loginCommand_ = new Command.ReplyCommand<string>(Login));
        }

        // Password PasswordBox를 추적
        public string Password
        {
            get => password_;
            set
            {
                password_ = value; OnPropertyChanged("Password");
            }
        }

        /// <summary>
        /// 회원생성 이벤트
        /// </summary>
        private void CreateAccount()
        {
            ClearThisViewModelMembers();
            Core.CallbackMediator.Notify("ToCreateAccount");
        }

        /// <summary>
        /// 로그인 이벤트
        /// </summary>
        /// <param name="id">로그인시 입력한 id</param>
        private void Login(string id)
        {
            if (IsEmptySomeContent(new object[] { id, Password }))
            {
                MessageBox.Show("Some content is empty!!", "Data Empty");
                return;
            }

            Model.Member member = GetMember(id, Password);
            if (member != null)
            {
                ClearThisViewModelMembers();
                Core.CallbackMediator.Notify("ToLobby", member);
            }
            else
            {
                MessageBox.Show("please check your id or passward!!", "Login Checker");
            }
        }

        /* Event Support Method... ***************************************************/
        private bool IsEmptySomeContent(object[] targets)
        {
            bool isEmptySomeContent = false;
            foreach (object target in targets)
            {
                isEmptySomeContent = Utility.CheckUtility.IsNullOrEmpty(target);
                if (isEmptySomeContent)
                {
                    break;
                }
            }
            return isEmptySomeContent;
        }

        private Model.Member GetMember(string id, string pw)
        {
            return memberService_.Get(id, pw);
        }

        private void ClearThisViewModelMembers()
        {
            Password = "";
        }
    }
}
