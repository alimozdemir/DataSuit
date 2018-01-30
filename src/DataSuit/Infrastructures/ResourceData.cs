using System;
using System.Collections.Generic;
using System.Text;

namespace DataSuit.Infrastructures
{
    internal class ResourceData
    {
        public List<string> AppNames { get; set; } = new List<string>();
        public List<string> FirstNames { get; set; } = new List<string>();
        public List<string> LastNames { get; set; } = new List<string>();
        public List<string> Emails { get; set; } = new List<string>();
        public List<string> CompanyNames { get; set; } = new List<string>();
        public List<string> DepartmentC { get; set; } = new List<string>();
        public List<string> DepartmentR { get; set; } = new List<string>();
        public List<string> CompanyNamesF { get; set; } = new List<string>();
        public List<string> IBANs { get; set; } = new List<string>();
        public List<string> JobTitles { get; set; } = new List<string>();
        public List<string> Language { get; set; } = new List<string>();
        public List<string> Slogans { get; set; } = new List<string>();
        public List<string> Adresses { get; set; } = new List<string>();
        public List<string> Usernames { get; set; } = new List<string>();
    }
}
