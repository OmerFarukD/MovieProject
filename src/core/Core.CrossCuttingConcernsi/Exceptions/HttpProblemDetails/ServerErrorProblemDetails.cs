using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public sealed class ServerErrorProblemDetails : ProblemDetails
    {

        public ServerErrorProblemDetails()
        {
            Title = "Internal Server Error";
            Detail = "Internal Server Error";
            Status = StatusCodes.Status500InternalServerError;
            Type = "http://example.com/problems/internalservererror";
        }
    }
}
