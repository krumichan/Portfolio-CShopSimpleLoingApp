using System.Windows;
using System.Windows.Input;

namespace LoginApp.ViewModel
{
    class ModifyAccountViewModel : Core.BaseViewModel, Core.IPageViewModel
    {
        private ICommand modifyAccountCommand_;
        private ICommand cancelCommand_;

        private readonly Service.MemberService memberService_;

        private string id_;
        private string password_;
        private string passwordCheck_;
        private string name_;

        private Model.Member currentUser;

        public ModifyAccountViewModel()
        {
            memberService_ = new Service.MemberService();
        }

        public void LoadPageData(object _data)
        {
            currentUser = _data as Model.Member;
            Id = currentUser.id;
            Password = currentUser.pw;
            PasswordCheck = currentUser.pw;
            Name = currentUser.name;
        }

        // 회원수정 명령
        public ICommand ModifyAccountCommand
        {
            get => modifyAccountCommand_ ?? (modifyAccountCommand_ = new Command.ReplyCommand(ModifyAccount));
        }

        // 취소 명령
        public ICommand CancelCommand
        {
            get => cancelCommand_ ?? (cancelCommand_ = new Command.ReplyCommand(Cancel));
        }

        // Id TextBox 추적
        public string Id
        {
            get => id_;
            set
            {
                id_ = value; OnPropertyChanged("Id");
            }
        }

        // password PasswordBox 추적
        public string Password
        {
            get => password_;
            set
            {
                password_ = value; OnPropertyChanged("Password");
            }
        }

        // password check PasswordBox 추적
        public string PasswordCheck
        {
            get => passwordCheck_;
            set
            {
                passwordCheck_ = value; OnPropertyChanged("PasswordCheck");
            }
        }

        // name TextBox 추적
        public string Name
        {
            get => name_;
            set
            {
                name_ = value; OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// 회원수정 이벤트
        /// </summary>
        private void ModifyAccount()
        {
            if (IsEmptySomeContent(new object[] { Password, PasswordCheck, Name }))
            {
                MessageBox.Show("Some content is empty!!", "Data Empty");
                return;
            }

            if (!IsEqualsPasswordAndCheckPassword(Password, PasswordCheck))
            {
                MessageBox.Show("password and password check are not equals!!", "Password Comparator");
                return;
            }

            Model.Member member = CreateNewMember(Id, Password, Name);
            if (ModifyMember(member))
            {
                ClearThisViewModelMembers();
                Core.CallbackMediator.Notify("ToLobby", member);
            }
            else
            {
                MessageBox.Show("please try one more time...", "Create Account");
            }
        }

        /// <summary>
        /// 취소 이벤트
        /// </summary>
        private void Cancel()
        {
            Model.Member passingUser = currentUser;
            ClearThisViewModelMembers();
            Core.CallbackMediator.Notify("ToLobby", passingUser);
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

        private bool IsEqualsPasswordAndCheckPassword(string pass, string passCk)
        {
            return pass.Equals(passCk);
        }

        private Model.Member CreateNewMember(string id, string pw, string name)
        {
            return new Model.Member
            {
                id = id
                ,pw = pw
                ,name = name
            };
        }

        private bool ModifyMember(Model.Member member)
        {
            return memberService_.Modify(member);
        }

        private void ClearThisViewModelMembers()
        {
            currentUser = null;
            Id = "";
            Password = "";
            PasswordCheck = "";
            Name = "";
        }
    }
}
