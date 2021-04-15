using System;
using System.Windows;
using System.Windows.Input;

namespace LoginApp.ViewModel
{
    class CreateAccountViewModel : Core.BaseViewModel, Core.IPageViewModel
    {
        private ICommand checkDuplicationCommand_;
        private ICommand createAccountCommand_;
        private ICommand cancelCommand_;

        private readonly Service.MemberService memberService_;

        private string password_;
        private string passwordCheck_;

        public CreateAccountViewModel()
        {
            memberService_ = new Service.MemberService();
        }

        public void LoadPageData(object _data)
        {

        }

        // 회원 ID 중복 확인 명령
        public ICommand CheckDuplicationCommand
        {
            get => checkDuplicationCommand_ ?? (checkDuplicationCommand_ = new Command.ReplyCommand<string>(CheckDuplication));
        }

        // 회원 생성 명령
        public ICommand CreateAccountCommand
        {
            get => createAccountCommand_ ?? (createAccountCommand_ = new Command.ReplyCommand<Tuple<string, string>>(CreateAccount));
        }

        // 취소 명령
        public ICommand CancelCommand
        {
            get => cancelCommand_ ?? (cancelCommand_ = new Command.ReplyCommand(Cancel));
        }

        // Password의 PasswordBox를 실시간으로 추적
        public string Password
        {
            get => password_;
            set
            {
                password_ = value; OnPropertyChanged("Password");
            }
        }

        // Password Check PasswordBox를 실시간으로 추적
        public string PasswordCheck
        {
            get => passwordCheck_;
            set
            {
                passwordCheck_ = value; OnPropertyChanged("PasswordCheck");
            }
        }

        /// <summary>
        /// 회원 id 중복확인 이벤트
        /// </summary>
        /// <param name="id">확인할 회원의 id</param>
        private void CheckDuplication(string id)
        {
            if (Utility.CheckUtility.IsNullOrEmpty(id))
            {
                MessageBox.Show("id is empty!!", "Data Empty");
                return;
            }
 
            if (GetMember(id) != null)
            {
                MessageBox.Show("id already exists!!", "Check Duplication");
            }
            else
            {
                MessageBox.Show("you can use this id.", "Check Duplication");
            }
        }

        /// <summary>
        /// 회원 생성 이벤트
        /// </summary>
        /// <param name="createAccountTuple">회원 id 및 이름 정보</param>
        private void CreateAccount(Tuple<string, string> createAccountTuple)
        {
            string id = createAccountTuple.Item1;
            string name = createAccountTuple.Item2;
            if (IsEmptySomeContent(new object[] { id, Password, PasswordCheck, name }))
            {
                MessageBox.Show("Some content is empty!!", "Data Empty");
                return;
            }

            if (!IsEqualsPasswordAndCheckPassword(Password, PasswordCheck))
            {
                MessageBox.Show("password and password check are not equals!!", "Password Comparator");
                return;
            }

            Model.Member member = CreateNewMember(id, Password, name);
            if (CreateMember(member))
            {
                ClearThisViewModelMembers();
                Core.CallbackMediator.Notify("ToLogin");
            }
            else
            {
                MessageBox.Show("please check duplication and try one more time...", "Create Account");
            }
        }

        private Model.Member GetMember(string id)
        {
            return memberService_.Get(id);
        }

        /// <summary>
        /// 취소 이벤트
        /// </summary>
        private void Cancel()
        {
            ClearThisViewModelMembers();
            Core.CallbackMediator.Notify("ToLogin");
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

        private bool CreateMember(Model.Member member)
        {
            return memberService_.Create(member);
        }

        private void ClearThisViewModelMembers()
        {
            Password = "";
            PasswordCheck = "";
        }
    }
}
