using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace Barber.Api.Controllers;

public class MainController : ControllerBase{
  public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary){
    var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

    return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
  }
  
  public ActionResult Validate(Dictionary<string, string[]> errors) {
    foreach (var error in errors){
      string key = error.Key;

      string[] values = error.Value;

      foreach(var value in values){
         ModelState.AddModelError(key, value);
      }
    }

    return ValidationProblem(ModelState);
  }
}