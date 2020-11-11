using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.HtmlHelpers;

namespace WordsTeacher.Controllers
{
    public class BaseController : Controller
    {
        private readonly List<string> _successMessages;
        private readonly List<string> _errorMessages;
        private readonly List<string> _infoMessages;
        private bool _initlialized;

        public BaseController()
        {
            _successMessages = new List<string>();
            _errorMessages = new List<string>();
            _infoMessages = new List<string>();
        }

        private void Initialize()
        {
            if (!_initlialized && TempData != null)
            {
                TempData[NotificationsName.SuccesNotificationName] = _successMessages;
                TempData[NotificationsName.ErrorNotificationName] = _errorMessages;
                TempData[NotificationsName.InformationNotificationName] = _infoMessages;
                _initlialized = true;
            }
        }

        public void AddSuccessMessage(string message)
        {
            Initialize();
            _successMessages.Add(message);
        }

        public void AddErrorMessage(string message)
        {
            Initialize();
            _errorMessages.Add(message);
        }

        public void AddInfoMessage(string message)
        {
            Initialize();
            _infoMessages.Add(message);
        }
    }
}