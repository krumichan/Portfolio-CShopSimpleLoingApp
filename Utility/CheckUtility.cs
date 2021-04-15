namespace LoginApp.Utility
{
    /// <summary>
    /// Data Check를 보조하는 유틸리티 클래스
    /// </summary>
    public static class CheckUtility
    {
        /// <summary>
        /// 해당 object의 null 또는 empty를 검사해준다.
        /// </summary>
        /// <param name="target">대상 object</param>
        /// <returns>null 또는 empty 검사 결과</returns>
        public static bool IsNullOrEmpty(object target)
        {
            bool isEmpty = false;

            if (typeof(string).IsInstanceOfType(target))
            {
                string convertTarget = target as string;
                isEmpty = convertTarget == null || convertTarget.Length == 0 /* || convertTarget.Trim().Length == 0 */;
            }

            return isEmpty;
        }
    }
}
