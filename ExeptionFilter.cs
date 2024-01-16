using Microsoft.AspNetCore.Mvc.Filters;

namespace API_BasicStore
{

    public class ExeptionFilter : ExceptionFilterAttribute
    {
        public ILogger<ExeptionFilter> Logger { get; }

        public ExeptionFilter(ILogger<ExeptionFilter> logger)
        {
            Logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            Logger.LogInformation("\n\n   -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- \n\n");
            Logger.LogError(context.Exception, context.Exception.Message);
            Logger.LogInformation("\n\n   -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- \n\n");

            base.OnException(context);
        }
        
    }
}
