namespace Utils
{
    /// <summary>
    /// Class used for checking conditions in system EXCEPT domain. Will throw exception i a condition fails.
    /// </summary>
    public static class Check
    {
        public static void Require(bool assertion, string message = null)
        {

            if (!assertion) throw new PreconditionException(message);

        }
    }
}
