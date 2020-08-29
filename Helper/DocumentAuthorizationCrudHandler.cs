using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;


namespace LienPhatERP.Helper
{
    public class DocumentAuthorizationCrudHandler:AuthorizationHandler<OperationAuthorizationRequirement, Document>
    {

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Document resource)
    {
        if (context.User.Identity.Name.Equals(resource.Author) &&
            requirement.Name == Operations.Read.Name)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    }
    public class CanDoAuthorizationCrudHandler : AuthorizationHandler<OperationAuthorizationRequirement, Document>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Document resource)
        {
            if (context.User.Identity?.Name == resource.Author &&
                requirement.Name == Operations.Read.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
    public class SameAuthorRequirement : IAuthorizationRequirement { }
    public class Document
    {
        public  string Author { get; set; }
        public string Url { get; set; }
        public  string FileName { get; set; }

    }
}
