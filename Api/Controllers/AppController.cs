using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Bson;

namespace Api.Controllers;
[Route("api-v1")]
public class AppController : ControllerBase
{
    public IActionResult StandarizedResponse<T>(T obj, int statusCode)
    {
        var responseCollection = new List<T>
        {
            obj
        };
        var httpResult = new ObjectResult(responseCollection){
            StatusCode = statusCode
        };
        return httpResult;
    }
    public IActionResult StandarizedResponse<T>(IEnumerable<T>? obj, int statusCode)
    {
        var httpResult = new ObjectResult(obj){
            StatusCode = statusCode
        };
        return httpResult;
    }
    public async Task<IActionResult> FinalValidation<T>(AbstractValidator<T> validator,T obj)
    {
        try{
            await validator.ValidateAndThrowAsync(obj);
            return StandarizedResponse<object>(null,StatusCodes.Status201Created);
        }catch(Exception e){
            return StandarizedResponse(e.Message,StatusCodes.Status400BadRequest);
        }
    }
}