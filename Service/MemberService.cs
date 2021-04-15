using LoginApp.Model;
using System.Collections.Generic;

namespace LoginApp.Service
{
    /// <summary>
    /// DAO와 실질적으로 데이터를 주고받는 서비스 클래스
    /// ( Interface 역할 )
    /// </summary>
    public class MemberService
    {
        private Model.Dao.MemberDAO memberDao;
        public MemberService()
        {
            memberDao = new Model.Dao.MemberDAO();
        }

        public List<Member> GetAll()
        {
            List<Member> members = memberDao.GetAll();
            return members;
        }

        public Member Get(string id)
        {
            Member member = memberDao.Get(id);
            return member;
        }

        public Member Get(string id, string pw)
        {
            Member member = memberDao.Get(id, pw);
            return member;
        }

        public bool Create(Member member)
        {
            bool success = memberDao.Create(member);
            return success;
        }

        public bool Modify(Member member)
        {
            bool success = memberDao.Modify(member);
            return success;
        }
        
        public bool Delete(Member member)
        {
            bool success = memberDao.Delete(member);
            return success;
        }
    }
}
