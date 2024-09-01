namespace Boxhead.Common
{
    public record Error
    {
        public string Code { get; init; }
        public string Message { get; init; }

        private Error() { }

        public static Error Create(string code, string message = "")
        {
            return new Error
            {
                Code = code,
                Message = message
            };
        }
    }
}