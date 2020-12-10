using System;
using System.Collections.Generic;
using System.Text;

namespace Abouca.Domain.Information
{
    public interface IInformationDetails
    {
        public int Id { get; set; }
        public string LinkedinUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string GithubUrl { get; set; }
        public string MediumUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string StackOverFlowUrl { get; set; }
        public User.User User { get; set; }
        public int UserId { get; set; }
    }
}
