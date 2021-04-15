using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace LoginApp.Model.Dao
{
    /// <summary>
    /// TB_MEMBER 에 접근하기 위한 Data Access Object
    /// </summary>
    class MemberDAO : Core.CommonDAO
    {
        /// <summary>
        /// 모든 회원을 습득한다.
        /// </summary>
        /// <returns>모든 회원 리스트</returns>
        public List<Member> GetAll()
        {
            List<Member> members = new List<Member>();
            string sql = 
                @" SELECT * "
                + " FROM tb_member "
                /* + " WHERE delete_flag != 1 " */;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectSource))
                {
                    connection.Open();

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;

                        using (OracleDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Member member = new Member();
                                member.id = dataReader["ID"] as string;
                                member.pw = dataReader["PW"] as string;
                                member.name = dataReader["NAME"] as string;
                                // member.deleteFlag = (int) dataReader["DELETE_FLAG"];
                                members.Add(member);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Error");
            }

            return members;
        }

        /// <summary>
        /// 임의의 회원을 습득한다.
        /// </summary>
        /// <param name="id">임의의 회원의 id값</param>
        /// <returns>임의의 회원</returns>
        public Member Get(string id)
        {
            Member member = null;
            string sql =
                @" SELECT * "
                + " FROM tb_member "
                + " WHERE id = :id "
                /* + " AND delete_flag != 1 " */;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectSource))
                {
                    connection.Open();

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Parameters.Add(new OracleParameter("id", id));

                        using (OracleDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                member = new Member();
                                member.id = dataReader["ID"] as string;
                                member.pw = dataReader["PW"] as string;
                                member.name = dataReader["NAME"] as string;
                                // member.deleteFlag = (int) dataReader["DELETE_FLAG"];
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Error");
            }

            return member;
        }

        /// <summary>
        /// 임의의 회원을 습득한다.
        /// </summary>
        /// <param name="id">회원 id값</param>
        /// <param name="pw">회원 pw값</param>
        /// <returns>임의의 회원</returns>
        public Member Get(string id, string pw)
        {
            Member member = null;
            string sql =
                " SELECT * "
                + " FROM tb_member "
                + " WHERE id = :id "
                + " AND pw = :pw "
                /* + " AND delete_flag != 1 " */;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectSource))
                {
                    connection.Open();

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Parameters.Add("id", id);
                        command.Parameters.Add("pw", pw);

                        using (OracleDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                member = new Member();
                                member.id = dataReader["ID"] as string;
                                member.pw = dataReader["PW"] as string;
                                member.name = dataReader["NAME"] as string;
                                // member.deleteFlag = (int) dataReader["DELETE_FLAG"];
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Error");
            }

            return member;
        }

        /// <summary>
        /// 새로운 회원을 생성한다.
        /// </summary>
        /// <param name="member">새로운 회원의 정보</param>
        /// <returns>생성 성공 여부</returns>
        public bool Create(Member member)
        {
            bool success = false;
            string sql =
                " INSERT INTO tb_member "
                + " VALUES ( :id, :pw, :name ) "
                /* + " VALUES ( :id, :pw, :name, 0 ) "*/;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectSource))
                {
                    connection.Open();

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        using (OracleTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                        {
                            try
                            {
                                command.Transaction = transaction;
                                command.CommandText = sql;
                                command.Parameters.Add("id", member.id);
                                command.Parameters.Add("pw", member.pw);
                                command.Parameters.Add("name", member.name);

                                int count = command.ExecuteNonQuery();
                                success = count == 1;

                                transaction.Commit();
                            }
                            catch (DataException)
                            {
                                transaction.Rollback();
                                success = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Error");
            }

            return success;
        }

        /// <summary>
        /// 회원 정보를 수정한다.
        /// </summary>
        /// <param name="member">수정 대상의 회원 정보</param>
        /// <returns>수정 성공 여부</returns>
        public bool Modify(Member member)
        {
            bool success = false;
            string sql =
                " UPDATE tb_member "
                + " SET pw = :pw "
                + " ,   name = :name "
                + " WHERE id = :id "
                /* + " AND delete_flag != 1 " */;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectSource))
                {
                    connection.Open();

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        using (OracleTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                        {
                            try
                            {
                                command.Transaction = transaction;
                                command.CommandText = sql;
                                command.Parameters.Add("pw", member.pw);
                                command.Parameters.Add("name", member.name);
                                command.Parameters.Add("id", member.id);
                                // member.deleteFlag = (int) dataReader["DELETE_FLAG"];

                                int count = command.ExecuteNonQuery();
                                success = count == 1;

                                transaction.Commit();
                            }
                            catch (DataException)
                            {
                                transaction.Rollback();
                                success = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Error");
            }

            return success;
        }

        /// <summary>
        /// 회원을 삭제한다.
        /// </summary>
        /// <param name="member">삭제 대상의 회원 정보</param>
        /// <returns>삭제 성공 여부</returns>
        public bool Delete(Member member)
        {
            bool success = false;
            string sql =
                " DELETE FROM tb_member "
                + " WHERE id = :id "
                + " AND pw = :pw "
                + " AND name = :name ";
            /*
             string sql =
                " UPDATE tb_member "
                + " SET delete_flag = 1 "
                + " WHERE id = :id ";
             */

            try
            {
                using (OracleConnection connection = new OracleConnection(connectSource))
                {
                    connection.Open();

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        using (OracleTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                        {
                            try
                            {
                                command.Transaction = transaction;
                                command.CommandText = sql;
                                command.Parameters.Add("id", member.id);
                                command.Parameters.Add("pw", member.pw);
                                command.Parameters.Add("name", member.name);

                                /*
                                command.Transaction = transaction;
                                command.CommandText = sql;
                                command.Parameters.Add("id", member.id);
                                */

                                int count = command.ExecuteNonQuery();
                                success = count == 1;

                                transaction.Commit();
                            }
                            catch (DataException)
                            {
                                transaction.Rollback();
                                success = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Error");
            }

            return success;
        }
    }
}
