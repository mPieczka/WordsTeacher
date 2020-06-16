using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.HtmlHelpers;
using WordsTeacher.Models.Ajax;

namespace WordsTeacher.Factories
{
    public class AjaxFactory
    {
        public AjaxResponse NotFound()
        {
            return new AjaxResponse { Success = false, Message = DefaultMessages.NotFoundMessage };
        }

        public AjaxResponse SuccesfullyDeleted()
        {
            return new AjaxResponse { Success = true, Message = DefaultMessages.DeletedMessage };
        }

        public AjaxResponse Successful()
        {
            return new AjaxResponse { Success = true };
        }
    }
}