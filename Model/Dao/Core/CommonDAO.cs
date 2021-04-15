namespace LoginApp.Model.Dao.Core
{
    /// <summary>
    /// Database Access Object의 Metadata를 가지고 있는 기저 클래스
    /// </summary>
    abstract class CommonDAO
    {
        private readonly string dataSource_ = "localhost:1521:XE";
        private readonly string id_ = "hr";
        private readonly string pw_ = "hr";

        protected readonly string connectSource;
        public CommonDAO()
        {
            connectSource =
                "Data Source=" + dataSource_ + ";"
                + "User Id=" + id_ + ";"
                + "Password=" + pw_ + ";";
        }
    }
}
