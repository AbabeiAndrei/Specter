using System;
using System.Collections.Generic;

namespace Specter.Api.Services.Email
{
    public class ActivateUserEmailTemplate : BaseEmailTemplate
    {
        private readonly string _name;
        private readonly string _activateLink;

        public override string Subject => "Specter - Password Reset";

        public ActivateUserEmailTemplate(string name, string activateLink)
        {
            _name = name;
            _activateLink = activateLink;
        }

        protected override string BuildBody()
        {
            return $"Welcome {_name}, in order to activate your account, please follow this url : {_activateLink}";
        }
    }
}