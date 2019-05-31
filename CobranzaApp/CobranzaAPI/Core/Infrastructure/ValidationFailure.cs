namespace CobranzaAPI.Core.Infrastructure
{
    public struct ValidationFailure
    {
        public ValidationFailure(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
        
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }
    }
}