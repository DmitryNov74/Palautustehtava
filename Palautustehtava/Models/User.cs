using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace Palautustehtava.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int AccessLevelId { get; set; }

        internal string? WriteToken(SecurityToken token)
        {
            throw new NotImplementedException();
        }
    }
}
