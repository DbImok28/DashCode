using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DashCode.Models
{
    public static class Validator
    {
        public static bool ValidateRegisterPassword(string input, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Password should not be empty";
                return false;
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than or greater than 12 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one special case characters";
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ValidateLogin(string input, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Login should not be empty";
                return false;
            }

            var mail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var username = new Regex(@"^[A-Za-z]+$");
            if (!mail.IsMatch(input))
            {
                if (!username.IsMatch(input))
                {
                    ErrorMessage = "Invalid login";
                    return false;
                }
            }
            return true;
        }
        public static bool ValidateUserName(string input, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "UserName should not be empty";
                return false;
            }
            var username = new Regex(@"^[A-Za-z]+$");
            if (!username.IsMatch(input))
            {
                ErrorMessage = "Invalid UserName";
                return false;
            }
            return true;
        }
        public static bool ValidateMail(string input, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Mail should not be empty";
                return false;
            }
            var mail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!mail.IsMatch(input))
            {
                ErrorMessage = "Invalid mail";
                return false;
            }
            return true;
        }
        public static bool ValidateName(string input, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Name should not be empty";
                return false;
            }
            var username = new Regex(@"^[A-Za-z]+[A-Za-z0-9_]*$");
            if (!username.IsMatch(input))
            {
                ErrorMessage = "Invalid Name";
                return false;
            }
            return true;
        }
        public static bool ValidateMSG(string input, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Name should not be empty";
                return false;
            }
            return true;
        }
    }
}
