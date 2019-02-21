using System;
using System.Linq;
using Archysoft.D1.Model.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Archysoft.D1.Web.Api.Model
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string Description { get; set; }
        public long Timestamp { get; set; }

        public ApiResponse()
        {
            Status = 1;
            Description = "Success";
            Timestamp = DateTime.UtcNow.ConvertToTimestamp();
        }

        public ApiResponse(int status, string description)
        {
            Status = status;
            Description = description;
        }

        public ApiResponse(ModelStateDictionary state)
        {
            if (state == null) return;

            Status = -1;
            Timestamp = DateTime.Now.ConvertToTimestamp();

            var validationKeys = state.Keys.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            foreach (var validationKey in validationKeys)
            {
                var property = state[validationKey];
                if (property.ValidationState == ModelValidationState.Invalid)
                {
                    Description = property.Errors.FirstOrDefault()?.ErrorMessage;
                }
            }
        }
    }

    public class ApiResponse<T> : ApiResponse where T : class
    {
        public T Model { get; set; }

        public ApiResponse()
        {
            
        }

        public ApiResponse(T model)
        {
            Model = model;
        }
    }
}
