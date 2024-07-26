using Microsoft.EntityFrameworkCore;

namespace GraphQL.Addition
{
    public class OurErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception is DbUpdateConcurrencyException)
                return error.WithCode("DbUpd");
            return error;
        }
    }
}
