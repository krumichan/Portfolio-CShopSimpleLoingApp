namespace LoginApp.Model
{
    /// <summary>
    /// 멤버 DTO 객체 ( 정확히는 VO 객체 )
    /// </summary>
    public class Member
    {
        public string id { get; set; }
        public string pw { get; set; }
        public string name { get; set; }
        /* public int deleteFlag { get; set; } */
    }
}