namespace GameStoreApplication.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Views;


    public class Controller
    {
        private const string DefaultPath = @"Resources\{0}.html";
        private const string ContentPlaceholder = "{{{content}}}";

        protected IDictionary<string, string> ViewData { get; private set; }

        protected Controller()
        {

            this.ViewData = new Dictionary<string, string>
            {
                ["showError"] = "none",
                ["authenticatedDisplay"] = "flex",
                ["anonymousDisplay"] = "none"

            };
        }

        public IHttpResponse FileViewResponse(string fileName)
        {
            var resultHtml = ProcessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    resultHtml = resultHtml.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(resultHtml));
        }

        private static string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));
            var fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));

            return layoutHtml.Replace(ContentPlaceholder, fileHtml);
        }

        protected string ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, context, results, true) == false)
            {
                foreach (var result in results)
                {
                    if (result != ValidationResult.Success)
                    {
                        return result.ErrorMessage;
                    }
                }
            }

            return null;
        }
    }
}
