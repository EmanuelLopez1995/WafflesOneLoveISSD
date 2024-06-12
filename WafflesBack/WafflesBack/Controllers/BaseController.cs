using Microsoft.AspNetCore.Mvc;

namespace WafflesBack.Controllers
{
    public class BaseController : ControllerBase
    {
        protected string GetParameterFromQuery(string name)
        {
            string value = HttpContext.Request.Query[name]; return value ?? string.Empty;
        }
    }
}
