public class ReturnTypes
    {
        public struct None
        {
        }

        public struct Success
        {
        }

        public struct Forbidden
        {
        }

        public struct Unauthorized
        {
            public Unauthorized(string? message)
            {

            }

            public string? Message { get; set; }
        }

        public struct Conflict
        {
        }

        public struct BadRequest
        {
        }

        public struct UnprocessableEntity
        {
        }

        public struct InternalError
        {
        }
}
